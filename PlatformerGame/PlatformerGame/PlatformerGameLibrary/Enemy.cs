#region FileDescription
/* Enemy.cs
 * 
 * Represents an enemy in the level.
 * 
 * Uses the XNA Platformer Starter Kit class of the same name as a starting framework.
 * 
 */
#endregion

#region UsingStatements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace PlatformerGameLibrary
{ 
    /// <summary>
    /// Facing direction along the X axis.
    /// </summary>
    enum FaceDirection
    {
        Left = -1,
        Right = 1,
    }

    public class Enemy
    {
        #region Fields

        Texture2D sprite;

        Level level;
        public Level Level
        {
            get { return level; }
        }

        /// <summary>
        /// Position in world space of the bottom center of this enemy.
        /// </summary>
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        // The "origin" of the enemy, the bottom center of the sprite
        Vector2 origin;
        // The local bounds of this enemy 
        private Rectangle localBounds;
        /// <summary>
        /// Gets a rectangle which bounds this enemy in world space.
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        // The direction this enemy is facing and moving along the X axis.
        private FaceDirection direction = FaceDirection.Left;

        // How long this enemy has been waiting before turning around.
        private float waitTime;

        // How long to wait before turning around.
        private const float MaxWaitTime = 0.1f;

        // How fast it moves in X direction.
        private const float MoveSpeed = 128.0f;

        Rectangle sourceImageSize;
        int enemyWidth;
        int enemyHeight;

        #endregion

        #region Initialization

        public Enemy(Level level, Vector2 pos, Texture2D textureName)
        {
            this.level = level;
            position = pos;

            LoadContent(textureName);
        }

        public void LoadContent(Texture2D textureName)
        {
            int sizeX = 32;
            int sizeY = 32;

            int width = (int)(sizeX * 1.0);
            int height = (int)(sizeY * 1.0);
            int left = (sizeX - width) / 2;
            int top = (sizeY - height) / 2;
            localBounds = new Rectangle(left, top, width, height);

            origin = new Vector2(width / 2, height);

            sprite = textureName;

            sourceImageSize = new Rectangle(0, 0, sprite.Width, sprite.Height);
            enemyWidth = sizeX;
            enemyHeight = sizeY;
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculate tile position based on the side we are walking towards.
            float posX = Position.X + localBounds.Width / 2 * (int)direction;
            int tileX = (int)Math.Floor(posX / Tile.Width) - (int)direction;
            int tileY = (int)Math.Floor(Position.Y / Tile.Height);

            if (waitTime > 0)
            {
                // Wait for some amount of time.
                waitTime = Math.Max(0.0f, waitTime - (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (waitTime <= 0.0f)
                {
                    // Then turn around.
                    direction = (FaceDirection)(-(int)direction);
                }
            }
            else
            {
                // If we are about to run into a wall or off a cliff, start waiting.
                if (Level.GetCollision(tileX + (int)direction, tileY - 1) == TileCollision.Impassable ||
                    Level.GetCollision(tileX + (int)direction, tileY) == TileCollision.Passable)
                {
                    waitTime = MaxWaitTime;
                }
                else
                {
                    // Move in the current direction.
                    Vector2 velocity = new Vector2((int)direction * MoveSpeed * elapsed, 0.0f);
                    position = position + velocity;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw facing the way the enemy is moving.
            SpriteEffects flip = direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            //spriteBatch.Draw(sprite, position, localBounds, Color.White, 0f, origin, 1f, flip, 0f);

            // Draw all of the source image compressed to the dest player rectangle.
            Rectangle destRect = new Rectangle((int)(position.X - enemyWidth / 2), (int)(position.Y - enemyHeight), enemyWidth, enemyHeight);
            spriteBatch.Draw(sprite, destRect, sourceImageSize, Color.White, 0f, Vector2.Zero, flip, 0f);
        }

        #endregion

    }
}
