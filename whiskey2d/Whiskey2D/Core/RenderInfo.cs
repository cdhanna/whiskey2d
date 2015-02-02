using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{
    [Serializable]
    public class RenderInfo
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public Matrix Transform { get; private set; }
        public RenderManager Renderer { get; private set; }
        public ResourceManager Resources { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }
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
