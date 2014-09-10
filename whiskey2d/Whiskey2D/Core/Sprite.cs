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


        public Sprite(Texture2D image)
        {
            Image = image;
            Scale = Vector2.One;
        }

    }
}
