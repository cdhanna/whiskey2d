using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Whiskey2D.Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    /// <summary>
    /// A Sprite holds visual data.
    /// </summary>
    [Serializable]
    public class Sprite
    {
        public const string PIXEL = "__PIXEL__";

        [NonSerialized]
        private Texture2D image;
        
        [NonSerialized]
        private RenderManager renderer;

        [NonSerialized]
        private ResourceManager resources;

      

        private string imagePath;
        private bool newImage = false;
        private int rows = 1;
        private int columns = 1;
        private int frame = 0;

        /// <summary>
        /// Retrieve the Renderer the Sprite is using
        /// </summary>
        /// <returns>Some Renderer</returns>
        public RenderManager getRenderer()
        {
            if (renderer == null){
                renderer = GameManager.Renderer;
            }

            return renderer;
        }

        /// <summary>
        /// Sets the Renderer that the Sprite will use
        /// </summary>
        /// <param name="renderer">Some Renderer</param>
        public void setRender(RenderManager renderer)
        {
            this.renderer = renderer;
        }

        /// <summary>
        /// Retrieves the ResourceManager the Sprite is using
        /// </summary>
        /// <returns>Some resourceManager</returns>
        public ResourceManager getResources()
        {
            if (resources == null)
            {
                resources = GameManager.Resources;

              
            }
            return resources;
        }

        /// <summary>
        /// Sets the Resourcemanager the sprite will use
        /// </summary>
        /// <param name="resources">some resourceManager</param>
        public void setResources(ResourceManager resources)
        {
            this.resources = resources;
        }

        /// <summary>
        /// Get the Image that the Sprite is using
        /// </summary>
        /// <returns>An Image</returns>
        public Texture2D getImage()
        {
            if (getRenderer() == null)
            {
                return null;
            }
            if (ImagePath == null)
            {
                ImagePath = PIXEL;
            }

            if (image == null && ImagePath != null || newImage)
            {
                newImage = false;
                if (ImagePath.Equals(PIXEL))
                {
                    
                    image = getRenderer().getPixel();
                }
                else
                {
                    image = getResources().loadImage(ImagePath);
                }

                Center();
            }

           
            return image;
        }

      
        /// <summary>
        /// The path to the Image the Sprite is using
        /// </summary>
        [System.ComponentModel.ReadOnly(true)]
        public String ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                newImage = true;
            }
        }

        /// <summary>
        /// The Vector Scale of the Sprite. By default, the Scale is set to (1, 1), which represents 100% scale.
        /// Setting to the scale to (2, 1) would stretch the sprite to 200% in the X direction, and leave it be in the Y direction.
        /// </summary>
        public Vector Scale { get; set; }

        /// <summary>
        /// A Vector representing the actual size of the Image, including the current Scale settings. 
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector ImageSize { get { return new Vector(ImageWidth * Scale.X, ImageHeight * Scale.Y); } }

        /// <summary>
        /// The width of the Image being used
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ImageWidth { get { return (getImage() == null) ? 0 : getImage().Width; } }

        /// <summary>
        /// The height of the Image being used
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float ImageHeight { get { return (getImage() == null) ? 0 : getImage().Height; } }

        /// <summary>
        /// The rotation the Sprite will be drawn at. The Rotation is in radians. 
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// The Color that the Sprite will be drawn with. 
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The z-depth of the Sprite. If two sprites overlap, the one with the higher z-depth will be drawn on top.
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Gets or Sets the number of Frame-Rows the Sprite has.
        /// </summary>
        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = Math.Max(1, value);
            }
        }

        /// <summary>
        /// Gets or Sets the number of Frame-Columns the Sprite has.
        /// </summary>
        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = Math.Max(1, value);
            }
        }

        /// <summary>
        /// Gets the total number of Frames in the Sprite. FrameCount equals Rows * Columns
        /// </summary>
        public int FrameCount
        {
            get
            {
                return Rows * Columns;
            }
        }

        /// <summary>
        /// Gets or Sets the current displaying frame.
        /// </summary>
        public int Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = MathHelper.Clamp(value, 0, FrameCount - 1);
            }
        }

        /// <summary>
        /// Gets the width of one frame in the image. This is unscaled.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float FrameWidth
        {
            get
            {
                return ImageWidth / Columns;
            }
        }

        /// <summary>
        /// Gets the height of one frame in the image. This is unscaled.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public float FrameHeight
        {
            get
            {
                return ImageHeight / Rows;
            }
        }

        /// <summary>
        /// Gets the size of one frame in the image. This is unscaled.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector FrameSize
        {
            get
            {
                return new Vector(FrameWidth, FrameHeight);
            }
        }

        /// <summary>
        /// Gets the height of one frame in the image. This is scaled
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector FrameSizeScaled
        {
            get
            {
                return new Vector(FrameWidth * Scale.X, FrameHeight * Scale.Y);
            }
        }

        /// <summary>
        /// Gets the offset of one frame in the image.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector FrameOffset
        {
            get
            {
                return new Vector(FrameWidth, FrameHeight) / 2;
            }
        }

        /// <summary>
        /// Gets the offset of one frame in the image. This is scaled
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector FrameOffsetScaled
        {
            get
            {
                return new Vector(FrameWidth * Scale.X, FrameHeight * Scale.Y) / 2;
            }
        }

        /// <summary>
        /// Gets a Rectangle representing the current frame of the sprite, in ImageCoordinates
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Rectangle FrameRectangle
        {
            get
            {
                float x = (Frame % Columns) * FrameWidth;
                float y = (Frame / Columns) * FrameHeight;
                return new Rectangle((int)x, (int)y, (int)FrameWidth, (int)FrameHeight);
            }
        }

        /// <summary>
        /// The offset of the sprite. By default, the offset is (0,0), which means all sprites will be drawn from their top-left corner.
        /// The Center() method will calculate the offset so the sprite is drawn from the center of its image.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector Offset { get; private set; }

        /// <summary>
        /// Gets the offset of the sprite, but scaled
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Vector ScaledOffset { get { return new Vector(Offset.X * Scale.X, Offset.Y * Scale.Y); } }

        /// <summary>
        /// Get or Set if the Sprite tiles itself when the scale is not {1, 1}
        /// </summary>
        public bool Tiled { get; set; }

        /// <summary>
        /// Get or Set if the Sprite is visible
        /// </summary>
        public bool Visible { get; set; }
  

        public Sprite(string imagePath)
        {
           
            ImagePath = imagePath;
            image = getImage();
            Scale = Vector.One;
            Offset = Vector.Zero;
            Depth = .5f;
            Color = Microsoft.Xna.Framework.Color.White;
            Visible = true;
            Rotation = 0;
        }
        public Sprite()
        {
            ImagePath = PIXEL;
            image = getImage();
            Scale = Vector.One;
            Offset = Vector.Zero;
            Depth = .5f;
            Color = Microsoft.Xna.Framework.Color.White;
            Rotation = 0;
            Visible = true;
        }

        public Sprite(RenderManager renderer)
        {
            setRender(renderer);
            
            ImagePath = PIXEL;
            image = getImage();
            Scale = Vector.One;
            Offset = Vector.Zero;
            Depth = .5f;
            Color = Microsoft.Xna.Framework.Color.White;
            Rotation = 0;
            Visible = true;
        }


        public Sprite( Sprite other) 
            : this(other.getRenderer(), other.getResources(), other)
        { }

        public Sprite(RenderManager renderer, ResourceManager resources, Sprite other)
        {
            setRender(renderer);
            setResources(resources);
            ImagePath = other.ImagePath;
            Scale = other.Scale;
            Offset = other.Offset;
            Depth = other.Depth;
            Color = other.Color;
            Tiled = other.Tiled;
            Rotation = other.Rotation;
            Visible = other.Visible;

        }

        public Sprite(string imagePath, Vector scale, Vector offset, float depth, Color color, float rotation)
        {
            ImagePath = imagePath;
            image = getImage();
            Scale = scale;
            Offset = offset;
            Depth = depth;
            Color = color;
            Visible = true;
            Rotation = rotation;
        }

        public Sprite(RenderManager renderer, string imagePath, Vector scale, Vector offset, float depth, Color color, float rotation)
        {
            setRender(renderer);
            ImagePath = imagePath;
            Visible = true;
            image = getImage();
            Scale = scale;
            Offset = offset;
            Depth = depth;
            Color = color;
            Rotation = rotation;
        }

        /// <summary>
        /// Centers the offset of a sprite. 
        /// </summary>
        public void Center()
        {
            Offset = new Vector(ImageWidth, ImageHeight) / 2;
        }


        /// <summary>
        /// Draw the Sprite
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to draw with</param>
        /// <param name="transform">The camera transform</param>
        /// <param name="position">the position to draw the sprite</param>
        public void draw(SpriteBatch spriteBatch, Matrix transform, Vector position)
        {
            if (!Visible)
                return;

            
            if (Tiled)
            {
                
                Rectangle srcRect = new Rectangle(0, 0, (int)(ImageWidth * Scale.X), (int)(ImageHeight * Scale.Y));
                spriteBatch.Draw(getImage(), position, srcRect, Color, Rotation, new Vector(Offset.X * (Scale.X ), Offset.Y * (Scale.Y )), Vector.One, SpriteEffects.None, Depth / 2);
            }
            else
            {
                float destRectWidth = FrameSize.X * Scale.X;
                float destRectHeight = FrameSize.Y * Scale.Y;
                Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)destRectWidth, (int)destRectHeight);

                Vector off = FrameOffset;

              

                spriteBatch.Draw(getImage(), destRect, FrameRectangle, Color, Rotation, off, SpriteEffects.None, Depth / 2);
            }
        }

        /// <summary>
        /// Create an Animation from the start frame, to the end frame
        /// </summary>
        /// <param name="startFrame">Some starting frame</param>
        /// <param name="endFrame">Some ending frame</param>
        /// <returns>A new animation</returns>
        public Animation createAnimation(int startFrame, int endFrame)
        {
            return createAnimation(startFrame, endFrame, 7, true);
        }

        /// <summary>
        /// Create an Animation from the start frame, to the end frame
        /// </summary>
        /// <param name="startFrame">Some starting frame</param>
        /// <param name="endFrame">Some ending frame</param>
        /// <param name="speed">How many ticks go by before the next frame of animation is played</param>
        /// <param name="looped">If the animation will loop automatically or not</param>
        /// <returns>A new animation</returns>
        public Animation createAnimation(int startFrame, int endFrame, int speed, bool looped)
        {

            Animation anim = new Animation(this);
            anim.Looped = looped;
            
            anim.Speed = speed;
            anim.StartFrame = MathHelper.Clamp(startFrame, 0, FrameCount);
            anim.EndFrame = MathHelper.Clamp(endFrame, 0, FrameCount);
            anim.CurrentFrame = anim.StartFrame;

            return anim;
        }


        internal Animation ActiveAnimation { get; set; }

        internal List<Animation> recentAnims = new List<Animation>();

        /// <summary>
        /// Updates the Sprite
        /// </summary>
        public void update()
        {
            recentAnims.Clear();
        }

    }

    

    

}
