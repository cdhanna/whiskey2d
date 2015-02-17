using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core.Hud
{
    /// <summary>
    /// A TextLine is a line of text that will be written to the screen
    /// </summary>
    public class TextLine
    {

        
        /// <summary>
        /// Create a new TextLine with default values
        /// </summary>
        public TextLine()
        {
            Font = GameManager.Resources.getDefaultFont();
            Position = Vector2.Zero;
            Text = "test";
            Color = Color.White;
            Visible = true;
            Size = Vector2.One;
            HudManager.Instance.addTextLine(this);

        }

        /// <summary>
        /// The font that should be used for the text
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// The position of the text
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The text to display
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// The color of the text
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The size of the text. 
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// true if the text should be visible, false otherwise
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Removes the TextLine from the HudManager
        /// </summary>
        public void close()
        {
            HudManager.Instance.removeTextLine(this);
        }

    }
}
