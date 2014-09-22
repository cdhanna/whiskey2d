using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core.Hud
{
    class TextLine
    {

        

        public TextLine()
        {
            Font = ResourceManager.getInstance().getDefaultFont();
            Position = Vector2.Zero;
            Text = "test";
            Color = Color.White;
            Visible = true;
            Size = Vector2.One;
            HudManager.getInstance().addTextLine(this);

        }

        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public String Text { get; set; }
        public Color Color { get; set; }
        public Vector2 Size { get; set; }
        public bool Visible { get; set; }

        public void close()
        {
            HudManager.getInstance().removeTextLine(this);
        }

    }
}
