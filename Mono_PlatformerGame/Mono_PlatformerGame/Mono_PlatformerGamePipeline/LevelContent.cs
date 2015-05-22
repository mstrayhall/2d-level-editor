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
using Mono_PlatformerGameLibrary;
#endregion

using Tile = Mono_PlatformerGameLibrary.Tile;

namespace Mono_PlatformerGamePipeline
{

    [ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Level, Mono_PlatformerGameLibrary")] //TODO
    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Level")] //TODO
    //[ContentTypeWriter]
    public class LevelContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer(Optional = true)]
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

    [ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Tile, Mono_PlatformerGameLibrary")] //TODO
    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.Tile")] //TODO
    //[ContentTypeWriter]
    public class TileContent //: ContentTypeWriter<Tile>
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public ExternalReference<Texture2DContent> Texture;

        //public override string GetRuntimeReader(TargetPlatform targetPlatform)
        //{
        //    return typeof(Tile).AssemblyQualifiedName;
        //}

        //protected override void Write(ContentWriter output, Tile value)
        //{
        //    throw new NotImplementedException();
        //}
    }

    [ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.TileMap, Mono_PlatformerGameLibrary")] //TODO
    //[ContentSerializerRuntimeType("Mono_PlatformerGameLibrary.TileMap")] //TODO
    //[ContentTypeWriter]
    public class TileMapContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Vector2 Position;

    }

}
