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
    class ControlsMenuScreen : MenuScreen
    {
        #region Fields

        // TODO for custom user keys, create event handlers for each entry as commented out below
        // TODO prompt user to input a key for each entry

        // Game controls
        MenuEntry walkMenuEntry;
        MenuEntry runMenuEntry;
        MenuEntry jumpMenuEntry;
        MenuEntry pauseMenuEntry;

        // Menu controls
        MenuEntry selectMenuEntry;
        MenuEntry backMenuEntry;

        MenuEntry backScreenMenuEntry;

        #endregion

        #region Initialization

        public ControlsMenuScreen()
            : base("Controls")
        {
            // Create our menu entries
            walkMenuEntry = new MenuEntry(String.Empty);
            runMenuEntry = new MenuEntry(String.Empty);
            jumpMenuEntry = new MenuEntry(String.Empty);
            pauseMenuEntry = new MenuEntry(String.Empty);
            selectMenuEntry = new MenuEntry(String.Empty);
            backMenuEntry = new MenuEntry(String.Empty);
            backScreenMenuEntry = new MenuEntry("Back");

            SetMenuEntryText();

            // Hook up menu event handlers
            // TODO add event handlers for each key
            backScreenMenuEntry.Selected += OnCancel;

            // Add entries to the menu
            MenuEntries.Add(walkMenuEntry);
            MenuEntries.Add(runMenuEntry);
            MenuEntries.Add(jumpMenuEntry);
            MenuEntries.Add(pauseMenuEntry);
            MenuEntries.Add(selectMenuEntry);
            MenuEntries.Add(backMenuEntry);
            MenuEntries.Add(backScreenMenuEntry);

            TransitionOnTime = TimeSpan.FromSeconds(0.3);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);
        }

        public override void LoadContent()
        {
            // TODO grab default/current keys from screenmanager

            //isLevelTimed = ScreenManager.IsTimeLimit;
            //isMusicOn = ScreenManager.IsMusicOn;
            //isUnlimitedLives = ScreenManager.IsUnlimitedLives;

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
            // TODO update this to use the current values for each entry (create variables to store)
            walkMenuEntry.Text = "Walk: " + "Left/Right Arrow Keys";
            runMenuEntry.Text = "Run: " + "LShift/RShift";
            jumpMenuEntry.Text = "Jump: " + "Space";
            pauseMenuEntry.Text = "Pause: " + "Escape/P/Q";
            selectMenuEntry.Text = "Menu Select: " + "Enter/Space";
            backMenuEntry.Text = "Menu Back: " + "Escape";
        }

        ///// <summary>
        ///// Event handler for when the TimeLimit menu entry is selected.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void TimeLimitMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    // Toggle the time limit boolean
        //    isLevelTimed = !isLevelTimed;
        //    ScreenManager.IsTimeLimit = isLevelTimed;

        //    SetMenuEntryText();
        //}

        ///// <summary>
        ///// Event handler for when the Music menu entry is selected.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void MusicMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    // Toggle the time limit boolean
        //    isMusicOn = !isMusicOn;
        //    ScreenManager.IsMusicOn = isMusicOn;

        //    SetMenuEntryText();
        //}

        ///// <summary>
        ///// Event handler for when the Lives menu entry is selected.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void LivesMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        //{
        //    // Toggle the time limit boolean
        //    isUnlimitedLives = !isUnlimitedLives;
        //    ScreenManager.IsUnlimitedLives = isUnlimitedLives;

        //    SetMenuEntryText();
        //}

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
