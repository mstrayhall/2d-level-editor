#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// The atual game
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
#endregion

namespace PlatformerGame
{
    class GameplayScreen : GameScreen
    {
        ContentManager content;
        SpriteBatch spriteBatch;

        /*
        // health bars
        Texture2D healthBar;

        // audio
        Song levelMusic;

        // timer
        TimeSpan countdownTimer;
        Vector2 timerPos;
        SpriteFont timerFont;

        // scrolling backgrounds
        float scrollSpeed;

        float scrollBoundaryLeft;
        float scrollBoundaryRight;
        float scrollBoundaryTop;
        float scrollBoundaryBottom;
        bool scrolledX;
        bool scrolledY;

        Level curLevel;
        int curLives;
        Vector2 livesPos;

        
        bool checkpointHit;
         * */

        // generate list of levels so we know where to go next
        int levelIndex;
        List<string> allLevels;
        Level level;

        int numLives;

        public GameplayScreen(int levelIndex)
        {
            this.levelIndex = levelIndex;

        }

        public override void LoadContent()
        {
            // If we don't yet have a reference to the content manager, 
            // grab one from the game
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            spriteBatch = ScreenManager.SpriteBatch;


            // TODO: search for all levels in this directory, add all?
            // Or do the starter kit method. But I think I wanna add all so I can do a level select screen.
            allLevels = new List<string>();
            string levelPath = "Levels/level0.txt";
            allLevels.Add(levelPath);

            levelPath = "Levels/level1.txt";
            allLevels.Add(levelPath);

            LoadLevelName("Levels/level0.txt");

            numLives = 3;
            
           
            /*
            // level stuff
            curLevel = new Level(allLevels[levelIndex]);
            curLevel.LoadLevel(content, ScreenManager.GraphicsDevice);
            scrollBoundaryLeft = -(ScreenManager.GraphicsDevice.Viewport.Width / 5);
            scrollBoundaryRight = (ScreenManager.GraphicsDevice.Viewport.Width / 5);
            scrollBoundaryTop = (ScreenManager.GraphicsDevice.Viewport.Height / 5);
            scrollBoundaryBottom= -(ScreenManager.GraphicsDevice.Viewport.Height / 5);
            scrolledX = false;
            scrolledY = false;

            // timer stuff
            countdownTimer = new TimeSpan(0, 0, 60);
            timerPos = new Vector2((ScreenManager.GraphicsDevice.Viewport.Width / 2) - 30, 24);
            timerFont = content.Load<SpriteFont>("gameFont");

            // lives stuff
            livesPos = new Vector2((3 * ScreenManager.GraphicsDevice.Viewport.Width / 4), 24);

            scrollSpeed = raptor.runSpeed;

            // music
            levelMusic = content.Load<Song>("HeatVision");
            MediaPlayer.Play(levelMusic);
            MediaPlayer.IsRepeating = true;
            */


            base.LoadContent();
        }

        public void LoadNextLevel()
        {
            levelIndex++;
            if (levelIndex >= allLevels.Count)
            {
                // That was the last level
                ScreenManager.AddScreen(new WinGameScreen(), null);
                this.ExitScreen();
            }
            else
            {
                // Unload old level first
                if (level != null) level.Dispose();

                // Load the new level
                string levelPath = allLevels[levelIndex];
                levelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/" + levelPath);
                level = new Level(levelPath, ScreenManager.Game.Services);
            }
        }

        public void LoadLevelName(string levelName)
        {
            string levelPath = levelName;
            levelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/" + levelPath);

            // Unload old level first
            if (level != null) level.Dispose();

            // Load the new level
            level = new Level(levelPath, ScreenManager.Game.Services);
        }

        public void ReloadCurrentLevel()
        {
            // Just build the level again
            // Unload old level first
            if (level != null) level.Dispose();

            // Reload the current level
            string levelPath = allLevels[levelIndex];
            levelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/" + levelPath);
            level = new Level(levelPath, ScreenManager.Game.Services);
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
            level.Update(gameTime);

            if (level.TimeRemaining == TimeSpan.Zero || !level.Player.IsAlive)
            {
                // if the Player is dead or time ran out, see if they can respawn
                numLives--;
                if (numLives >= 0)
                {
                    ReloadCurrentLevel();

                }
                else
                {
                    ScreenManager.AddScreen(new GameOverScreen(), null);
                    this.ExitScreen();
                }
            }
            else if (level.ReachedExit)
            {
                LoadNextLevel();
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// Draw the game.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            // Clear to white
            ScreenManager.GraphicsDevice.Clear(Color.White);

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
            
            spriteBatch.Begin();

            level.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
