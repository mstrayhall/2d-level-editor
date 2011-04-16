#region FileDescription
/* Tile.cs
 * 
 * Represents a Tile in the level.
 * 
 * Uses the XNA Platformer Starter Kit class of the same name as a starting framework.
 * 
 */
#endregion

#region UsingStatements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace PlatformerGame
{
    public enum TileCollision
    {
        // player can completely pass through
        Passable = 0,

        // player can't pass through at all
        Impassable = 1,

        // player can pass through from any direction except when coming from above
        Platform = 2,
    }

    public struct Tile
    {
        [ContentSerializer]
        public Texture2D Texture;

        [ContentSerializer]
        public TileCollision Collision;

        [ContentSerializerIgnore]
        public bool IsDamage;

        [ContentSerializerIgnore]
        public const int Width = 64;
        [ContentSerializerIgnore]
        public const int Height = 64;

        [ContentSerializerIgnore]
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision, bool damageTile)
        {
            Texture = texture;
            Collision = collision;

            IsDamage = damageTile;
        }
    }

}
