using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Whiskey2D.Core
{
    public class RenderManager
    {
        private static RenderManager instance;
        public static RenderManager getInstance()
        {
            if (instance == null)
            {
                instance = new RenderManager();
            }
            return instance;
        }
        
        private Texture2D pixel;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a new RenderManager
        /// </summary>
        private RenderManager()
        {
            
        }


        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
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
            spriteBatch.Begin();

            List<GameObject> allGobs = ObjectManager.getInstance().getAllObjects();
            foreach (GameObject gob in allGobs)
            {
                Sprite spr = gob.Sprite;
                if (spr != null)
                {
                    spriteBatch.Draw(spr.Image, gob.Position, null, Color.White, 0, Vector2.Zero, spr.Scale, SpriteEffects.None, .5f);
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
            if (pixel == null)
            {
                pixel = new Texture2D(this.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<Color>(new Color[] { Color.White });
            }
            return pixel;
        }

   
    }
}
