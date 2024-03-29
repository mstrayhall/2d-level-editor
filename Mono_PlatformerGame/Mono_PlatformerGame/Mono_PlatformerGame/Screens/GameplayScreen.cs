﻿#region File Description
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mono_PlatformerGameLibrary;
#endregion

namespace Mono_PlatformerGame
{
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager gameOverlayContent;
        ContentManager levelContent;
        SpriteBatch spriteBatch;
        SpriteFont hudFont;

        // The Content Builder and Content Manager are used to load textures
        // at runtime through the Content Pipeline
        ContentBuilder contentBuilder;

        // Audio
        Song levelMusic;
        bool isMusicOn;

        // Generate list of levels so we know where to go next.
        // Store the folder containing all the levels for this game
        string baseLevelsPath;
        string tempLevelXNBPath;
        int levelIndex;
        List<string> allLevels;
        Level level;


        // HUD Stuff: lives
        Vector2 livesPos;
        int numLives;
        bool isUmlimitedLives;

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
        // Paused
        Texture2D pauseOverlay;
        Vector2 pauseOverlaySize;
        Vector2 pauseOverlayPos;
        bool isPaused;

        // Input
        InputManager playerInput;

        // Errors
        bool foundError;
        string errorMessage;

        #endregion

        #region Initialization

        public GameplayScreen(int levelIndex, string gameLevelsPath)
        {
            this.levelIndex = levelIndex;
            baseLevelsPath = gameLevelsPath;

            playerInput = new InputManager();
        }

        public override void LoadContent()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] assembliesToAdd = new string[]
            {
                // Add our Level Specification
                Path.Combine(currentDirectory, "Mono_PlatformerGamePipeline.dll"),
                Path.Combine(currentDirectory, "Mono_PlatformerGameLibrary.dll"),
            };

            contentBuilder = new ContentBuilder(baseLevelsPath, assembliesToAdd);

            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            // This one loads assets for drawing the HUD and overlays
            gameOverlayContent = new ContentManager(ScreenManager.Game.Services, "Content");

            // This one loads levels using the level processor
            levelContent = new ContentManager(ScreenManager.Game.Services, contentBuilder.BaseOutputDirectory);
            tempLevelXNBPath = contentBuilder.BaseOutputDirectory;

            spriteBatch = ScreenManager.SpriteBatch;

            // Errors
            foundError = false;
            errorMessage = "";

            // HUD
            hudFont = gameOverlayContent.Load<SpriteFont>(@"bin\hudFont");

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

            // Create the level
            CreateLevelXNB();

            // Set the number of lives for the player and the position for lives on the HUD
            numLives = allLevels.Count * 3;
            livesPos = new Vector2(20, 15);
            isUmlimitedLives = ScreenManager.IsUnlimitedLives;

            // Position for timer on the HUD
            timerPos = new Vector2(200, 15);
            isTimed = ScreenManager.IsTimeLimit;

            // Position for level name on HUD
            levelNamePos = new Vector2(400, 15);
           
            // Overlay positioning
            winOverlay = gameOverlayContent.Load<Texture2D>(@"bin\winOverlay");
            dieOverlay = gameOverlayContent.Load<Texture2D>(@"bin\dieOverlay");
            winOverlaySize = new Vector2(winOverlay.Width, winOverlay.Height);
            dieOverlaySize = new Vector2(dieOverlay.Width, dieOverlay.Height);
            Vector2 screenCenter = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2,
                                                ScreenManager.GraphicsDevice.Viewport.Height / 2);
            winOverlayPos = screenCenter - (winOverlaySize / 2);
            dieOverlayPos = screenCenter - (dieOverlaySize / 2);

            pauseOverlay = gameOverlayContent.Load<Texture2D>(@"bin\pauseOverlay");
            pauseOverlaySize = new Vector2(pauseOverlay.Width, pauseOverlay.Height);
            pauseOverlayPos = screenCenter - (pauseOverlaySize / 2);
            isPaused = false;

            // Audio
            isMusicOn = ScreenManager.IsMusicOn;
            
            // Load the first level from allLevels -- this should be done last.
            levelIndex--;
            LoadNextLevel();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            contentBuilder.Dispose();

            MediaPlayer.Stop();

            base.UnloadContent();
        }

        private void CreateLevelXNB()
        {
            contentBuilder.Clear();

            string dirPath = Path.Combine(baseLevelsPath, "XNB_Files");
            List<string> dirFileNames = new List<string>();
            if (Directory.Exists(dirPath))
            {
                string[] dirFiles = Directory.GetFiles(dirPath, "*.xnb", SearchOption.AllDirectories);
                foreach (string file in dirFiles)
                {
                    dirFileNames.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
            bool foundXNBFiles = false;
            if (dirFileNames.Count > 0) foundXNBFiles = true;

            foreach (string file in allLevels)
            {
                string levelName = file;
                string levelPath = Path.Combine(baseLevelsPath, file + ".xml");
                if (foundXNBFiles)
                {
                    if (!dirFileNames.Contains(file))
                    {
                        // If we don't have the xnb of this xml file here, go ahead and build it
                        contentBuilder.Add(levelPath, levelName, null, "LevelProcessor");

                        // TODO build mgcb


                    }
                }
                else
                {
                    // If we found no xnb files, just build everything
                    
                    // TODO build mgcb


                    contentBuilder.Add(levelPath, levelName, null, "LevelProcessor");
                }
            }
            
            // TODO build mgcb


            string buildError = contentBuilder.Build();
            if (string.IsNullOrEmpty(buildError))
            {
                // We're good to game.
            }
            else
            {
                errorMessage = "Error building level XNB files.\n"
                                + "All .xml files in the selected directory must be\n"
                                + "in a format compatible with this level editor.";
                foundError = true;
            }
        }

        private void SetLevelMusic()
        {
            try
            {
                // If this level had any music listed to play
                if (level.LevelSong != null && isMusicOn)
                {
                    Song songName = level.LevelSong;
                    // If we dont have any music playing yet 
                    if (levelMusic == null)
                    {
                        levelMusic = songName;
                        MediaPlayer.Play(levelMusic);
                        MediaPlayer.IsRepeating = true;
                    }
                    // If we want to play a different song than the one playing, do so.
                    else if (levelMusic != null)
                    {
                        string curLevelSongName = Path.GetFileNameWithoutExtension(levelMusic.Name);
                        string curPlayingSongName = Path.GetFileNameWithoutExtension(songName.Name);
                        if (curLevelSongName != curPlayingSongName)
                        {
                            levelMusic = songName;
                            MediaPlayer.Play(levelMusic);
                            MediaPlayer.IsRepeating = true;
                        }
                    }
                    // Else just continue playing the same song
                }
                // Else continue playing any music we had playing
            }
            catch (Exception)
            {
                foundError = true;
                errorMessage = "Error loading level music";
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
                try
                {
                    level = levelContent.Load<Level>(fileNameOnly);
                }
                catch (Exception)
                {
                    contentBuilder.Clear();
                    contentBuilder.Add(Path.Combine(baseLevelsPath, levelName + ".xml"), levelName, null, "LevelProcessor");
                    string error = contentBuilder.Build();
                    try
                    {
                        level = levelContent.Load<Level>(levelPath);
                    }
                    catch (Exception e)
                    {
                        foundError = true;
                        errorMessage = "Error loading level.\n"
                                        + "Please check " + levelName + ".xml\n"
                                        + "to make sure all asset paths are correct."
                                        + "\nBuild error: " + error
                                        + "\n\nError: " + e.Message; // TODO test how long this message is...probably wanna stick with just mine.
                    }
                }
                if (!foundError)
                {
                    level.Initialize(ScreenManager.GraphicsDevice, ScreenManager.Game.Services, isTimed);
                    SetLevelMusic();
                    return true;
                }
                else return false;
            }
        }

        public void ReloadCurrentLevel()
        {
            // Just reload the level by setting the level counter back.
            levelIndex--;
            LoadNextLevel();
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Processes the user's input
        /// </summary>
        /// <param name="input">The InputState for the current frame</param>
        public override void HandleInput(InputState input)
        {
            if (level != null)
            {
                // If we don't have any level terminating conditions, we can accept input
                if (!(level.ReachedExit || !level.Player.IsAlive || level.TimeRemaining == TimeSpan.Zero))
                {
                    // TODO move this functionality into its own screen
                    // If the game is paused...
                    if (isPaused)
                    {
                        // If they press pause again, unpause
                        if (input.IsUnpauseGame(null))
                        {
                            PauseGameToggle();
                        }
                        // If they want to exit, then quit
                        else if (input.IsExitGame(null))
                        {
                            this.ExitScreen();
                        }
                    }
                    // If the game is not paused...
                    else
                    {
                        if (input.IsPauseGame(null))
                        {
                            PauseGameToggle();
                        }
                    }
                }
            }

            base.HandleInput(input);
        }


        /// <summary>
        /// Updates pause state and music volume depending on the current pause state
        /// </summary>
        private void PauseGameToggle()
        {
            isPaused = !isPaused;

            if (isMusicOn)
            {
                // Lower music volume if paused, else make it max
                MediaPlayer.Volume = isPaused ? 0.2f : 1.0f;
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (foundError)
            {
                foundError = false; // reset so we dont load multiple error screens
                ScreenManager.AddScreen(new ErrorScreen(errorMessage), null);
                this.ExitScreen();
                return;
            }

            PlayerIndex playerIndex;

            if (!isPaused)
            {
                playerInput.Update(gameTime);

                if (level == null)
                {
                    bool haveNewLevel = LoadNextLevel();
                    if (!haveNewLevel) return;
                }

                level.Update(gameTime);

                if (level.TimeRemaining == TimeSpan.Zero || !level.Player.IsAlive)
                {
                    // if the Player is dead or time ran out, see if they can respawn (and wait for their input)
                    if (playerInput.IsKeyDown(Keys.Space, null, out playerIndex))
                    {
                        if (numLives > 0 || isUmlimitedLives == true)
                        {
                            numLives--;
                            ReloadCurrentLevel();
                        }
                        else
                        {
                            ScreenManager.AddScreen(new GameOverScreen(), null);
                            this.ExitScreen();
                        }
                    }
                }
                else if (level.ReachedExit)
                {
                    // If player reached the exit, pause until they choose to continue
                    if (playerInput.IsKeyDown(Keys.Space, null, out playerIndex))
                    {
                        LoadNextLevel();
                    }
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Draws shadowed strings of information for the player to see.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawHUD(SpriteBatch spriteBatch)
        {
            // Main color for our HUD items
            Color hudColor = new Color(10, 10, 10);

            spriteBatch.Begin();

            // Lives
            if (isUmlimitedLives)
            {
                DrawShadowedString(hudFont, "Lives\n" + "Unlimited", livesPos, hudColor);
            }
            else
            {
                DrawShadowedString(hudFont, "Lives\n" + numLives, livesPos, hudColor);
            }

            // Timer
            if (isTimed)
            {
                DrawShadowedString(hudFont, "Time Left\n" + (int)(level.TimeRemaining.TotalSeconds), timerPos, hudColor);
            }
            else
            {
                DrawShadowedString(hudFont, "Time Left\n" + "Unlimited", timerPos, hudColor);
            }

            // Level name
            DrawShadowedString(hudFont, "Level Name\n" + allLevels[levelIndex], levelNamePos, hudColor);

            spriteBatch.End();
        }


        /// <summary>
        /// Draw a shadowed string -- a message with a white shadow behind it
        /// </summary>
        private void DrawShadowedString(SpriteFont font, string text, Vector2 position, Color color)
        {
            // Draw the shadow part a little up and left in white
            spriteBatch.DrawString(font, text, position - Vector2.One, Color.White);

            // Draw the real message on top in the given color
            spriteBatch.DrawString(font, text, position, color);
        }


        /// <summary>
        /// Draw the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (foundError)
            {
                foundError = false; // reset so we dont load multiple error screens
                ScreenManager.AddScreen(new ErrorScreen(errorMessage), null);
                this.ExitScreen();
                return;
            }

            if (level == null)
            {
                bool haveNewLevel = LoadNextLevel();
                if (!haveNewLevel) return;
            }

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Draw level first
            level.Draw(gameTime, spriteBatch);

            // Draw HUD on top
            DrawHUD(spriteBatch);

            // Draw characters over this
            level.DrawPlayerAndEnemies(gameTime, spriteBatch);

            // Finally draw overlays on very top
            spriteBatch.Begin();
            if (!level.Player.IsAlive || level.TimeRemaining == TimeSpan.Zero)
            {
                spriteBatch.Draw(dieOverlay, dieOverlayPos, Color.White);
            }
            else if (level.ReachedExit)
            {
                spriteBatch.Draw(winOverlay, winOverlayPos, Color.White);
            }
            else if (isPaused)
            {
                spriteBatch.Draw(pauseOverlay, pauseOverlayPos, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}
