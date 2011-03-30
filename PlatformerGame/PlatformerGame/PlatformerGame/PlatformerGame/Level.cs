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

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }
        GraphicsDevice graphicsDevice;

        #endregion


        #region Initialization

        public Level(string path, IServiceProvider services, GraphicsDevice graphicsDevice)
        {
            if (content == null)
            {
                content = new ContentManager(services, "Content");
            }

            this.graphicsDevice = graphicsDevice;

            timeRemaining = TimeSpan.FromMinutes(1);

            // Load the tiles
            LoadTiles(path);

            // Load the background(s)
            layers = new Texture2D[1];
            layers[0] = content.Load<Texture2D>("background");


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
                for (int x = 0; x < Width; ++x)
                {
                    char tileType = lines[y][x];
                    tiles[x, y] = LoadTileType(tileType, x, y);
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

        private Tile LoadTileType(char tileType, int x, int y)
        {
            // TODO: This will need to handle loading of all tile types, loading each type appropriately

            switch (tileType)
            {
                // Blank space
                case '.':
                    return new Tile(null, TileCollision.Passable, false);

                // Platform
                case '-':
                    return LoadTile("Platform", TileCollision.Platform, false);

                // Player 1
                case '1':
                    return LoadStartTile(x, y);

                // Exit
                case 'X':
                    return LoadExitTile(x, y);

                // Solid block
                case '#':
                    return LoadTile("Block", TileCollision.Impassable, false);

                // Error for unsupported tile found
                default:
                    throw new Exception("Invalid tile character in level");
            }
        }

        private Tile LoadTile(string name, TileCollision collision, bool isDamage)
        {
            // TODO: for multiple tilesets, add in "Tiles/" + the tileset name we're using..add this variable
            return new Tile(content.Load<Texture2D>(name), collision, isDamage);
        }

        private Tile LoadStartTile(int x, int y)
        {
            if (Player != null)
            {
                throw new Exception("A level may not have multiple players");
            }

            startPos = RectangleExtensions.GetBottomCenter(GetBounds(x, y));
            player = new Player(this, startPos, "Player");

            return new Tile(null, TileCollision.Passable, false);
        }

        private Tile LoadExitTile(int x, int y)
        {
            if (exitPos != InvalidPosition)
            {
                throw new Exception("A level may not have multiple exits");
            }

            exitPos = GetBounds(x, y).Center;

            return LoadTile("Exit", TileCollision.Passable, false);
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
            if (!Player.IsAlive || TimeRemaining == TimeSpan.Zero)
            {

            }
            else if (ReachedExit)
            {

            }
            else
            {
                timeRemaining -= gameTime.ElapsedGameTime;

                player.Update(gameTime);

                UpdateEnemies(gameTime);

                // Check if the player fell down a pit
                if (Player.BoundingRectangle.Top >= Height * Tile.Height)
                {
                    PlayerDeath(null);
                }

                // Check if they reached the exit
                if (player.IsAlive && player.BoundingRectangle.Contains(exitPos))
                {
                    ExitReached();
                }
            }

            if (timeRemaining < TimeSpan.Zero)
            {
                timeRemaining = TimeSpan.Zero;
            }
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);

                // TODO: see if an enemy hits a player

            }
        }

        public void PlayerDeath(Enemy killer)
        {
            player.Die(killer);
        }

        private void ExitReached()
        {
            player.BeatLevel();

            reachedExit = true;
        }

        public void StartNewLife()
        {
            Player.Reset(startPos);
        }

        #endregion


        #region Draw

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Texture2D layer in layers)
            {
                //spriteBatch.Draw(layer, Vector2.Zero, Color.White);
                spriteBatch.Draw(layer, graphicsDevice.Viewport.Bounds, Color.White);
            }

            DrawTiles(spriteBatch);

            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // If there is a visible tile here
                    Texture2D tex = tiles[x, y].Texture;
                    if (tex != null)
                    {
                        // Draw it
                        Vector2 pos = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(tex, pos, Color.White);
                    }
                }
            }
        }

        #endregion
    }
}
