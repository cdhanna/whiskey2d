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

namespace WhiskeyEditor.MonoHelp
{
    using XnaColor = Microsoft.Xna.Framework.Color;
    using WhiskeyColor = Whiskey2D.Core.Color;
    public class EditorRenderManager : RenderManager
    {

        public GraphicsDevice GraphicsDevice { get; private set; }
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;

        private Sprite alwaysOnSprite;


        public EditorLevel Level { get; set; }


        private BloomComponent bloomComponent;
        private BloomSettings bloomSettings;
        private RenderTarget2D hudTarget;

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

            Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;


            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);


            //    //draw grid
            //    if (ActiveCamera != null)
            //    {


            //        Vector topLeft =  (Vector.Zero);
            //        Vector botRight =  (new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));

            //        int gridSize = 100;
            //        drawLine(spriteBatch, new Vector(topLeft.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom, new Vector(botRight.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom);
            //        //float currY = topLeft.Y - (topLeft.Y % gridSize);
            //        //while (currY < botRight.Y)
            //        //{
            //        //    drawLine(spriteBatch, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));

            //        //    currY += gridSize;
            //        //}

                    


            //    }

            //spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            
            foreach (GameObject gob in descs)
            {
                Sprite spr = gob.Sprite;
                if (spr != null)
                {
                    spr.draw(spriteBatch, gob.Position);
                    //spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth/2);
                }
            }

            
            


            spriteBatch.End();
        }

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

            

            XnaColor lineColor = XnaColor.Lerp(Level.BackgroundColor.invert(), XnaColor.DarkGray, .8f);
            lineColor = XnaColor.Lerp(lineColor, XnaColor.Transparent, .7f);
            //lineColor = XnaColor.Lerp(lineColor, Level.BackgroundColor, .5f);

            //draw grid
            //int gridSize = 120;

            float currY = GridManager.Instance.snapY(topLeft.Y);//topLeft.Y - (topLeft.Y % gridSize);
            while (currY < botRight.Y)
            {

                XnaColor color = lineColor;
                if (currY == 0)
                    color = Level.BackgroundColor.invert();

                drawLine(spriteBatch, color, .01f, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));
                spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currY, new Vector2(topLeft.X, currY - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);

                currY += GridManager.Instance.GridSizeY;
            }

            float currX = GridManager.Instance.snapX(topLeft.X);//topLeft.X - (topLeft.X % gridSize);
            while (currX < botRight.X)
            {
                XnaColor color = lineColor;
                if (currX == 0)
                    color = Level.BackgroundColor.invert();
                drawLine(spriteBatch, color, .01f, new Vector(currX, topLeft.Y), new Vector(currX, botRight.Y));
                spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currX, new Vector2(currX - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").X, topLeft.Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);
                currX += GridManager.Instance.GridSizeX;
            }

        }

        private void drawLine(SpriteBatch spriteBatch, XnaColor color, float depth, Vector start, Vector end)
        {
            Vector diff = end - start;

            spriteBatch.Draw(getPixel(), start, null, color, (float)Math.Atan2(diff.Y, diff.X), new Vector2(0, .5f) , new Vector(diff.Length(), 2), SpriteEffects.None, depth);
        }

        private void drawBox(SpriteBatch spriteBatch, XnaColor color, float depth, Vector topLeft, Vector botRight)
        {
            drawLine(spriteBatch, color, depth, topLeft, new Vector(botRight.X, topLeft.Y));
            drawLine(spriteBatch, color, depth, topLeft, new Vector(topLeft.X, botRight.Y));
            drawLine(spriteBatch, color, depth, botRight, new Vector(topLeft.X, botRight.Y)); //bottom line
            drawLine(spriteBatch, color, depth, botRight, new Vector(botRight.X, topLeft.Y));
        }


        public void render(List<InstanceDescriptor> descs)
        {
            Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;

           

            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            foreach (InstanceDescriptor gob in descs)
            {
                Sprite spr = gob.Sprite;
                spr.setRender(this);
                spr.setResources(WhiskeyControl.Resources);
                if (spr != null)
                {
                    Vector2 alwaysOnPos = gob.Position;
                    spriteBatch.Draw(alwaysOnSprite.getImage(), alwaysOnPos, null, alwaysOnSprite.Color, alwaysOnSprite.Rotation, alwaysOnSprite.Offset, alwaysOnSprite.Scale, SpriteEffects.None, (spr.Depth / 2) - .01f);
                    //Rectangle destRect = new Rectangle(0, 0, (int)(spr.ImageWidth * spr.Scale.X), (int)(spr.ImageHeight * spr.Scale.Y));
                    gob.Sprite.draw(spriteBatch, gob.Position);
                    //spriteBatch.Draw(spr.getImage(), gob.Position, destRect, spr.Color, spr.Rotation, spr.Offset, Vector.One, SpriteEffects.None, spr.Depth / 2);
                }
            }

            spriteBatch.End();


            
        }


        public void renderAll(List<GameObject> gobs, List<InstanceDescriptor> insts)
        {



            if (bloomComponent == null)
            {
                bloomComponent = new BloomComponent(GraphicsDevice, WhiskeyControl.Content);
                bloomComponent.loadContent();
                bloomSettings = BloomSettings.PresetSettings[0];
                bloomComponent.Settings = bloomSettings;
            }

            if (GraphicsDevice.PresentationParameters.BackBufferHeight != hudTarget.Height || GraphicsDevice.PresentationParameters.BackBufferWidth != hudTarget.Width)
            {
                hudTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            }


            if (Level.BloomSettings != null)
            {
                bloomComponent.Settings = Level.BloomSettings;
            }

            renderHud();

            
            bloomComponent.BeginDraw();

           
            GraphicsDevice.Clear(Level.BackgroundColor);

            render(gobs);
            render(insts);

            


            bloomComponent.draw();



            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(hudTarget, Vector.Zero, XnaColor.White);
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

            if (ActiveCamera != null)
            {

                drawGrid();
                drawBoxes();
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

        
    }
}
