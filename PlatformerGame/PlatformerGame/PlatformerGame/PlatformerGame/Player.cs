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

        #endregion


        #region Gameplay Data

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

        public Player(Level level, Vector2 startPos)
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

        }

        #endregion


        #region Update and Draw

        public void HandleInput()
        {
        }

        private void HandleCollisions()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
        
        private void Jump()
        {
        }

        public void Reset(Vector2 position)
        {
        }

        private void Die()
        {
        }

        private void BeatLevel()
        {
        }

        #endregion
    }
}
