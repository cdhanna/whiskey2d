using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.MonoHelp
{
    using XnaColor = Microsoft.Xna.Framework.Color;

    public class EditorRenderManager : RenderManager
    {

        public GraphicsDevice GraphicsDevice { get; private set; }
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;

        private Sprite alwaysOnSprite;

        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            alwaysOnSprite = new Sprite("selection.png", Vector.One, Vector.Zero, .5f, XnaColor.White, 0);
            alwaysOnSprite = new Sprite(WhiskeyControl.Renderer, WhiskeyControl.Resources, alwaysOnSprite);
            alwaysOnSprite.getImage();
            alwaysOnSprite.Center();
            alwaysOnSprite.Scale *= .5f;
        }

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        public void close()
        {
            this.spriteBatch.Dispose();
        }

        /// <summary>
        /// Renders the Game
        /// </summary>
        public void render(List<GameObject> descs)
        {
           
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            foreach (GameObject gob in descs)
            {
                Sprite spr = gob.Sprite;
                if (spr != null)
                {
                    
                    spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth/2);
                }
            }

            spriteBatch.End();
        }

        public void render(List<InstanceDescriptor> descs)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);

            foreach (InstanceDescriptor gob in descs)
            {
                Sprite spr = gob.Sprite;
                spr.setRender(this);
                spr.setResources(WhiskeyControl.Resources);
                if (spr != null)
                {
                    Vector2 alwaysOnPos = gob.Position + gob.Bounds.Size / 2;
                    spriteBatch.Draw(alwaysOnSprite.getImage(), alwaysOnPos, null, alwaysOnSprite.Color, alwaysOnSprite.Rotation, alwaysOnSprite.Offset, alwaysOnSprite.Scale, SpriteEffects.None, (spr.Depth / 2) - .01f);
                    spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth / 2);
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Renders the HUD
        /// </summary>
        public void renderHud()
        {
            //TODO fix this
        }


        /// <summary>
        /// Gets a pixel image
        /// </summary>
        /// <returns></returns>
        public Texture2D getPixel()
        {
            if (pixel == null)
            {
                pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<XnaColor>(new XnaColor[] { XnaColor.White });
            }
            return pixel;
        }




        public void render()
        {
            //do nothing
        }
    }
}
