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
        private Texture2D pixel;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a new RenderManager
        /// </summary>
        public RenderManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = new SpriteBatch(this.graphicsDevice);
        }


        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init()
        {

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
            //fill in render code.
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
