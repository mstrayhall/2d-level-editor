#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Displays a main menu
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
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
    class MainMenuScreen : MenuScreen
    {
        ContentManager content;

        // Menu Music
        Song menuMusic;

        public MainMenuScreen()
            : base("2D Platformer -- DIY")
        {
           
            // Create our menu entries
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry editLevelsMenuEntry = new MenuEntry("Create/Modify Levels");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");            
            MenuEntry creditsMenuEntry = new MenuEntry("Credits");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Add event handlers to menu entries
            playGameMenuEntry.Selected += OnPlayGame;
            editLevelsMenuEntry.Selected += OnEditLevels;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            creditsMenuEntry.Selected += OnViewCredits;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(editLevelsMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(creditsMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            TransitionOnTime = TimeSpan.FromSeconds(0.3);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);
        }

        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            // Menu Music
            //menuMusic = content.Load<Song>("ZeroTheUltimateWarrior");
            //MediaPlayer.Play(menuMusic);
            //MediaPlayer.IsRepeating = true;

            base.LoadContent();
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void OnPlayGame(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new LoadGameScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Create/Modify Levels menu entry is selected.
        /// </summary>
        void OnEditLevels(object sender, PlayerIndexEventArgs e)
        {
            Form1 form = new Form1(ScreenManager.Game.Services);
            form.ShowDialog();
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Credits menu entry is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnViewCredits(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditsScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

    }
}
