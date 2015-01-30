using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    [Serializable]
    public class RenderHints
    {
        public Color Color { get; set; }
        public PrimitiveType PrimitiveType { get; set; }

        public RenderHints()
        {
            Color = Color.White;
            PrimitiveType = PrimitiveType.LineStrip;
        }

        public RenderHints setColor(Color color)
        {
            Color = color;
            return this;
        }

        public RenderHints setPrimitiveType(PrimitiveType prim)
        {
            PrimitiveType = prim;
            return this;
        }



    }
}
