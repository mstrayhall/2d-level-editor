#region FileDescription
/* Level.cs
 * 
 * Represents a level in the game.
 * 
 * Uses the XNA Platformer Starter Kit class of the same name as a starting framework.
 * 
 */
#endregion

#region UsingStatements
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace PlatformerGame
{
    class Level : IDisposable
    {
        #region Gameplay Variables

        ContentManager content;

        // 2D tilemap of tiles in the level
        Tile[,] tiles;
        // Various layers that make up the level
        Texture2D[] layers;

        // Player, enemies, and items in the level
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        Player player;

        List<Enemy> enemies = new List<Enemy>();


        // Key positions in the level
        Vector2 startPos;
        Point exitPos = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1, -1);

         // Level completion properties
        public bool ReachedExit
        {
            get { return reachedExit; }
        }
        bool reachedExit;

        public TimeSpan TimeRemaining
        {
            get { return timeRemaining; }
        }
        TimeSpan timeRemaining;
        
        #endregion


        #region Initialization

        public Level(string path)
        {
            if (content == null)
            {
                //content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            timeRemaining = TimeSpan.FromMinutes(1);

            // Load the tiles


            // Load the background(s)

        }

        /// <summary>
        /// TODO: MODIFY TO LOAD BASED ON XML
        /// </summary>
        /// <param name="path"></param>
        private void LoadTiles(string path)
        {
            // Load level and ensure all lines are of the same length
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                    {
                        throw new Exception("Level lines are of different widths");
                    }
                    line = reader.ReadLine();
                }
            }

            // Create the tile grid
            tiles = new Tile[width, lines.Count];

            // Loop over every tile position and load each tile
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    char tileType = lines[x][y];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }

            // Make sure the level has a beginning and an end
            if (Player == null)
            {
                throw new Exception("No starting point");
            }
            if (exitPos == InvalidPosition)
            {
                throw new Exception("No exit in the level");
            }

        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            //This will need to handle loading of all tile types, loading each type appropriately

            return new Tile(null, TileCollision.Passable, false);
        }


        public void Dispose()
        {
            content.Unload();
        }

        #endregion


        #region Bounds and Collision

        public TileCollision GetCollision(int x, int y)
        {
            // Prevent moving outside the left and right level boundaries
            if (x < 0 || x >= Width)
            {
                return TileCollision.Impassable;
            }

            // Allow being above or below the level boundaries
            if (y < 0 || y >= Height)
            {
                return TileCollision.Passable;
            }

            return tiles[x, y].Collision;
        }

        /// <summary>
        /// Gets the bounding rectangle of a tile in world space.
        /// </summary>        
        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
        }

        /// <summary>
        /// Width of level measured in tiles.
        /// </summary>
        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        /// <summary>
        /// Height of the level measured in tiles.
        /// </summary>
        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        #endregion


        #region Update

        public void Update(GameTime gameTime)
        {

        }

        private void UpdateEnemies(GameTime gameTime)
        {

        }

        private void PlayerDeath(Enemy killer)
        {
        }

        private void ExitReached()
        {

        }

        public void StartNewLife()
        {
            Player.Reset(startPos);
        }

        #endregion


        #region Draw

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {

        }

        #endregion
    }
}
