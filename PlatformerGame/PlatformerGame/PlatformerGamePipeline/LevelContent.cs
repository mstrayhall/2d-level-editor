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
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
#endregion

using Tile = PlatformerGameLibrary.Tile;

namespace PlatformerGamePipeline
{

    [ContentSerializerRuntimeType("PlatformerGame.Level, PlatformerGame")]
    public class LevelContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer(Optional=true)]
        public ExternalReference<Texture2DContent> Background;

        [ContentSerializer]
        public TileContent[] TileTypes;

        [ContentSerializer]
        public TileMapContent[] TileMap;
        
    }

    [ContentSerializerRuntimeType("PlatformerGame.Level, PlatformerGame")]
    public class TileContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public ExternalReference<Texture2DContent> Texture;

    }

    [ContentSerializerRuntimeType("PlatformerGame.Level, PlatformerGame")]
    public class TileMapContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public Vector2 Position;

    }

}
