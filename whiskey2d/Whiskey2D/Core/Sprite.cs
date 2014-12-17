using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Whiskey2D.Core.Managers;
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

        /// <summary>
        /// The Image that the Sprite is linked to
        /// </summary>
        //public Texture2D Image { get; set; }
        [NonSerialized]
        private Texture2D image;
        

        [NonSerialized]
        private RenderManager renderer;

        public RenderManager getRenderer()
        {
            if (renderer == null){
                renderer = GameManager.Renderer;
            }

            return renderer;
        }
        public void setRender(RenderManager renderer)
        {
            this.renderer = renderer;
        }


        [NonSerialized]
        private ResourceManager resources;
        public ResourceManager getResources()
        {
            if (resources == null)
            {
                resources = GameManager.Resources;
            }
            return resources;
        }
        public void setResources(ResourceManager resources)
        {
            this.resources = resources;
        }


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
            }

            

            return image;
        }

        //public void setImage(Texture2D image)
        //{
        //    this.image = image;
        //}



        private string imagePath;
        private bool newImage = false;
       // public String ImagePath { get { return this.imagePath; } }
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
        public Vector ImageSize { get { return new Vector(ImageWidth * Scale.X, ImageHeight * Scale.Y); } }

        public float ImageWidth { get { return (image == null) ? 0 : image.Width; } }
        public float ImageHeight { get { return (image == null) ? 0 : image.Height; } }


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
        /// The offset of the sprite. By default, the offset is (0,0), which means all sprites will be drawn from their top-left corner.
        /// The Center() method will calculate the offset so the sprite is drawn from the center of its image.
        /// </summary>
        public Vector Offset { get; set; }

        

        /// <summary>
        /// Creates a sprite with a given Image.
        /// </summary>
        /// <param name="image">A non-null Image</param>
        //public Sprite(Texture2D image)
        //{
        //    this.image = image;
        //    Scale = Vector.One;
        //    Offset = Vector.Zero;
        //    Depth = .5f;
        //    Color = Microsoft.Xna.Framework.Color.White;
        //    Rotation = 0;
        //}

        public Sprite(string imagePath)
        {
           
            ImagePath = imagePath;
            image = getImage();
            Scale = Vector.One;
            Offset = Vector.Zero;
            Depth = .5f;
            Color = Microsoft.Xna.Framework.Color.White;
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
        }

        public Sprite(RenderManager renderer, ResourceManager resources, Sprite other)
        {
            setRender(renderer);
            setResources(resources);
            ImagePath = other.ImagePath;
           // image = other.getImage();
            Scale = other.Scale;
            Offset = other.Offset;
            Depth = other.Depth;
            Color = other.Color;
            Rotation = other.Rotation;
        }

        public Sprite(string imagePath, Vector scale, Vector offset, float depth, Color color, float rotation)
        {
            ImagePath = imagePath;
            image = getImage();
            Scale = scale;
            Offset = offset;
            Depth = depth;
            Color = color;
            Rotation = rotation;
        }

        public Sprite(RenderManager renderer, string imagePath, Vector scale, Vector offset, float depth, Color color, float rotation)
        {
            setRender(renderer);
            ImagePath = imagePath;
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
    }
}
