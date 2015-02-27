using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{
    /// <summary>
    /// RenderInfo contains information useful to anything that wants to render
    /// </summary>
    [Serializable]
    public class RenderInfo
    {
        /// <summary>
        /// Gets the SpriteBatch that is being used for the rendering process
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// Gets the Camera Transform that is being used for the rendering process
        /// </summary>
        public Matrix Transform { get; private set; }

        /// <summary>
        /// Gets the RenderManager that is being used for the rendering process
        /// </summary>
        public RenderManager Renderer { get; private set; }

        /// <summary>
        /// Gets the ResourceManager that is being used for the rendering process
        /// </summary>
        public ResourceManager Resources { get; private set; }

        /// <summary>
        /// Gets the GraphicsDevice that is being used for the rendering process
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Create a RenderInfo 
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="transform"></param>
        /// <param name="renderer"></param>
        /// <param name="resources"></param>
        public RenderInfo(GraphicsDevice graphicsDevice ,SpriteBatch spriteBatch, Matrix transform, RenderManager renderer, ResourceManager resources)
        {
            SpriteBatch = spriteBatch;
            Transform = transform;
            Renderer = renderer;
            Resources = resources;
            GraphicsDevice = graphicsDevice;
        }

    }
}
