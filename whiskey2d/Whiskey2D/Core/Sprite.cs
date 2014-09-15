using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    /// <summary>
    /// A Sprite holds visual data.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The Image that the Sprite is linked to
        /// </summary>
        public Texture2D Image { get; set; }

        /// <summary>
        /// The Vector Scale of the Sprite. By default, the Scale is set to (1, 1), which represents 100% scale.
        /// Setting to the scale to (2, 1) would stretch the sprite to 200% in the X direction, and leave it be in the Y direction.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// A Vector representing the actual size of the Image, including the current Scale settings. 
        /// </summary>
        public Vector2 ImageSize { get { return new Vector2(Image.Width * Scale.X, Image.Height * Scale.Y); } }
        
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
        public Vector2 Offset { get; set; }

        

        /// <summary>
        /// Creates a sprite with a given Image.
        /// </summary>
        /// <param name="image">A non-null Image</param>
        public Sprite(Texture2D image)
        {
            Image = image;
            Scale = Vector2.One;
            Offset = Vector2.Zero;
            Depth = .5f;
            Color = Color.White;
            Rotation = 0;
        }

        /// <summary>
        /// Centers the offset of a sprite. 
        /// </summary>
        public void Center()
        {
            Offset = new Vector2(Image.Width, Image.Height) / 2;
        }
    }
}
