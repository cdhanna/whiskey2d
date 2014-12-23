using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core.Hud;

namespace Whiskey2D.Core.Managers.Impl
{

    using XnaColor = Microsoft.Xna.Framework.Color;

    /// <summary>
    /// Renders a Whiskey Game
    /// </summary>
    public class DefaultRenderManager : RenderManager
    {
        //private static DefaultRenderManager instance;

        ///// <summary>
        ///// Retrieves the RenderManager
        ///// </summary>
        ///// <returns>The RenderManager</returns>
        //public static DefaultRenderManager getInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new DefaultRenderManager();
        //    }
        //    return instance;
        //}
        
      
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;
        /// <summary>
        /// Creates a new RenderManager
        /// </summary>
        public DefaultRenderManager()
        {
            ActiveCamera = new Camera();
        }

      

        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            if (graphicsDevice != null)
                this.spriteBatch = new SpriteBatch(this.graphicsDevice);
        }

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        public void close()
        {

        }

        /// <summary>
        /// Renders the Game
        /// </summary>
        public void render()
        {
           
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            List<GameObject> allGobs = GameManager.Objects.getAllObjects();
            foreach (GameObject gob in allGobs.Where(g => g.Active) )
            {
                Sprite spr = gob.Sprite;
                if (spr != null)
                {

                    //if (spr.getImage() == GameManager.Renderer.getPixel())
                    //{
                    //    spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth / 2);
                    //}



                    {
                        spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth / 2);
                        //GameManager.Log.debug(spr.ImagePath);
                    }

                }
            }

            spriteBatch.End();


            

        }

        /// <summary>
        /// Renders the HUD
        /// </summary>
        public void renderHud()
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            List<Box> boxes = HudManager.getInstance().getAllBoxes();
            foreach (Box box in boxes)
            {
                if (box.Visible)
                {
                    spriteBatch.Draw(getPixel(), box.Position, null, box.Color, 0, Vector.Zero, box.Size, SpriteEffects.None,box.Depth);
                }
            }

            List<TextLine> lines = HudManager.getInstance().getAllTextLines();
            foreach (TextLine line in lines)
            {
                if (line.Visible)
                {
                    spriteBatch.DrawString(line.Font, line.Text, line.Position, line.Color,0, Vector.Zero, line.Size, SpriteEffects.None, .91f);
                }
            }

            spriteBatch.End();
        }


        /// <summary>
        /// Gets a pixel image
        /// </summary>
        /// <returns></returns>
        public Texture2D getPixel()
        {
            if (pixel == null && graphicsDevice != null)
            {
                pixel = new Texture2D(this.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<XnaColor>(new XnaColor[] { XnaColor.White });
            }
            return pixel;
        }



        public Camera ActiveCamera
        {
            get;
            set;
        }
    }
}
