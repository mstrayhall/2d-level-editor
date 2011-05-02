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
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
#endregion

namespace PlatformerGame
{
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
                
        bool isFullscreen;
        MenuEntry fullscreenMenuEntry;
        MenuEntry backMenuEntry;

        #endregion

        #region Initialization

        public OptionsMenuScreen()
            : base("Options")
        {
            
            // Create our menu entries
            fullscreenMenuEntry = new MenuEntry(String.Empty);
            backMenuEntry = new MenuEntry("Back");

            SetMenuEntryText();

            // Hook up menu event handlers
            fullscreenMenuEntry.Selected += FullscreenMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            // Add entries to the menu
            MenuEntries.Add(fullscreenMenuEntry);
            MenuEntries.Add(backMenuEntry);

            TransitionOnTime = TimeSpan.FromSeconds(0.3);
            TransitionOffTime = TimeSpan.FromSeconds(0.3);

        }

        public override void LoadContent()
        {
            // Grab the current fullscreen state
            isFullscreen = ScreenManager.GraphicsDeviceManager.IsFullScreen;
            
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
            fullscreenMenuEntry.Text = "Fullscreen: " + (isFullscreen ? "Enabled" : "Disabled");
        }

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

        #endregion

    }
}
