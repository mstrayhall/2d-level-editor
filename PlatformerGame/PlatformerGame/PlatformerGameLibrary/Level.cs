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

namespace PlatformerGameLibrary
{
    public class Level : IDisposable
    {
        #region Fields and Properties

        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Texture2D Background;

        // Keep track of all the types of tiles in this level
        [ContentSerializer]
        public Tile[] TileTypes;

        [ContentSerializer]
        public Vector2 MapSize;

        // Store the locations and tile type for every tile location
        [ContentSerializer]
        public TileMap[] TileArray;

        // 2D tilemap of tiles in the level
        Tile[,] tiles;
        // Various layers that make up the level
        Texture2D[] layers;

        // Player, enemies, and items in the level
        [ContentSerializerIgnore]
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        Player player;

        [ContentSerializerIgnore]
        List<Enemy> enemies = new List<Enemy>();


        // Key positions in the level
        Vector2 startPos;
        Point exitPos = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1, -1);

         // Level completion properties
        [ContentSerializerIgnore]
        public bool ReachedExit
        {
            get { return reachedExit; }
        }
        bool reachedExit;

        [ContentSerializerIgnore]
        public TimeSpan TimeRemaining
        {
            get { return timeRemaining; }
        }
        TimeSpan timeRemaining;

        [ContentSerializerIgnore]
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        [ContentSerializerIgnore]
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }
        GraphicsDevice graphicsDevice;

        #endregion


        #region Initialization

        public void Initialize(GraphicsDevice device, IServiceProvider services)
        {
            if (content == null)
            {
                content = new ContentManager(services, "Content");
            }

            graphicsDevice = device;

            timeRemaining = TimeSpan.FromMinutes(1);

            // Load the background(s)
            if (Background != null)
            {
                layers = new Texture2D[1];
                layers[0] = Background;
            }
            else layers = new Texture2D[0];

            // Load the tiles
            LoadTiles();
        }

        /// <summary>
        /// TODO: MODIFY TO LOAD BASED ON XML
        /// </summary>
        /// <param name="path"></param>
        private void LoadTiles()
        {
            int height = (int)MapSize.X;
            int width = (int)MapSize.Y;

            // Create the tile grid
            tiles = new Tile[width, height];

            // Loop over every tile position and load each tile
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    TileMap curTile = TileArray[y * (width) + x];
                    tiles[x, y] = LoadTileType(curTile);
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

        private Tile LoadTileType(TileMap curTile)
        {
            // TODO: This will need to handle loading of all tile types, loading each type appropriately

            int y = (int)curTile.Position.X;
            int x = (int)curTile.Position.Y;

            switch (curTile.Name)
            {
                // Blank space
                case "Blank Tile":
                    return new Tile(null, TileCollision.Passable, false);

                // Player
                case "Player":
                    return LoadStartTile(x, y);

                //-------------- PLATFORMS --------------//
                // Solid block
                case "Solid Platform":
                    return LoadTile("Block", TileCollision.Impassable, false);

                // Platform
                case "Passthrough Platform":
                    return LoadTile("Platform", TileCollision.Platform, false);

                // Exit
                case "End Level Platform":
                    return LoadExitTile(x, y);

                //-------------- ENEMIES --------------//
                //
                case "Stationary Enemy":
                    return LoadTile(curTile.Name, TileCollision.Passable, true);

                //
                case "Walking Enemy":
                    return LoadTile(curTile.Name, TileCollision.Passable, true);

                //
                case "Jumping Enemy":
                    return LoadTile(curTile.Name, TileCollision.Passable, true);

                //
                case "Tough Enemy":
                    return LoadTile(curTile.Name, TileCollision.Passable, true);

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
                        Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, (int)Tile.Size.X, (int)Tile.Size.Y);
                        spriteBatch.Draw(tex, dest, Color.White);
                    }
                }
            }
        }

        #endregion
    }

    public class TileMap
    {
        #region Fields

        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Vector2 Position;

        // grab texture from tiletype dict in level class
        Texture2D texture;
        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return texture; }
        }

        #endregion

        #region Initialization

        public void Initialize(GraphicsDevice device)
        {

        }

        #endregion
    }
}
