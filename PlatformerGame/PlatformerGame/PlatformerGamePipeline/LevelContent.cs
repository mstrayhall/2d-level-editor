#region File Description
/* LevelContent.cs
 * 
 * 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Audio;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
#endregion

using Tile = PlatformerGameLibrary.Tile;

namespace PlatformerGamePipeline
{

    [ContentSerializerRuntimeType("PlatformerGameLibrary.Level, PlatformerGameLibrary")]
    public class LevelContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer(Optional=true)]
        public ExternalReference<Texture2DContent> Background;

        [ContentSerializer(Optional = true)]
        public ExternalReference<SongContent> LevelSong;

        [ContentSerializer]
        public TileContent[] TileTypes;

        [ContentSerializer]
        public Vector2 MapSize;

        [ContentSerializer]
        public TileMapContent[] TileArray;
        
    }

    [ContentSerializerRuntimeType("PlatformerGameLibrary.Tile, PlatformerGameLibrary")]
    public class TileContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public ExternalReference<Texture2DContent> Texture;

    }

    [ContentSerializerRuntimeType("PlatformerGameLibrary.TileMap, PlatformerGameLibrary")]
    public class TileMapContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Vector2 Position;

    }

}
