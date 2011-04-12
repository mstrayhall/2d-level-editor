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


namespace PlatformerGamePipeline
{

    [ContentSerializerRuntimeType("PlatformerGame.Level, PlatformerGame")]
    public class LevelContent
    {
        [ContentSerializer]
        public string Name;

        [ContentSerializer]
        public ExternalReference<Texture2DContent> Background;

        [ContentSerializer]
        public LevelTilesContent[] Tiles;

        // TODO: somehow include Tile class from PlatformerGame namespace
        [ContentSerializer]
        //public
        
    }

    public class LevelTilesContent
    {
        // data for what kind of tiles we have and their textures
    }
}
