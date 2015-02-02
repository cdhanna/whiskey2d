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
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.EditorObjects;
using Whiskey2D.Core.Managers.Impl;


namespace WhiskeyEditor.MonoHelp
{
    using XnaColor = Microsoft.Xna.Framework.Color;
    using WhiskeyColor = Whiskey2D.Core.Color;
    public class EditorRenderManager : RenderManager
    {

        public GraphicsDevice GraphicsDevice { get; private set; }
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;
        private Effect lightEffect;

        private Sprite alwaysOnSprite;


        public EditorLevel Level { get; set; }


        private BloomComponent bloomComponent;
        private BloomSettings bloomSettings;
        private RenderTarget2D hudTarget;

        private Texture2D alphaClearTexture;
        private RenderTarget2D lightMapTarget;
        private RenderTarget2D sceneTarget;

        //public Camera Camera { get; set; }

        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
          //  Camera = new Camera();
            ActiveCamera = new Camera();
            alwaysOnSprite = new Sprite("selection.png", Vector.One, Vector.Zero, .5f, XnaColor.White, 0);
            alwaysOnSprite = new Sprite(WhiskeyControl.Renderer, WhiskeyControl.Resources, alwaysOnSprite);
            alwaysOnSprite.getImage();
            alwaysOnSprite.Center();
            alwaysOnSprite.Scale *= .5f;


            hudTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            lightEffect = WhiskeyControl.Resources.Content.Load<Effect>("light.mgfx");

            
        }

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        public void close()
        {
            this.spriteBatch.Dispose();
        }

        ///// <summary>
        ///// Renders the Game
        ///// </summary>
        //public void render(List<GameObject> descs)
        //{

        //    Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;


        //    //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);


        //    //    //draw grid
        //    //    if (ActiveCamera != null)
        //    //    {


        //    //        Vector topLeft =  (Vector.Zero);
        //    //        Vector botRight =  (new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));

        //    //        int gridSize = 100;
        //    //        drawLine(spriteBatch, new Vector(topLeft.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom, new Vector(botRight.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom);
        //    //        //float currY = topLeft.Y - (topLeft.Y % gridSize);
        //    //        //while (currY < botRight.Y)
        //    //        //{
        //    //        //    drawLine(spriteBatch, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));

        //    //        //    currY += gridSize;
        //    //        //}

                    


        //    //    }

        //    //spriteBatch.End();


        //    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            
        //    foreach (GameObject gob in descs)
        //    {
        //        Sprite spr = gob.Sprite;
        //        if (spr != null)
        //        {
        //            spr.draw(spriteBatch, gob.Position);
        //            //spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth/2);
        //        }
        //    }

            
            


        //    spriteBatch.End();
        //}

        private void drawBoxes()
        {

            XnaColor constantBox = XnaColor.Lerp(Level.BackgroundColor.invert(), XnaColor.Red, .5f);
            XnaColor screenBox = XnaColor.Lerp(Level.BackgroundColor.invert(), XnaColor.Blue, .5f);

            Vector topLeft = ActiveCamera.getGameCoordinate(Vector.Zero);
            Vector botRight = ActiveCamera.getGameCoordinate(new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));


            //draw screen-box
            int screenWidth = 1280;
            int screenHeight = 720;
            Vector screenSize = new Vector(screenWidth, screenHeight);
            Vector center = (topLeft + botRight) / 2;

            Vector screenTopLeft = center - screenSize / 2;
            Vector screenBotRight = center + screenSize / 2;


            drawBox(spriteBatch, screenBox, .02f, screenTopLeft, screenBotRight);
            drawBox(spriteBatch, constantBox, .03f, Vector.Zero, Vector.Zero + screenSize);

        }

        private void drawGrid()
        {
            Vector topLeft = ActiveCamera.getGameCoordinate(Vector.Zero);
            Vector botRight = ActiveCamera.getGameCoordinate(new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));


            float intensity = Level.BackgroundColor.intensity();
            XnaColor lineColor = Level.BackgroundColor.invert();
            if (intensity > .4f && intensity < .6f)
            {
                lineColor = XnaColor.Lerp(lineColor, XnaColor.White,1-  Math.Abs(.5f - intensity));
            }
            //XnaColor lineColor = new XnaColor(32, 32, 32);
            //if (intensity < .5f)
            //{
            //    lineColor = new XnaColor(232, 232, 232);
            //}

            
           // lineColor = XnaColor.Lerp(lineColor, XnaColor.Transparent, .7f);
            
            
            
            
            //lineColor = XnaColor.Lerp(lineColor, Level.BackgroundColor, .5f);

            //draw grid
            //int gridSize = 120;

            float currY = GridManager.Instance.snapY(topLeft.Y);//topLeft.Y - (topLeft.Y % gridSize);
            while (currY < botRight.Y)
            {

                XnaColor color = lineColor;
                if (currY == 0)
                    color = Level.BackgroundColor.invert();

                drawLine(spriteBatch, color, .3f, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));
                spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currY, new Vector2(topLeft.X, currY - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);

                currY += GridManager.Instance.GridSizeY;
            }

            float currX = GridManager.Instance.snapX(topLeft.X);//topLeft.X - (topLeft.X % gridSize);
            while (currX < botRight.X)
            {
                XnaColor color = lineColor;
                if (currX == 0)
                    color = Level.BackgroundColor.invert();
                drawLine(spriteBatch, color, .3f, new Vector(currX, topLeft.Y), new Vector(currX, botRight.Y));
                spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currX, new Vector2(currX - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").X, topLeft.Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);
                currX += GridManager.Instance.GridSizeX;
            }

        }

        private void drawLine(SpriteBatch spriteBatch, XnaColor color, float depth, Vector start, Vector end)
        {
            drawLine(spriteBatch, color, depth, start, end, 2.0f);
        }
        private void drawLine(SpriteBatch spriteBatch, XnaColor color, float depth, Vector start, Vector end, float thickness)
        {
            Vector diff = end - start;

            spriteBatch.Draw(getPixel(), start, null, color, (float)Math.Atan2(diff.Y, diff.X), new Vector2(0, .5f) , new Vector(diff.Length(), thickness), SpriteEffects.None, depth);
        }

        private void drawBox(SpriteBatch spriteBatch, XnaColor color, float depth, Vector topLeft, Vector botRight)
        {
            drawLine(spriteBatch, color, depth, topLeft, new Vector(botRight.X, topLeft.Y));
            drawLine(spriteBatch, color, depth, topLeft, new Vector(topLeft.X, botRight.Y));
            drawLine(spriteBatch, color, depth, botRight, new Vector(topLeft.X, botRight.Y)); //bottom line
            drawLine(spriteBatch, color, depth, botRight, new Vector(botRight.X, topLeft.Y));
        }


        public void render(List<GameObject> descs)
        {

            ObjectController controller = WhiskeyControl.Active.ObjectController;
            if (controller != null)
            {

              //  List<GameObject> uiGobs = new List<GameObject>();

                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, CameraTransform);
                foreach (GameObject gob in descs)
                {
                    if (gob == controller )//|| gob == controller.Selected)
                        continue;


                    //if (gob is ObjectControlPoint)
                    //{
                    //    uiGobs.Add(gob);
                    //    continue;
                    //}

                    

                    //Sprite spr = gob.Sprite;
                    //spr.setRender(this);
                    //spr.setResources(WhiskeyControl.Resources);


                    Boolean shouldDraw = true;

                    if (gob is InstanceDescriptor)
                    {
                        shouldDraw = ((InstanceDescriptor)gob).Layer.Visible;
                    }


                    if (shouldDraw)
                    {
                        //if (spr != null)
                        {
                            Vector2 alwaysOnPos = gob.Position;
                            spriteBatch.Draw(alwaysOnSprite.getImage(), alwaysOnPos, null, alwaysOnSprite.Color, alwaysOnSprite.Rotation, alwaysOnSprite.Offset, alwaysOnSprite.Scale, SpriteEffects.None, 0);
                           
                            gob.Bounds.draw(RenderInfo, new RenderHints().setColor(Level.BackgroundColorCompliment));
                            gob.renderImage(RenderInfo);

                            //gob.Sprite.draw(spriteBatch,CameraTransform, gob.Position);
                        }
                    }
                }

                spriteBatch.End();

              
                
              
            }
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

        public void renderAll(List<GameObject> gobs, List<InstanceDescriptor> insts)
        {
            //get width and height of window
            int bbWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int bbHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

            //initialize bloom component
            if (bloomComponent == null)
            {
                bloomComponent = new BloomComponent(GraphicsDevice, WhiskeyControl.Content);
                bloomComponent.loadContent();
                bloomSettings = BloomSettings.PresetSettings[0];
                bloomComponent.Settings = bloomSettings;
            }

            //load bloom settings
            if (Level.BloomSettings != null)
            {
                bloomComponent.Settings = Level.BloomSettings;
            }

            //ensure render targets are correctly sized
            hudTarget = checkRenderTarget(hudTarget, bbWidth, bbHeight);
            lightMapTarget = checkRenderTarget(lightMapTarget, bbWidth, bbHeight);
            sceneTarget = checkRenderTarget(sceneTarget, bbWidth, bbHeight);

            //get game objects, and ui gameobjects
            List<GameObject> uiGameObjects = new List<GameObject>();
            List<GameObject> gobsToRender = new List<GameObject>();
            gobs.ForEach(g => uiGameObjects.Add(g));
            insts.ForEach(g => gobsToRender.Add(g));

            //DRAW HUD
            renderHud();
            //draw light radius
            gobsToRender.ForEach( g => {

                if (g.Light.Visible)
                {
                    Convex convex = new Convex(g.Position, 0, VectorSet.Dodecahedren * (g.Light.Radius / 2));
                    convex.render(GraphicsDevice, CameraTransform, new RenderHints().setColor(Level.BackgroundColor.invert()));
                }

            });
           

            //DRAW GAME OBJECTS 
            bloomComponent.BeginDraw();
            GraphicsDevice.Clear(Level.BackgroundColor);
            render(gobsToRender);
            bloomComponent.draw();


            //DRAW LIGHTMAP
            renderLightMap(gobs, insts);
            
            //DRAW SCENE WITH LIGHTMAP
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Textures[1] = lightMapTarget;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, Level.PreviewLighting ? lightEffect : null);
            spriteBatch.Draw(bloomComponent.OutputTarget, Vector.Zero, XnaColor.White);
       
            spriteBatch.End();

            //DRAW HUD
            spriteBatch.Begin();
            spriteBatch.Draw(hudTarget, Vector.Zero, XnaColor.White);
            spriteBatch.End();
        }

        private void setShaderValue(EffectParameter parameter, float val)
        {
            if (parameter != null)
                parameter.SetValue(val);
        }

        public void renderLightMap(List<GameObject> gobs, List<InstanceDescriptor> insts)
        {
            ConvexHull.InitializeStaticMembers(GraphicsDevice);

            EffectParameter widthParameter = lightEffect.Parameters["screenWidth"];
            EffectParameter heightParameter = lightEffect.Parameters["screenHeight"];

           
            setShaderValue(widthParameter, lightMapTarget.Width);
            setShaderValue(heightParameter, lightMapTarget.Height);

            GraphicsDevice.SetRenderTarget(lightMapTarget);
            GraphicsDevice.Clear(Level.AmbientLight);

            insts.ForEach(i =>
            {
                ClearAlphaToOne();


                GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                GraphicsDevice.BlendState = CustomBlendStates.WriteToAlpha;

                //shadowing algorithm taken from http://www.catalinzima.com/xna/samples/dynamic-2d-shadows/
                if (Level.PreviewShadowing)
                {
                    insts.ForEach(hull =>
                    {
                        if (hull.ShadowCaster)
                        {
                            Convex convex = hull.Bounds.Convex;
                            convex.Origin = hull.Position;
                            convex.Rotation = hull.Sprite.Rotation;
                            ConvexHull convexHull = new ConvexHull(convex, WhiskeyColor.White);
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
                alphaClearTexture = WhiskeyControl.Resources.Content.Load<Texture2D>("AlphaOne");
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, CustomBlendStates.WriteToAlpha);
            spriteBatch.Draw(alphaClearTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), WhiskeyColor.White);
            spriteBatch.End();
        }


        /// <summary>
        /// Renders the HUD
        /// </summary>
        public void renderHud()
        {
            GraphicsDevice.SetRenderTarget(hudTarget);
            GraphicsDevice.Clear(XnaColor.Transparent);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, CameraTransform);
            //spriteBatch.Begin();
            ObjectController controller = WhiskeyControl.Active.ObjectController;
            List<GameObject> uiGobs = new List<GameObject>();
            uiGobs.Add(controller.ControlPointBot);
            uiGobs.Add(controller.ControlPointTop);
            uiGobs.Add(controller.ControlPointLeft);
            uiGobs.Add(controller.ControlPointRight);
            uiGobs.Add(controller.ControlPointLeftBot);
            uiGobs.Add(controller.ControlPointLeftTop);
            uiGobs.Add(controller.ControlPointRightBot);
            uiGobs.Add(controller.ControlPointRightTop);
            uiGobs.Add(controller.ControlPointLightRadius);



            if (ActiveCamera != null)
            {

                drawGrid();
                drawBoxes();
            }

            if (controller.Selected != null)
            {
               // spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, CameraTransform);

                uiGobs.ForEach(g =>
                {
                    g.Sprite.draw(spriteBatch, CameraTransform, g.Position);
                });

                //controller.Bounds.draw(RenderInfo, new RenderHints().setColor(controller.Sprite.Color).setPrimitiveType(PrimitiveType.LineStrip));

                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.TopLeft, controller.Bounds.TopRight, 6);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.TopLeft, controller.Bounds.BottomLeft, 6);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.BottomLeft, controller.Bounds.BottomRight, 6);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.BottomRight, controller.Bounds.TopRight, 6);


               

                Sprite spr = controller.Selected.Sprite;
                spr.setRender(this);
                spr.setResources(WhiskeyControl.Resources);
                //controller.Selected.Sprite.draw(spriteBatch,CameraTransform, controller.Selected.Position);
                //spriteBatch.End();
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
                pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<XnaColor>(new XnaColor[] { XnaColor.White });
            }
            return pixel;
        }




        public void render()
        {
            //do nothing
           
        }


        public Matrix CameraTransform { get { return ActiveCamera == null ? Matrix.Identity : ActiveCamera.TranformMatrix; } }

        public Camera ActiveCamera
        {
            get { return WhiskeyControl.ActiveCamera; }
            set { }
        }

        public RenderInfo RenderInfo
        {
            get
            {
                return new RenderInfo(GraphicsDevice, spriteBatch, CameraTransform, this, WhiskeyControl.Resources);
            }
        }
        
    }
}
