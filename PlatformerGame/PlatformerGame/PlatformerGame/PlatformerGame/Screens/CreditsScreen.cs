#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
#endregion

namespace PlatformerGame
{
    class CreditsScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteBatch spriteBatch;

        string creditText;
        float scrollSpeed;

        Vector2 textPos;
        SpriteFont font;

        int numLines;
        int textHeight;

        bool paused;

        #endregion

        #region Initialization

        public CreditsScreen()
        {
            // Sets how fast the credits will scroll
            scrollSpeed = 0.1f;

            // Build the credit text here
            creditText = "CREDITS\n\n"
                         + "Level Editor\n"
                         + "--Matt Hudson\n\n"
                         + "Platformer Game\n"
                         + "--Matthew Strayhall\n\n"
                         + "\n\n"
                         + "XNA Creator's Club Tutorials\n"
                         + "--Starter code for menu system\n"
                         + "--Inspiration for level, player, and enemy class base structure"
                         + "\n\n"
                         + "Testers\n"
                         + "--Ben Kern\n"
                         + "--Kevin Schwarz"
                         + "\n\n"
                         + "Special Thanks\n"
                         + "--Nathan Bean";

        }

        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            // Grab the spriteBatch from the screenManager
            spriteBatch = ScreenManager.SpriteBatch;

            // Set the initial text position
            textPos = new Vector2(100, ScreenManager.GraphicsDevice.Viewport.Height);
            font = content.Load<SpriteFont>("creditFont");

            // See how many lines we have so we know how long to scroll before returning to menu
            string[] tokens = creditText.Split('\n');
            numLines = tokens.Length;
            textHeight = numLines * font.LineSpacing;
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        #endregion


        #region Update and Draw

        /// <summary>
        /// Allow the user to pause the credits or exit them early.
        /// </summary>
        /// <param name="input"></param>
        public override void HandleInput(InputState input)
        {
            // Allow player to return to main menu at any time.
            PlayerIndex playerIndex;

            // If the user presses the ESCAPE key or the back button, back out of this screen
            if (input.IsNewKeyPress(Keys.Escape, null, out playerIndex) || input.IsNewButtonPress(Buttons.Back, null, out playerIndex))
            {
                this.ExitScreen();
            }

            // If the user presses the SPACE key or the A button, pause the credits
            if (input.IsNewKeyPress(Keys.Space, null, out playerIndex) || input.IsNewButtonPress(Buttons.A, null, out playerIndex))
            {
                if (paused == false) paused = true;
                else paused = false;
            }

            base.HandleInput(input);
        }

        /// <summary>
        /// Scroll the text until we are off the top of the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!paused)
            {
                // Update the text position if we aren't paused.
                textPos.Y -= scrollSpeed * gameTime.ElapsedGameTime.Milliseconds;
            }

            if (textPos.Y + textHeight < 0)
            {
                // We have scrolled through all credits off the top of the screen
                // so we can return to the main menu.
                this.ExitScreen();
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// Draw the credit text
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, creditText, textPos, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    
    }
}
