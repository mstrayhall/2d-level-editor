#region FileDescription
/* Player.cs
 * 
 * Represents a player in the level.
 * 
 * Uses the XNA Platformer Starter Kit class of the same name as a starting framework.
 * 
 */
#endregion

#region UsingStatements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Mono_PlatformerGameLibrary
{
    public class Player
    {
        #region Constants

        private const float WALKSPEED = 300f;
        private const float RUNSPEED = 500f;

        private const float MAXJUMPTIME = 0.45f;
        private const float JUMPLAUNCHSPEED = -4000f;
        private const float GRAVITYACCEL = 4000f;
        private const float MAXFALLSPEED = 1000f;
        private const float JUMPCONTROLPOWER = 0.15f;

        #endregion


        #region Gameplay Data

        public struct PlayerInput
        {
            public bool Right;
            public bool Left;
            public bool Jump;
            public bool Crouch;
            public bool Run;
        }

        PlayerInput playerInput;
        InputManager inputManager;

        TimeSpan lastHurt;
        SpriteEffects flip = SpriteEffects.None;
        Texture2D sprite;

        public float Health
        {
            get { return health; }
            set { health = value; }
        }
        float health;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public bool IsAlive
        {
            get { return isAlive; }
        }
        bool isAlive;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;
        Vector2 startPosition;

        float previousBottom;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;

        public bool IsOnGround
        {
            get { return isOnGround; }
            set { isOnGround = value; }
        }
        bool isOnGround;

        bool isJumping;
        bool wasJumping;
        float jumpTime;

        float movement = 0f;

        // The "origin" of the player, the bottom center of the sprite
        Vector2 origin;
        // The local bounds of this player 
        Rectangle localBounds;
        // Gets the bounding rectangle for the player in world space
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        Rectangle sourceImageSize;
        int playerWidth;
        int playerHeight;
        
        #endregion


        #region Initialization

        public Player(Level level, Vector2 startPos, Texture2D textureName)
        {
            this.level = level;

            startPosition = startPos;
            position = startPosition;

            inputManager = new InputManager();

            LoadContent(textureName);

            Reset(position);
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
            playerWidth = sizeX;
            playerHeight = sizeY;
        }

        #endregion


        #region Update and Draw

        public void AddHealth(int value)
        {
            health += value;
            health = (int)MathHelper.Clamp(health, 0, 100);
        }

        public void RemoveHealth(int value, Enemy hurtBy)
        {
            if (lastHurt > TimeSpan.FromSeconds(1))
            {
                health -= value;
                health = (int)MathHelper.Clamp(health, 0, 100);
                lastHurt = TimeSpan.Zero;

                if (health <= 0)
                {
                    Die(hurtBy);
                }
            }
        }

        public void HandleInput(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 prevPos = position;

            // Clear old input and x velocity
            movement = 0f;

            // Get the input from the player
            playerInput.Right = inputManager.IsRight(null);
            playerInput.Left = inputManager.IsLeft(null);
            playerInput.Jump = inputManager.IsJump(null);
            playerInput.Crouch = inputManager.IsCrouch(null);
            playerInput.Run = inputManager.IsRun(null);
            
            isJumping = playerInput.Jump;
            if (playerInput.Left) movement = -1f;
            if (playerInput.Right) movement = 1f;

            // Update velocity based on the input
            if (playerInput.Run)
            {
                velocity.X = movement * RUNSPEED;
            }
            else
            {
                velocity.X = movement * WALKSPEED;
            }

            // Calculate velocity from jumping
            velocity.Y = Jump(velocity.Y, gameTime);
            // Add gravity's force to this
            velocity.Y += GRAVITYACCEL * elapsed;
            velocity.Y = MathHelper.Clamp(velocity.Y, -MAXFALLSPEED, MAXFALLSPEED);

            velocity.X = MathHelper.Clamp(velocity.X, -RUNSPEED, RUNSPEED);

            // Apply the velocity to the position
            position += velocity * elapsed;
            position = new Vector2((float)Math.Round(position.X), (float)Math.Round(position.Y));
            

            // If player has moved into some object, move them out of it
            HandleCollisions();

            // If we were unable to move, stop velocity in that direction
            if (prevPos.X == position.X)
            {
                velocity.X = 0;
            }
            if (prevPos.Y == position.Y)
            {
                velocity.Y = 0;
            }
        }

        /// <summary>
        /// Detects and resolves all collisions between the player and his neighboring
        /// tiles. When a collision is detected, the player is pushed away along one
        /// axis to prevent overlapping. There is some special logic for the Y axis to
        /// handle platforms which behave differently depending on direction of movement.
        /// </summary>
        private void HandleCollisions()
        {
            // Get the player's bounding rectangle and find neighboring tiles.
            Rectangle bounds = BoundingRectangle;
            int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
            int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;

            // Reset flag to search for ground collision.
            isOnGround = false;

            // For each potentially colliding tile,
            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    // If this tile is collidable,
                    TileCollision collision = Level.GetCollision(x, y);
                    if (collision != TileCollision.Passable)
                    {
                        // Determine collision depth (with direction) and magnitude.
                        Rectangle tileBounds = Level.GetBounds(x, y);
                        Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            // Resolve the collision along the shallow axis. Force it to resolve X if the player is out of bounds of the level.
                            if ((absDepthY <= absDepthX || collision == TileCollision.Platform) && bounds.Left >= 0 && bounds.Right <= Level.PixelWidth)
                            {
                                // If we crossed the top of a tile, we are on the ground.
                                if (previousBottom <= tileBounds.Top)
                                    isOnGround = true;

                                // Ignore platforms, unless we are on the ground.
                                if (collision == TileCollision.Impassable || IsOnGround)
                                {
                                    // Resolve the collision along the Y axis.
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    // Perform further collisions with the new bounds.
                                    bounds = BoundingRectangle;
                                }

                                if (isJumping && depth.Y > 0)
                                {
                                    jumpTime = 0f;
                                }
                            }
                            else if (collision == TileCollision.Impassable) // Ignore platforms.
                            {
                                // Resolve the collision along the X axis.
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                // Perform further collisions with the new bounds.
                                bounds = BoundingRectangle;
                            }
                        }
                    }
                }
            }

            // Save the new bounds bottom.
            previousBottom = bounds.Bottom;
        }

        public void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            lastHurt += gameTime.ElapsedGameTime;

            HandleInput(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else if (velocity.X < 0)
            {
                flip = SpriteEffects.None;
            }

            // Draw all of the source image compressed to the dest player rectangle.
            Rectangle destRect = new Rectangle((int)(position.X - playerWidth/2), (int)(position.Y - playerHeight), playerWidth, playerHeight);
            spriteBatch.Draw(sprite, destRect, sourceImageSize, Color.White, 0f, Vector2.Zero, flip, 0f);
        }
        
        private float Jump(float velocityY, GameTime gameTime)
        {
            // If the player wants to jump
            if (isJumping)
            {
                // Begin or continue a jump
                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                {
                    if (jumpTime == 0.0f)
                    {
                        //jumpSound.Play();
                    }

                    jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //sprite.PlayAnimation(jumpAnimation);
                }

                // If we are in the ascent of the jump
                if (0.0f < jumpTime && jumpTime <= MAXJUMPTIME)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    velocityY = JUMPLAUNCHSPEED * (1.0f - (float)Math.Pow(jumpTime / MAXJUMPTIME, JUMPCONTROLPOWER));
                }
                // We are descending
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                // and sets velocity to zero if we were previously ascending.
                // NOTE: this might not play well if we introduced other game elements that propel the player upward.
                if (wasJumping && velocityY < 0f) velocityY = 0f;
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
        }

        public void Reset(Vector2 position)
        {
            Position = startPosition;
            Velocity = Vector2.Zero;
            isAlive = true;
        }

        public void Die(Enemy killedBy)
        {
            isAlive = false;
        }

        public void BeatLevel()
        {
        }

        #endregion
    }
}
