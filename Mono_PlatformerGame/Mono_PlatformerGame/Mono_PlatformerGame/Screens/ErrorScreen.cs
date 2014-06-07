#region File Description
//-----------------------------------------------------------------------------
// ErrorScreen.cs
//
// This prints errors out to handle them "gracefully" so the player doesn't have
// to deal with a straight up crash.
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
using System.Windows.Forms;
#endregion

namespace Mono_PlatformerGame
{
    class ErrorScreen : GameScreen
    {
        SpriteFont errorFont;

        ContentManager content;

        string errorMessage;
        Vector2 errorPos;

        public ErrorScreen(string message)
        {
            errorMessage = "Error\n\n"
                + message
                + "\n\n"
                + "Press Space or Escape to return to the main menu.";

            errorPos = new Vector2(20, 20);
        }

        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            errorFont = content.Load<SpriteFont>("errorFont");

            base.LoadContent();
        }

        public override void UnloadContent()
        {

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            // If the user preses the space bar or escape, exit the screen to return to the menu
            if (input.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.Space, ControllingPlayer, out playerIndex)
                || input.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.Escape, ControllingPlayer, out playerIndex))
            {
                ScreenManager.RemoveScreen(this);
            }

            base.HandleInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Clear to black
            ScreenManager.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.DrawString(errorFont, errorMessage, errorPos, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
