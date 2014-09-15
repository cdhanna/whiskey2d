using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    public class Sprite
    {

        public Texture2D Image { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 ImageSize { get { return new Vector2(Image.Width * Scale.X, Image.Height * Scale.Y); } }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public float Depth { get; set; }
        public Vector2 Offset { get; set; }

        


        public Sprite(Texture2D image)
        {
            Image = image;
            Scale = Vector2.One;
            Offset = Vector2.Zero;
            Depth = .5f;
            Color = Color.White;
            Rotation = 0;
        }

        public void Center()
        {
            Offset = new Vector2(Image.Width, Image.Height) / 2;
        }
    }
}
