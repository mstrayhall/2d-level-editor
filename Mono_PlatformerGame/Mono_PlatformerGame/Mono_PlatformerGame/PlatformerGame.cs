#region File Description
/* PlatformerGame.cs
 * 
 * Basically starts running the game by setting up the screen manager.
 * 
 * Screen manager and screen code basic functionality from create.msdn.com.
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Mono_PlatformerGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PlatformerGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public PlatformerGame()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1088; //1280
            graphics.PreferredBackBufferHeight = 672; //704

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize the screen manager
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            // Activate the first screen
            screenManager.AddScreen(new MainMenuScreen(), null);

            // Set the ScreenManager's graphics properties to be used throughout the game
            screenManager.GraphicsDeviceManager = graphics;
            screenManager.WindowedWidth = graphics.PreferredBackBufferWidth;
            screenManager.WindowedHeight = graphics.PreferredBackBufferHeight;
            screenManager.IsTimeLimit = true;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);
        }
    }
}
