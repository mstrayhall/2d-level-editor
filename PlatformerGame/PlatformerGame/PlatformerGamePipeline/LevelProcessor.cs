#region File Description
/* LevelProcessor.cs
 * 
 * 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
#endregion

// TODO: replace these with the processor input and output types.
using TInput = PlatformerGamePipeline.LevelContent;
using TOutput = PlatformerGamePipeline.LevelContent;

namespace PlatformerGamePipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Level Processor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            // Build the background texture
            if (input.Background != null)
            {
                input.Background = context.BuildAsset<Texture2DContent, Texture2DContent>(input.Background, null);
            }
            // Cycle through all LevelTilesContents contained in LevelContent
            foreach (TileContent tile in input.TileTypes)
            {
                // Build the texture associated with each tile so it can be included in the .xnb file
                // to be created by the Content Pipeline.
                tile.Texture = context.BuildAsset<Texture2DContent, Texture2DContent>(tile.Texture, null);
            }
            return input;
        }
    }
}