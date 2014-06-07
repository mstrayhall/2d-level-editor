#region File Description
/* LevelProcessor.cs
 * 
 * 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Audio;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
#endregion

using TInput = Mono_PlatformerGamePipeline.LevelContent;
using TOutput = Mono_PlatformerGamePipeline.LevelContent;

namespace Mono_PlatformerGamePipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// </summary>
    [ContentProcessor(DisplayName = "LevelProcessor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            try
            {
                // Build the background texture
                if (input.Background != null)
                {
                    input.Background = context.BuildAsset<Texture2DContent, Texture2DContent>(input.Background, null);
                }

                //Build the level music
                if (null != input.LevelSong)
                {
                    ExternalReference<AudioContent> audio = new ExternalReference<AudioContent>(input.LevelSong.Filename);
                    input.LevelSong = context.BuildAsset<AudioContent, SongContent>(audio, "SongProcessor");
                    audio = null;
                }

                // Cycle through all LevelTilesContents contained in LevelContent
                foreach (TileContent tile in input.TileTypes)
                {
                    // Build the texture associated with each tile so it can be included in the .xnb file
                    // to be created by the Content Pipeline.
                    if (!File.Exists(tile.Texture.Filename))
                    {
                        throw new Exception();
                    }
                    else tile.Texture = context.BuildAsset<Texture2DContent, Texture2DContent>(tile.Texture, null);
                }
            }
            catch (Exception)
            {

            }
            return input;
        }
    }
}