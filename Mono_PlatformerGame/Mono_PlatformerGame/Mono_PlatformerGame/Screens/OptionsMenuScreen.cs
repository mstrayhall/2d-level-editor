#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Based off an example of the Peer to Peer Game Project from the following:
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
#endregion

namespace Mono_PlatformerGame
{
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
                
        //bool isFullscreen;
        //MenuEntry fullscreenMenuEntry;

        bool isLevelTimed;
        MenuEntry timeLimitMenuEntry;

        bool isMusicOn;
        MenuEntry musicMenuEntry;

        bool isUnlimitedLives;
        MenuEntry livesMenuEntry;

        MenuEntry backMenuEntry;

        #endregion

        #region Initialization

        public OptionsMenuScreen()
            : base("Options")
        {
            
            // Create our menu entries
            //fullscreenMenuEntry = new MenuEntry(String.Empty);
            timeLimitMenuEntry = new MenuEntry(String.Empty);
            musicMenuEntry = new MenuEntry(String.Empty);
            livesMenuEntry = new MenuEntry(String.Empty);
            backMenuEntry = new MenuEntry("Back");

            SetMenuEntryText();

            // Hook up menu event handlers
            //fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;
            timeLimitMenuEntry.Selected += TimeLimitMenuEntrySelected;
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            livesMenuEntry.Selected += LivesMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            // Add entries to the menu
            //MenuEntries.Add(fullscreenMenuEntry);
            MenuEntries.Add(timeLimitMenuEntry);
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(livesMenuEntry);
            MenuEntries.Add(backMenuEntry);

            TransitionOnTime = TimeSpan.FromSeconds(0.3);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);

        }

        public override void LoadContent()
        {
            // Grab the current fullscreen state
            //isFullscreen = ScreenManager.GraphicsDeviceManager.IsFullScreen;

            isLevelTimed = ScreenManager.IsTimeLimit;
            isMusicOn = ScreenManager.IsMusicOn;
            isUnlimitedLives = ScreenManager.IsUnlimitedLives;

            // Update the menu entry text
            SetMenuEntryText();
             
            base.LoadContent();
        }

        #endregion

        #region Handle Input and Update

        /// <summary>
        /// Updates the menu entry text to be displayed.
        /// </summary>
        void SetMenuEntryText()
        {
            //fullscreenMenuEntry.Text = "Fullscreen: " + (isFullscreen ? "Enabled" : "Disabled");
            timeLimitMenuEntry.Text = "Time Limit: " + (isLevelTimed ? "On" : "Off");
            musicMenuEntry.Text = "Music: " + (isMusicOn ? "On" : "Off");
            livesMenuEntry.Text = "Lives: " + (isUnlimitedLives ? "Unlimited" : "3 per level");
        }

        /// <summary>
        /// Event handler for when the TimeLimit menu entry is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimeLimitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // Toggle the time limit boolean
            isLevelTimed = !isLevelTimed;
            ScreenManager.IsTimeLimit = isLevelTimed;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Music menu entry is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // Toggle the time limit boolean
            isMusicOn = !isMusicOn;
            ScreenManager.IsMusicOn = isMusicOn;

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Lives menu entry is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LivesMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // Toggle the time limit boolean
            isUnlimitedLives = !isUnlimitedLives;
            ScreenManager.IsUnlimitedLives = isUnlimitedLives;

            SetMenuEntryText();
        }

        /*
        /// <summary>
        /// Event handler for when the Fullscreen menu entry is selected.
        /// </summary>
        void FullscreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            // Toggle fullscreen boolean
            isFullscreen = !isFullscreen;

            if (isFullscreen)
            {
                // Grab the current display mode of the monitor
                DisplayMode dm = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

                // Set our game's resolution to be our monitor's current resolution, toggle fullscreen, and apply changes
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth = dm.Width;
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight = dm.Height;
            }
            else
            {
                // Go back to our windowed mode resolution from when we started the game
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferWidth = ScreenManager.WindowedWidth;
                ScreenManager.GraphicsDeviceManager.PreferredBackBufferHeight = ScreenManager.WindowedHeight;
            }

            // Update the graphics device manager to reflect the changes
            ScreenManager.GraphicsDeviceManager.IsFullScreen = isFullscreen;
            ScreenManager.GraphicsDeviceManager.ApplyChanges();

            // Update the the menu text to reflect the change
            SetMenuEntryText();
        }
        */

        #endregion

    }
}
