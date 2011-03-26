#region File Description
//-----------------------------------------------------------------------------
// GameOverScreen.cs
//
// The endgame screen
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace PlatformerGame
{
    class GameOverScreen : GameScreen
    {
        SpriteFont gameOverFont;

        ContentManager content;

        TimeSpan displayTime;

        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameOverFont = content.Load<SpriteFont>("gameFont");

            displayTime = TimeSpan.FromSeconds(0);

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
             // Add our elapsed time to the total time we've been displaying
            displayTime += gameTime.ElapsedGameTime;

            // If we've reached three seconds, transition to the next screen
            if (displayTime > TimeSpan.FromSeconds(5.0))
            {
                ScreenManager.RemoveScreen(this);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // If the user preses the space bar, start a new game
            if (input.IsNewButtonPress(Buttons.Start, ControllingPlayer, out playerIndex) || input.IsNewKeyPress(Keys.Space, ControllingPlayer, out playerIndex))
            {
                ScreenManager.RemoveScreen(this);
            }

            base.HandleInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Clear to black
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.DrawString(gameOverFont, "GAME OVER", new Vector2((ScreenManager.GraphicsDevice.Viewport.Width / 2) - 100, (ScreenManager.GraphicsDevice.Viewport.Height / 3)), Color.Snow);

            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
