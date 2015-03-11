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
    using CoreLayer = Whiskey2D.Core.Layer;

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
        private BloomComponent lightBloomComponent;
        private RenderTarget2D hudTarget;
        private RenderTarget2D hudObjectsTarget;
        private Texture2D alphaClearTexture;
        private RenderTarget2D lightMapTarget;
        private RenderTarget2D sceneTarget;
        private RenderTarget2D layerScreenShader;
        private RenderTarget2D currentLayerTarget;

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

        private void drawBoxes()
        {

            XnaColor constantBox = XnaColor.Lerp(Level.BackgroundColor.Inverted, XnaColor.Red, .5f);
            XnaColor screenBox = XnaColor.Lerp(Level.BackgroundColor.Inverted, XnaColor.Blue, .5f);

            Vector topLeft = ActiveCamera.getGameCoordinate(Vector.Zero);
            Vector botRight = ActiveCamera.getGameCoordinate(new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));


            

            //draw screen-box
            int screenWidth = 1280;
            int screenHeight = 720;
            Vector screenSize = new Vector(screenWidth, screenHeight);
            Vector center = (topLeft + botRight) / 2;

            Vector screenTopLeft = center - screenSize / 2;
            Vector screenBotRight = center + screenSize / 2;
            Vector screenBoxSize = screenBotRight - screenTopLeft;


            

            drawBox(spriteBatch, screenBox, .02f,4, screenTopLeft, screenBotRight);
            drawBox(spriteBatch, constantBox, .03f,8, Vector.Zero, Vector.Zero + screenSize);

            if (Level.PreviewHud)
            {
                spriteBatch.Draw(hudObjectsTarget, screenTopLeft, null, XnaColor.White);
            }
        }

        private void drawGrid()
        {
            Vector topLeft = ActiveCamera.getGameCoordinate(Vector.Zero);
            Vector botRight = ActiveCamera.getGameCoordinate(new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));
            Vector size = botRight - topLeft;

            
            float intensity = Level.BackgroundColor.Intensity;
            XnaColor lineColor = Level.BackgroundColor.Inverted;
            if (intensity > .4f && intensity < .6f)
            {
                lineColor = XnaColor.Lerp(lineColor, XnaColor.White,1-  Math.Abs(.5f - intensity));
            }
            

            float currY = GridManager.Instance.snapY(topLeft.Y);//topLeft.Y - (topLeft.Y % gridSize);
            while (currY < botRight.Y)
            {

                XnaColor color = lineColor;
                if (currY == 0)
                    color = Level.BackgroundColor.Inverted;

                drawLine(spriteBatch, color, .01f, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));
                spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currY, new Vector2(topLeft.X, currY - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);

                currY += GridManager.Instance.GridSizeY;
            }

            float currX = GridManager.Instance.snapX(topLeft.X);//topLeft.X - (topLeft.X % gridSize);
            while (currX < botRight.X)
            {
                XnaColor color = lineColor;
                if (currX == 0)
                    color = Level.BackgroundColor.Inverted;
                drawLine(spriteBatch, color, .01f, new Vector(currX, topLeft.Y), new Vector(currX, botRight.Y));
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

            spriteBatch.Draw(getPixel(), start, null, color, (float)Math.Atan2(diff.Y, diff.X), new Vector2(0, .5f) , new Vector(diff.Length, thickness), SpriteEffects.None, depth);
        }

        private void drawBox(SpriteBatch spriteBatch, XnaColor color, float depth,float thickness, Vector topLeft, Vector botRight)
        {
            drawLine(spriteBatch, color, depth, topLeft, new Vector(botRight.X, topLeft.Y), thickness);
            drawLine(spriteBatch, color, depth, topLeft, new Vector(topLeft.X, botRight.Y), thickness);
            drawLine(spriteBatch, color, depth, botRight, new Vector(topLeft.X, botRight.Y), thickness); //bottom line
            drawLine(spriteBatch, color, depth, botRight, new Vector(botRight.X, topLeft.Y), thickness);
        }


        public void render(List<GameObject> descs)
        {

            ObjectController controller = WhiskeyControl.Active.ObjectController;
            if (controller != null)
            {

                GraphicsDevice.SetRenderTarget(layerScreenShader);
                GraphicsDevice.Clear(XnaColor.Transparent);

                foreach (CoreLayer layer in Level.Layers)
                {
                    if (!layer.Visible)
                    {
                        continue;
                    }


                    string shaderMode = layer.ShaderMode;
                    BlendState blendState = BlendModeConverter.getState(layer.BlendMode);
                    if (shaderMode != CoreLayer.DEFAULT_SHADER_MODE && shaderMode != null)
                    {
                        Effect fx = WhiskeyControl.Resources.loadEffect(shaderMode);
                        layer.setEffect(fx);
                    }
                    else 
                    {
                        layer.setEffect(null);
                    }

                    string postShaderMode = layer.PostShaderMode;
                    if (postShaderMode != CoreLayer.DEFAULT_SHADER_MODE && postShaderMode != null)
                    {
                        layer.setPostEffect(WhiskeyControl.Resources.loadEffect(postShaderMode));
                    }
                    else
                    {
                        layer.setPostEffect(null);
                    }

                    if (WhiskeyControl.InputManager != null)
                    {
                        Vector mouse = WhiskeyControl.InputManager.MousePosition;
                        mouse.X /= currentLayerTarget.Width;
                        mouse.Y /= currentLayerTarget.Height;
                        Level.ShaderParameters.setVector(ShaderParameters.PARAM_MOUSE_POS, mouse);

                        Vector3 translation = ActiveCamera.TranformMatrix.Translation;
                        translation.X /= currentLayerTarget.Width;
                        translation.Y /= currentLayerTarget.Height;
                        Level.ShaderParameters.setVector(ShaderParameters.PARAM_CAMERA, new Vector(translation.X, translation.Y));
                        Level.ShaderParameters.setFloat(ShaderParameters.PARAM_CAMERA_ZOOM, ActiveCamera.Zoom);
                    }

                    Effect layerEffect = layer.getEffect();
                    Effect post = layer.getPostEffect();

                    Level.ShaderParameters.updateEffect(layerEffect);
                    Level.ShaderParameters.updateEffect(post);


                    GraphicsDevice.SetRenderTarget(currentLayerTarget);
                    
                    GraphicsDevice.Clear(XnaColor.Transparent);


                    if (layer.ScreenShader)
                    {
                        spriteBatch.Begin(SpriteSortMode.FrontToBack, blendState, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, layerEffect);
                        spriteBatch.Draw(bloomComponent.SceneTarget, Vector.Zero, XnaColor.White);
                        spriteBatch.End();
                    }
                    else
                    {

                        

                        spriteBatch.Begin(SpriteSortMode.FrontToBack, blendState, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, layerEffect, CameraTransform);
                        foreach (GameObject gob in descs.Where(g => (g.Layer == null && layer == Level.DefaultLayer) || layer == g.Layer))
                        {
                            if (gob == controller)//|| gob == controller.Selected)
                                continue;


                           
                            Vector2 alwaysOnPos = gob.Position;
                            spriteBatch.Draw(alwaysOnSprite.getImage(), alwaysOnPos, null, alwaysOnSprite.Color, alwaysOnSprite.Rotation, alwaysOnSprite.Offset, alwaysOnSprite.Scale, SpriteEffects.None, 0);

                            gob.Bounds.draw(RenderInfo, new RenderHints().setColor(Level.BackgroundColorCompliment));
                            gob.renderImage(RenderInfo);

                            
                        }

                        spriteBatch.End();


                       
                        
                    }

                   

                    GraphicsDevice.SetRenderTarget(layerScreenShader);
                        
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, post);
                    spriteBatch.Draw(currentLayerTarget, Vector.Zero, XnaColor.White);
                    spriteBatch.End();
                    

                }
               
                
              
            }
        }

        private RenderTarget2D checkRenderTarget(RenderTarget2D target, int width, int height)
        {
            if (target == null || height != target.Height || width != target.Width)
            {
                PresentationParameters pp = GraphicsDevice.PresentationParameters;
                return new RenderTarget2D(GraphicsDevice, width, height, false,
                                                   pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount,
                                                   RenderTargetUsage.PreserveContents);
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
            if (lightBloomComponent == null)
            {
                lightBloomComponent = new BloomComponent(GraphicsDevice, WhiskeyControl.Content);
                lightBloomComponent.loadContent();
                lightBloomComponent.Settings = BloomSettings.PresetSettings[5];
            }

            //load bloom settings
            if (Level.BloomSettings != null)
            {
                bloomComponent.Settings = Level.BloomSettings;
            }

            if (Level.LightBloomSettings != null)
            {
                lightBloomComponent.Settings = Level.LightBloomSettings;
            }


            //ensure render targets are correctly sized
            hudTarget = checkRenderTarget(hudTarget, bbWidth, bbHeight);
            lightMapTarget = checkRenderTarget(lightMapTarget, bbWidth, bbHeight);
            sceneTarget = checkRenderTarget(sceneTarget, bbWidth, bbHeight);
            hudObjectsTarget = checkRenderTarget(hudObjectsTarget, 1280, 720);
            layerScreenShader = checkRenderTarget(layerScreenShader, bbWidth, bbHeight);
            currentLayerTarget = checkRenderTarget(currentLayerTarget, bbWidth, bbHeight);
            //get game objects, and ui gameobjects
            List<GameObject> uiGameObjects = new List<GameObject>();
            List<GameObject> gobsToRender = new List<GameObject>();
            gobs.ForEach(g => uiGameObjects.Add(g));
            insts.Where(g => !g.HudObject).ToList().ForEach(g => gobsToRender.Add(g));

            //DRAW HUD
            renderHud();
            //draw light radius
            gobsToRender.ForEach( g => {

                if (g.Light.Visible)
                {
                    Convex convex = new Convex(g.Position, 0, VectorSet.Dodecahedren * (g.Light.Radius / 2));
                    convex.render(GraphicsDevice, CameraTransform, new RenderHints().setColor(Level.BackgroundColor.Inverted));
                }

            });
           

            //DRAW GAME OBJECTS 
          
            
           // bloomComponent.BeginDraw();
           
            render(gobsToRender);

            bloomComponent.BeginDraw();
            GraphicsDevice.Clear(Level.BackgroundColor);
           
            spriteBatch.Begin();
            spriteBatch.Draw(layerScreenShader, Vector.Zero, XnaColor.White);
            spriteBatch.End();

            bloomComponent.draw();


            //DRAW LIGHTMAP
            renderLightMap(gobs, insts.Where(g => !g.HudObject).ToList());
            
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
                if (i.Light.Visible)
                {

                    ClearAlphaToOne();


                    GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    GraphicsDevice.BlendState = CustomBlendStates.MultiplyShadows;

                    //shadowing algorithm taken from http://www.catalinzima.com/xna/samples/dynamic-2d-shadows/
                    if (Level.PreviewShadowing)
                    {
                        insts.ForEach(hull =>
                        {
                            if (hull.Shadows.CastsShadows)
                            {
                                Boolean pass = hull != i;
                                if (i.Shadows.SelfShadows)
                                {
                                    pass = true;
                                }
                                
                                if (pass)
                                {
                                    Convex convex = hull.Bounds.Convex;
                                    convex.Origin = hull.Position;
                                    convex.Rotation = hull.Sprite.Rotation;
                                    
                                    ConvexHull convexHull = new ConvexHull(convex, WhiskeyColor.White);
                                    convexHull.DrawShadows(i.Light, CameraTransform, hull.Shadows.IncludeLight, hull.Shadows.Solidness, hull.Shadows.Height);
                                }
                            }
                        });
                    }


                    spriteBatch.Begin(SpriteSortMode.Immediate, CustomBlendStates.MultiplyWithAlpha, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, CameraTransform);
                    i.renderLight(RenderInfo);
                    spriteBatch.End();
                }
            });
            ClearAlphaToOne();

            lightBloomComponent.BeginDraw();
            spriteBatch.Begin();
            spriteBatch.Draw(lightMapTarget, Vector.Zero, null, XnaColor.White);
            spriteBatch.End();
            lightBloomComponent.draw();
            lightMapTarget = lightBloomComponent.OutputTarget;
        }

        private void ClearAlphaToOne()
        {
            if (alphaClearTexture == null)
            {
                alphaClearTexture = WhiskeyControl.Resources.Content.Load<Texture2D>("AlphaOne");
            }

           
            spriteBatch.Begin(SpriteSortMode.Immediate, CustomBlendStates.AlphaOnly);
            spriteBatch.Draw(alphaClearTexture, new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight), WhiskeyColor.White);
            spriteBatch.End();
        }


        /// <summary>
        /// Renders the HUD
        /// </summary>
        public void renderHud()
        {
            GraphicsDevice.SetRenderTarget(hudObjectsTarget);
            GraphicsDevice.Clear(XnaColor.Transparent);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            List<GameObject> hudGobs = Level.getAllObjects().Where(g => g.HudObject).ToList();
            hudGobs.ForEach(h =>
            {
                h.renderImage(RenderInfo);
            });
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(hudTarget);
            GraphicsDevice.Clear(XnaColor.Transparent);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, CameraTransform);
            //spriteBatch.Begin();

            spriteBatch.Draw(hudObjectsTarget, Vector.Zero, null, XnaColor.White);

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

                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.TopLeft, controller.Bounds.TopRight, 6 / Level.Camera.Zoom);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.TopLeft, controller.Bounds.BottomLeft, 6 / Level.Camera.Zoom);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.BottomLeft, controller.Bounds.BottomRight, 6 / Level.Camera.Zoom);
                drawLine(spriteBatch, controller.Sprite.Color, .3f, controller.Bounds.BottomRight, controller.Bounds.TopRight, 6 / Level.Camera.Zoom);


               

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
