#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// The actual game
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
using PlatformerGameLibrary;
#endregion

namespace PlatformerGame
{
    class GameplayScreen : GameScreen
    {
        // TODO: MERGE CONTENTS TO USE SAME PATH (or at least know how to navigate to both)
        ContentManager content2;
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont hudFont;

        // The Content Builder and Content Manager are used to load textures
        // at runtime through the Content Pipeline
        ContentBuilder contentBuilder;

        /*
        // health bars
        Texture2D healthBar;

        // audio
        Song levelMusic;

        bool checkpointHit;
         * */

        // generate list of levels so we know where to go next
        // Store the folder containing all the levels for this game
        string baseLevelsPath;
        string tempLevelXNBPath;
        int levelIndex;
        List<string> allLevels;
        Level level;


        // HUD Stuff: lives
        Vector2 livesPos;
        int numLives;

        // Timer
        Vector2 timerPos;
        bool isTimed;

        // Level name
        Vector2 levelNamePos;

        // Overlays
        Texture2D winOverlay;
        Texture2D dieOverlay;
        Vector2 winOverlaySize;
        Vector2 winOverlayPos;
        Vector2 dieOverlaySize;
        Vector2 dieOverlayPos;

        InputManager playerInput;

        public GameplayScreen(int levelIndex, string gameLevelsPath)
        {
            this.levelIndex = levelIndex;
            baseLevelsPath = gameLevelsPath;

            playerInput = new InputManager();
        }

        public override void LoadContent()
        {
            contentBuilder = new ContentBuilder();

            content2 = new ContentManager(ScreenManager.Game.Services, "Content");

            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, contentBuilder.OutputDirectory);
            tempLevelXNBPath = contentBuilder.OutputDirectory;

            spriteBatch = ScreenManager.SpriteBatch;
            //string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            //string relativePath = Path.Combine(assemblyLocation, "../../../../../PlatformerGameContent/Tiles/");
            //string contentPath = Path.GetFullPath(relativePath);

            hudFont = content2.Load<SpriteFont>("hudFont");

            // Given the folder path specified by the user, load all levels in that folder
            allLevels = new List<string>();

            DirectoryInfo di = new DirectoryInfo(baseLevelsPath);
            if (!di.Exists)
            {
                this.ExitScreen();
                return;
            }

            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                // Add all the txt files to the level list
                if (file.Extension == ".xml")
                {
                    allLevels.Add(Path.GetFileNameWithoutExtension(file.FullName));
                }
            }

            CreateLevelXNB();

            // Load the first level from allLevels
            levelIndex--;
            //LoadNextLevel();
            //LoadLevelName("level0.txt");


            // Set the number of lives for the player and the position for lives on the HUD
            numLives = 3;
            livesPos = new Vector2(20, 15);

            // Position for timer on the HUD
            timerPos = new Vector2(200, 15);
            isTimed = ScreenManager.IsTimeLimit;

            // Position for level name on HUD
            levelNamePos = new Vector2(400, 15);
           
            /*
            // timer stuff
            countdownTimer = new TimeSpan(0, 0, 60);

            // music
            levelMusic = content.Load<Song>("HeatVision");
            MediaPlayer.Play(levelMusic);
            MediaPlayer.IsRepeating = true;
            */

            winOverlay = content2.Load<Texture2D>("winOverlay");
            dieOverlay = content2.Load<Texture2D>("dieOverlay");
            winOverlaySize = new Vector2(winOverlay.Width, winOverlay.Height);
            dieOverlaySize = new Vector2(dieOverlay.Width, dieOverlay.Height);
            Vector2 screenCenter = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                                                ScreenManager.GraphicsDevice.Viewport.Height / 2);
            winOverlayPos = screenCenter - (winOverlaySize / 2);
            dieOverlayPos = screenCenter - (dieOverlaySize / 2);

            base.LoadContent();
        }

        private void CreateLevelXNB()
        {
            contentBuilder.Clear();

            foreach (string file in allLevels)
            {
                string levelName = file;
                string levelPath = baseLevelsPath + file + ".xml";
                contentBuilder.Add(levelPath, levelName, null, "LevelProcessor");
            }
            string buildError = contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                // We're good to game.
            }
            else
            {
                // Show the error
                // TODO: SHOW ERROR
                //MessageBox.Show(buildError, "Build Error");
            }
        }

        // Returns true if a new level was successfully loaded
        public bool LoadNextLevel()
        {
            levelIndex++;
            if (allLevels.Count == 0)
            {
                // We never had any levels to load
                ScreenManager.RemoveScreen(this);
                return false;
            }
            else if (levelIndex >= allLevels.Count)
            {
                // That was the last level
                ScreenManager.AddScreen(new WinGameScreen(), null);
                ScreenManager.RemoveScreen(this);
                return false;
            }
            else
            {
                // Unload old level first
                if (level != null) level.Dispose();

                // Load the new level
                string levelName = allLevels[levelIndex];
                string levelPath = Path.Combine(tempLevelXNBPath, levelName);
                string fileNameOnly = Path.GetFileNameWithoutExtension(levelPath);
                level = content.Load<Level>(levelPath);
                level.Initialize(ScreenManager.GraphicsDevice, ScreenManager.Game.Services, isTimed);
                return true;
            }
        }

        public void LoadLevelName(string levelName)
        {
            string levelPath = Path.Combine(tempLevelXNBPath, levelName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(levelPath);

            // Unload old level first
            if (level != null) level.Dispose();

            // Load the new level
            level = content.Load<Level>(levelPath);
            level.Initialize(ScreenManager.GraphicsDevice, ScreenManager.Game.Services, isTimed);
        }

        public void ReloadCurrentLevel()
        {
            // Just build the level again
            // Unload old level first
            if (level != null) level.Dispose();

            // Reload the current level
            string levelName = allLevels[levelIndex];
            string levelPath = Path.Combine(tempLevelXNBPath, levelName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(levelPath);

            level = content.Load<Level>(levelPath);
            level.Initialize(ScreenManager.GraphicsDevice, ScreenManager.Game.Services, isTimed);
        }

        /// <summary>
        /// Processes the user's input, to skip the splash screen.
        /// </summary>
        /// <param name="input">The InputState for the current frame</param>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;

            if (input.IsPauseGame(null))
            {
                // TODO: Actually pause and ask if they want to return to title screen instead of just doing it.
                // TODO: Also, dont decrement the timer if we do this. Need to tell the Level that we're paused.

                this.ExitScreen();
            }

            if (input.IsNewKeyPress(Keys.E, null, out playerIndex))
            {
                level.PlayerDeath(null);
            }

            base.HandleInput(input);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            PlayerIndex playerIndex;
            playerInput.Update(gameTime);

            if (level == null)
            {
                bool haveNewLevel = LoadNextLevel();
                if (!haveNewLevel) return;
            }

            level.Update(gameTime);

            if (level.TimeRemaining == TimeSpan.Zero || !level.Player.IsAlive)
            {
                // if the Player is dead or time ran out, see if they can respawn
                if (numLives >= 0)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(dieOverlay, dieOverlayPos, Color.White);
                    spriteBatch.End();
                    if (playerInput.IsKeyDown(Keys.Space, null, out playerIndex))
                    {
                        numLives--;
                        ReloadCurrentLevel();
                    }
                }
                else
                {
                    ScreenManager.AddScreen(new GameOverScreen(), null);
                    this.ExitScreen();
                }
            }
            else if (level.ReachedExit)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(winOverlay, winOverlayPos, Color.White);
                spriteBatch.End();
                if (playerInput.IsKeyDown(Keys.Space, null, out playerIndex))
                {
                    LoadNextLevel();
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }



        // TODO: Implement
        public void DrawHUD(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(hudFont, "Lives\n" + numLives, livesPos, Color.Black);

            if (isTimed) spriteBatch.DrawString(hudFont, "Time Left\n" + (int)(level.TimeRemaining.TotalSeconds), timerPos, Color.Black);
            else spriteBatch.DrawString(hudFont, "Time Left\n" + "Unlimited", timerPos, Color.Black);

            spriteBatch.DrawString(hudFont, "Level Name\n" + allLevels[levelIndex], levelNamePos, Color.Black);

            spriteBatch.End();
        }



        /// <summary>
        /// Draw the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (level == null)
            {
                bool haveNewLevel = LoadNextLevel();
                if (!haveNewLevel) return;
            }

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            // Clear to Black
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            /*
            //--------Health Bar drawing--------//
            spriteBatch.Begin();

            // draw the health bar border
            spriteBatch.Draw(healthBar, new Rectangle(ScreenManager.GraphicsDevice.Viewport.Width / 4 - healthBar.Width / 2, 25,
                healthBar.Width, 32), new Rectangle(0, 0, healthBar.Width, healthBar.Height), Color.White);

            // draw the health bar inside
            spriteBatch.Draw(healthBar, new Rectangle((ScreenManager.GraphicsDevice.Viewport.Width / 4 - healthBar.Width / 2) + 4, 25 + 5,
                (int)((healthBar.Width - 10) * (raptor.health / 100.0f)) + 2, 32 - 10),
                new Rectangle(8, 10, healthBar.Width - 15, healthBar.Height - 20), Color.Red);

            spriteBatch.End();
            */

            level.Draw(gameTime, spriteBatch);

            DrawHUD(spriteBatch);

            spriteBatch.Begin();

            level.DrawPlayerAndEnemies(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
