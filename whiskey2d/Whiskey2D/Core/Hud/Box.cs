using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Whiskey2D.Core.Hud
{
    class Box
    {

        public Box()
        {
            Position = Vector2.Zero;
            Size = Vector2.One;
            Color = Color.White;
            Depth = .9f;
            HudManager.getInstance().addBox(this);
        }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color Color {get; set; }
        public float Depth { get; set; }
        public bool Visible { get; set; }
        public void close()
        {
            HudManager.getInstance().removeBox(this);
        }

    }
}
