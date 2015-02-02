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
        
      
        private GraphicsDevice GraphicsDevice;
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;
        private BloomComponent bloomComponent;
        private Effect lightEffect;
        private RenderTarget2D lightMapTarget;
        private Texture2D alphaClearTexture;

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
            this.GraphicsDevice = graphicsDevice;
            if (graphicsDevice != null)
            {
                
                bloomComponent = new BloomComponent(graphicsDevice, GameManager.Instance.Content);
                bloomComponent.loadContent();
                this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
                lightEffect = GameManager.Resources.Content.Load<Effect>("light.mgfx");
            }
        }

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        public void close()
        {

        }

        private RenderTarget2D checkRenderTarget(RenderTarget2D target, int width, int height)
        {
            if (target == null || height != target.Height || width != target.Width)
            {
                PresentationParameters pp = GraphicsDevice.PresentationParameters;
                return new RenderTarget2D(GraphicsDevice, width, height, false,
                                                   pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount,
                                                   RenderTargetUsage.DiscardContents);
            }
            else return target;
        }

        /// <summary>
        /// Renders the Game
        /// </summary>
        public void render()
        {
            //ActiveCamera.Position = Vector.Zero;
            Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;

            int bbWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int bbHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            lightMapTarget = checkRenderTarget(lightMapTarget, bbWidth, bbHeight);
            bloomComponent.Settings = GameManager.Level.BloomSettings;


            //DRAW LIGHTMAP
            renderLightMap(GameManager.Objects.getAllObjects().Where(g => g.Active).ToList() );

            bloomComponent.BeginDraw();

            GraphicsDevice.Clear(GameManager.Level.BackgroundColor);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            
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
                        spr.draw(spriteBatch,transform, gob.Position);
                        //spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth / 2);
                        //GameManager.Log.debug(spr.ImagePath);
                    }

                }
            }

            spriteBatch.End();

            bloomComponent.draw();


            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Textures[1] = lightMapTarget;
            // GraphicsDevice.Clear(XnaColor.TransparentBlack);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, GameManager.Level.LightingEnabled ? lightEffect : null);
            spriteBatch.Draw(bloomComponent.OutputTarget, Vector.Zero, XnaColor.White);

            spriteBatch.End();

        }
        private void setShaderValue(EffectParameter parameter, float val)
        {
            if (parameter != null)
                parameter.SetValue(val);
        }
        public void renderLightMap(List<GameObject> gobs)
        {

            ConvexHull.InitializeStaticMembers(GraphicsDevice);

            EffectParameter widthParameter = lightEffect.Parameters["screenWidth"];
            EffectParameter heightParameter = lightEffect.Parameters["screenHeight"];


            setShaderValue(widthParameter, lightMapTarget.Width);
            setShaderValue(heightParameter, lightMapTarget.Height);

            GraphicsDevice.SetRenderTarget(lightMapTarget);
            GraphicsDevice.Clear(GameManager.Level.AmbientLight);

            gobs.ForEach(i =>
            {
                ClearAlphaToOne();


                GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                GraphicsDevice.BlendState = CustomBlendStates.WriteToAlpha;

                //shadowing algorithm taken from http://www.catalinzima.com/xna/samples/dynamic-2d-shadows/
                if (GameManager.Level.ShadowsEnabled)
                {
                    gobs.ForEach(hull =>
                    {
                        if (hull.ShadowCaster)
                        {
                            Convex convex = hull.Bounds.Convex;
                            convex.Origin = hull.Position;
                            convex.Rotation = hull.Sprite.Rotation;
                            ConvexHull convexHull = new ConvexHull(convex, Color.White);
                            convexHull.DrawShadows(i.Light, CameraTransform);
                        }
                    });
                }


                spriteBatch.Begin(SpriteSortMode.Immediate, CustomBlendStates.MultiplyWithAlpha, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, CameraTransform);
                i.renderLight(RenderInfo);
                spriteBatch.End();
            });
            ClearAlphaToOne();
        }


        private void ClearAlphaToOne()
        {
            if (alphaClearTexture == null)
            {
                alphaClearTexture = GameManager.Resources.Content.Load<Texture2D>("AlphaOne");
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, CustomBlendStates.WriteToAlpha);
            spriteBatch.Draw(alphaClearTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
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
            if (pixel == null && GraphicsDevice != null)
            {
                pixel = new Texture2D(this.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<XnaColor>(new XnaColor[] { XnaColor.White });
            }
            return pixel;
        }



        public Camera ActiveCamera
        {
            get
            {
                return GameManager.Instance.ActiveLevel.Camera;
            }
            set { } //do nothing
        }

        public Matrix CameraTransform { get { return ActiveCamera == null ? Matrix.Identity : ActiveCamera.TranformMatrix; } }

        public RenderInfo RenderInfo
        {
            get
            {
                return new RenderInfo(GraphicsDevice, spriteBatch, CameraTransform, this, GameManager.Resources);
            }
        }

    }
}
