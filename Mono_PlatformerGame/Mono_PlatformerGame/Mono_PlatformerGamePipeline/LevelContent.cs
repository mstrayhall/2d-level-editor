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
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endregion

using Tile = Mono_PlatformerGameLibrary.Tile;

namespace Mono_PlatformerGamePipeline
{

    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Level, Mono_PlatformerGameLibrary")] //TODO
    [ContentTypeWriter()]
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

    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Tile, Mono_PlatformerGameLibrary")] //TODO
    public class TileContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public ExternalReference<Texture2DContent> Texture;

    }

    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.TileMap, Mono_PlatformerGameLibrary")] //TODO
    public class TileMapContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Vector2 Position;

    }

}
