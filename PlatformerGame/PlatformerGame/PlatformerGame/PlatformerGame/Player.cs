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

namespace PlatformerGame
{
    class Player
    {
        #region Constants

        private const float MaxMoveSpeed = 1000f;
        private const float MoveAccel = 10000f;

        private const float MaxJumpTime = 0.5f;
        private const float JumpLaunchSpeed = -5000f;
        private const float GravityAccel = 4000f;
        private const float MaxFallSpeed = 1000f;
        private const float JumpControlPower = 0.15f;

        #endregion


        #region Gameplay Data

        TimeSpan damageTimer;
        SpriteEffects flip = SpriteEffects.None;
        Texture2D sprite;

        public float Health
        {
            get { return health; }
            set { health = value; }
        }
        float health;

        public int NumLives
        {
            get { return numLives; }
        }
        int numLives;

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
                int left = (int)Math.Round(Position.X - 0) + localBounds.X;
                int top = (int)Math.Round(Position.Y - 0) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }
        
        
        #endregion


        #region Initialization

        public Player(Level level, Vector2 startPos, Texture2D texture)
        {
            this.level = level;

            LoadContent();
        }

        public void LoadContent()
        {
            // Calculate the local edges of the texture
            int width = 10;
            int height = 10;
            int left = -(width / 3);
            int top = -(height / 3);
            localBounds = new Rectangle(left, top, width, height);

            origin = new Vector2(width / 2, height);

            sprite = level.Content.Load<Texture2D>("splash");
        }

        #endregion


        #region Update and Draw

        public void HandleInput(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 prevPos = position;

            // Get the input from the player



            // Update velocity based on the input
            velocity.X += movement * MoveAccel * elapsed;
            velocity.Y += GravityAccel * elapsed;

            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -MaxFallSpeed, MaxFallSpeed);

            velocity.Y = Jump(velocity.Y, gameTime);

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

        private void HandleCollisions()
        {
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);

            // TODO: update animation if necessary

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

            spriteBatch.Draw(sprite, Vector2.Zero, localBounds, Color.White, 0f, origin, 1f, flip, 1f);
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
                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    // Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
                    velocityY = JumpLaunchSpeed * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                }
                else
                {
                    // Reached the apex of the jump
                    jumpTime = 0.0f;
                }
            }
            else
            {
                // Continues not jumping or cancels a jump in progress
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;

            return velocityY;
        }

        public void Reset(Vector2 position)
        {
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
