#region File Description
//-----------------------------------------------------------------------------
// SplashScreen.cs
//
// A class representing a simple, unanimated, single-image splash screen
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Mono_PlatformerGame
{
    class SplashScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D background;
        TimeSpan displayTime;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructs a new splash screen.
        /// </summary>
        public SplashScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(3.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            displayTime = new TimeSpan();
        }

        /// <summary>
        /// Loads content for the splash screen.
        /// </summary>
        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            // Load our background texture
            background = content.Load<Texture2D>("splash");

            base.LoadContent();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the splash screen - we should automatically transition to the
        /// next screen after three seconds have elapsed
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // Add our elapsed time to the total time we've been displaying
            displayTime += gameTime.ElapsedGameTime;

            // If we've reached three seconds, transition to the next screen
            if (displayTime > TimeSpan.FromSeconds(2.0))
            {
                ScreenManager.AddScreen(new MainMenuScreen(), null);
                ScreenManager.RemoveScreen(this);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// Processes the user's input, to skip the splash screen.
        /// </summary>
        /// <param name="input">The InputState for the current frame</param>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // If the user preses the start button or space key, move to the next screen
            if (input.IsNewButtonPress(Buttons.Start, null, out playerIndex) || input.IsNewKeyPress(Keys.Space, null, out playerIndex))
            {
                ScreenManager.AddScreen(new MainMenuScreen(), null);
                ScreenManager.RemoveScreen(this);
            }

            base.HandleInput(input);
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draw the splash screen
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Draw the image
            spriteBatch.Begin();
            spriteBatch.Draw(background, ScreenManager.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
