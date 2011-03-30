#region File Description
//-----------------------------------------------------------------------------
// LoadGameScreen.cs
//
// Allows the user to select which game to play or which level files to load 
// from their local directory.
//-----------------------------------------------------------------------------
#endregion

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
using System.Windows.Forms;
#endregion

namespace PlatformerGame
{
    class LoadGameScreen : GameScreen
    {
        bool directoryFound;
        string directoryPath;

        public LoadGameScreen()
        {
            directoryFound = false;
        }

        public override void LoadContent()
        {
            directoryPath = LoadGame();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public string LoadGame()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // Default to the directory which contains our content files.
            string executingPath = AppDomain.CurrentDomain.BaseDirectory;
            string gameContentPath = Path.Combine(executingPath, "../../../../PlatformerGameContent");

            fbd.SelectedPath = gameContentPath;
            fbd.Description = "Select a folder containing level files";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderPath = fbd.SelectedPath;
                directoryFound = true;
                return folderPath;
            }
            else return "";

        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (directoryFound)
            {
                ScreenManager.AddScreen(new GameplayScreen(0, directoryPath), null);
                this.ExitScreen();
            }
            else
            {
                // We exited the dialog box or something bad happened
                this.ExitScreen();
                return;
            }
            
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
