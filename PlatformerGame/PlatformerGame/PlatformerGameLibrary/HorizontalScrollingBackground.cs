using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGameLibrary
{
    public class HorizontalScrollingBackground
    {

        // The Backgrounds that make up the images to be scrolled across the screen.
        List<Background> backgrounds;

        // The Background at the right end of the chain
        Background rightMostBackground;
        // The Background at the left end of the chain
        Background leftMostBackground;
        
        // The viewing area for drawing the scrolling background images within
        Viewport viewport;

        // The direction to scroll the background images
        public enum ScrollDirection
        {
            Left,
            Right
        }

        public HorizontalScrollingBackground(Viewport viewport) // TODO may need to pass in total level height to account for vertical scrolling. scale to level height size not viewport.
        {
            backgrounds = new List<Background>();
            rightMostBackground = null;
            leftMostBackground = null;
            this.viewport = viewport;
        }

        public void LoadContent(ContentManager contentManager)
        {
            // Clear the Sprites currently stored as the left and right ends of the chain
            rightMostBackground = null;
            leftMostBackground = null;

            // The total width of all the sprites in the chain
            float totalWidth = 0;

            // Cycle through all of the Background sprites that have been added
            // and load their content and position them.
            foreach (Background background in backgrounds)
            {
                // Load the Background's content and apply its scale.
                // The scale is calculated by figuring out how far the sprite 
                // needs to be stretched to make it fill the height of the viewport.
                background.LoadContent(contentManager, background.AssetName);
                background.Scale = viewport.Height / background.Size.Height;

                // If the Background sprite is the first in line, then rightMostBackground will be null.
                if (rightMostBackground == null)
                {
                    // Position the first Background sprite in line at the (0,0) position
                    background.Position = new Vector2(viewport.X, viewport.Y);
                    leftMostBackground = background;
                }
                else
                {
                    // Position the Background after the last Background in line
                    background.Position = new Vector2(rightMostBackground.Position.X + rightMostBackground.Size.Width, viewport.Y);
                }

                // Set the sprite as the last one in line
                rightMostBackground = background;

                // Increment the width of all the sprites combined in the chain
                totalWidth += background.Size.Width;
            }

            // If the width of all the Backgrounds in the chain does not fill the twice the Viewport width
            // then we need to cycle through the images over and over until we have added
            // enough background images to fill the twice the width. 
            int curIndex = 0;
            if (backgrounds.Count > 0)
            {
                while (totalWidth < viewport.Width * 2)
                {
                    // Add another background image to the chain
                    Background background = new Background();
                    background.AssetName = backgrounds[curIndex].AssetName;
                    background.LoadContent(contentManager, background.AssetName);
                    background.Scale = viewport.Height / background.Size.Height;
                    background.Position = new Vector2(rightMostBackground.Position.X + rightMostBackground.Size.Width, viewport.Y);
                    backgrounds.Add(background);
                    rightMostBackground = background;

                    // Add the new background image's width to the total width of the chain
                    totalWidth += background.Size.Width;

                    // Move to the next image in the background images.
                    // If we've moved to the end of the indexes, start over.
                    curIndex += 1;
                    if (curIndex > backgrounds.Count - 1)
                    {
                        curIndex = 0;
                    }
                }
            }
        }

        // Adds a background sprite to be scrolled through the screen
        public void AddBackground(string assetName)
        {
            Background background = new Background();
            background.AssetName = assetName;

            backgrounds.Add(background);
        }

        // Update the position of the background images
        public void Update(GameTime gameTime, int speed, ScrollDirection direction)
        {          
            if (direction == ScrollDirection.Left)
            {
                // Check to see if any of the Background sprites have moved off the screen to the left.
                // if they have, then move them to the right of the chain of scrolling backgrounds.
                foreach (Background background in backgrounds)
                {
                    if (background.Position.X < viewport.X - background.Size.Width)
                    {
                        background.Position = new Vector2(rightMostBackground.Position.X + rightMostBackground.Size.Width, viewport.Y); // TODO change viewport.Y to background.pos.y?
                        rightMostBackground = background;
                    }
                }
            }
            else if (direction == ScrollDirection.Right)
            {
                // Check to see if any of the background images have moved off the screen to the right.
                // If they have, then move them to the left of the chain of scrolling backgrounds.
                foreach (Background background in backgrounds)
                {
                    if (background.Position.X > viewport.X + viewport.Width)
                    {
                        background.Position = new Vector2(leftMostBackground.Position.X - leftMostBackground.Size.Width, viewport.Y);
                        leftMostBackground = background;
                    }
                }
            }

            // Set the Direction based on movement to the left or right that was passed in
            Vector2 directionVector = Vector2.Zero;
            if (direction == ScrollDirection.Left)
            {
                directionVector.X = -1;
            }
            else if (direction == ScrollDirection.Right)
            {
                directionVector.X = 1;
            }
            
            // Update the postions of each of the Background sprites
            foreach (Background background in backgrounds)
            {
                background.Update(gameTime, new Vector2(speed, 0), directionVector);
            }
        }

        // Draw the background images to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Background background in backgrounds)
            {
                background.Draw(spriteBatch);
            }
        }

    }
}


/*
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdvancedScrollingA2DBackground
{
    class HorizontallyScrollingBackground
    {
        //The Sprites that make up the images to be scrolled
        //across the screen.
        List<Sprite> mBackgroundSprites;
        
        //The Sprite at the right end of the chain
        Sprite mRightMostSprite;
        //The Sprite at the left end of the chain
        Sprite mLeftMostSprite;
        
        //The viewing area for drawing the Scrolling background images within
        Viewport mViewport;

        //The Direction to scroll the background images
        public enum HorizontalScrollDirection
        {
            Left,
            Right
        }

        public HorizontallyScrollingBackground(Viewport theViewport)
        {
            mBackgroundSprites = new List<Sprite>();
            mRightMostSprite = null;
            mLeftMostSprite = null;
            mViewport = theViewport;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            //Clear the Sprites currently stored as the left and right ends of the chain
            mRightMostSprite = null;
            mLeftMostSprite = null;

            //The total width of all the sprites in the chain
            float aWidth = 0;

            //Cycle through all of the Background sprites that have been added
            //and load their content and position them.
            foreach (Sprite aBackgroundSprite in mBackgroundSprites)
            {
                //Load the sprite's content and apply it's scale, the scale is calculate by figuring
                //out how far the sprite needs to be stretech to make it fill the height of the viewport
                aBackgroundSprite.LoadContent(theContentManager, aBackgroundSprite.AssetName);
                aBackgroundSprite.Scale = mViewport.Height / aBackgroundSprite.Size.Height;

                //If the Background sprite is the first in line, then mLastInLine will be null.
                if (mRightMostSprite == null)
                {
                    //Position the first Background sprite in line at the (0,0) position
                    aBackgroundSprite.Position = new Vector2(mViewport.X, mViewport.Y);
                    mLeftMostSprite = aBackgroundSprite;
                }
                else
                {
                    //Position the sprite after the last sprite in line
                    aBackgroundSprite.Position = new Vector2(mRightMostSprite.Position.X + mRightMostSprite.Size.Width, mViewport.Y);
                }

                //Set the sprite as the last one in line
                mRightMostSprite = aBackgroundSprite;

                //Increment the width of all the sprites combined in the chain
                aWidth += aBackgroundSprite.Size.Width;
            }

            //If the Width of all the sprites in the chain does not fill the twice the Viewport width
            //then we need to cycle through the images over and over until we have added
            //enough background images to fill the twice the width. 
            int aIndex = 0;
            if (mBackgroundSprites.Count > 0 && aWidth < mViewport.Width * 2)
            {
                do
                {
                    //Add another background image to the chain
                    Sprite aBackgroundSprite = new Sprite();
                    aBackgroundSprite.AssetName = mBackgroundSprites[aIndex].AssetName;
                    aBackgroundSprite.LoadContent(theContentManager, aBackgroundSprite.AssetName);
                    aBackgroundSprite.Scale = mViewport.Height / aBackgroundSprite.Size.Height;
                    aBackgroundSprite.Position = new Vector2(mRightMostSprite.Position.X + mRightMostSprite.Size.Width, mViewport.Y);
                    mBackgroundSprites.Add(aBackgroundSprite);
                    mRightMostSprite = aBackgroundSprite;

                    //Add the new background Image's width to the total width of the chain
                    aWidth += aBackgroundSprite.Size.Width;

                    //Move to the next image in the background images
                    //If we've moved to the end of the indexes, start over
                    aIndex += 1;
                    if (aIndex > mBackgroundSprites.Count - 1)
                    {
                        aIndex = 0;
                    }

                } while (aWidth < mViewport.Width * 2);
            }
        }

        //Adds a background sprite to be scrolled through the screen
        public void AddBackground(string theAssetName)
        {
            Sprite aBackgroundSprite = new Sprite();
            aBackgroundSprite.AssetName = theAssetName;

            mBackgroundSprites.Add(aBackgroundSprite);
        }

        //Update the posotin of the background images
        public void Update(GameTime theGameTime, int theSpeed, HorizontalScrollDirection theDirection)
        {          
            if (theDirection == HorizontalScrollDirection.Left)
            {
                //Check to see if any of the Background sprites have moved off the screen
                //if they have, then move them to the right of the chain of scrolling backgrounds
                foreach (Sprite aBackgroundSprite in mBackgroundSprites)
                {
                    if (aBackgroundSprite.Position.X < mViewport.X - aBackgroundSprite.Size.Width)
                    {
                        aBackgroundSprite.Position = new Vector2(mRightMostSprite.Position.X + mRightMostSprite.Size.Width, mViewport.Y);
                        mRightMostSprite = aBackgroundSprite;
                    }
                }
            }
            else if (theDirection == HorizontalScrollDirection.Right)
            {
                //Check to see if any of the background images have moved off the screen
                //if they have, then move them to the left of the chain of scrolling backgrounds
                foreach (Sprite aBackgroundSprite in mBackgroundSprites)
                {
                    if (aBackgroundSprite.Position.X > mViewport.X + mViewport.Width)
                    {
                        aBackgroundSprite.Position = new Vector2(mLeftMostSprite.Position.X - mLeftMostSprite.Size.Width, mViewport.Y);
                        mLeftMostSprite = aBackgroundSprite;
                    }
                }
            }

            //Set the Direction based on movement to the left or right that was passed in
            Vector2 aDirection = Vector2.Zero;
            if (theDirection == HorizontalScrollDirection.Left)
            {
                aDirection.X = -1;
            }
            else if (theDirection == HorizontalScrollDirection.Right)
            {
                aDirection.X = 1;
            }
            
            //Update the postions of each of the Background sprites
            foreach (Sprite aBackgroundSprite in mBackgroundSprites)
            {
                aBackgroundSprite.Update(theGameTime, new Vector2(theSpeed, 0), aDirection);
            }
        }

        //Draw the background images to the screen
        public void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Sprite aBackgroundSprite in mBackgroundSprites)
            {
                aBackgroundSprite.Draw(theSpriteBatch);
            }
        }

    }
}

*/