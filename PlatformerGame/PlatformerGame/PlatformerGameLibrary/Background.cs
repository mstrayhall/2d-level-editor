using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGameLibrary
{
    public class Background
    {
        //The current position of the Background
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the Background
        private Texture2D backgroundTexture;

        //The asset name for the Sprite's Texture
        public string AssetName;

        //The Size of the Sprite (with scale applied)
        public Rectangle Size;

        //The amount to increase/decrease the size of the original sprite. When
        //modified throught he property, the Size of the sprite is recalculated
        //with the new scale applied.
        private float mScale = 1.0f;
        public float Scale
        {
            get 
            { 
                return mScale; 
            }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(backgroundTexture.Width * Scale), (int)(backgroundTexture.Height * Scale));
            }
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager contentManager, string assetName)
        {
            backgroundTexture = contentManager.Load<Texture2D>(assetName);
            AssetName = assetName;
            Size = new Rectangle(0, 0, (int)(backgroundTexture.Width * Scale), (int)(backgroundTexture.Height * Scale));
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, Position,
                new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
