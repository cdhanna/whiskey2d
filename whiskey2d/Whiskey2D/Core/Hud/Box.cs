using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Whiskey2D.Core.Hud
{
    /// <summary>
    /// A box is just a box that is drawn for hud purposes
    /// </summary>
    public class Box
    {

        /// <summary>
        /// Creates a new Box with default values
        /// </summary>
        public Box()
        {
            Position = Vector2.Zero;
            Size = Vector2.One;
            Color = Color.White;
            Depth = .9f;
            HudManager.Instance.addBox(this);
        }

        /// <summary>
        /// The top-left corner of the box
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The size of the box
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// The color of the box
        /// </summary>
        public Color Color {get; set; }

        /// <summary>
        /// The z-depth value of the box. Defaults to .9
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// True if the box is visible, false otherwise
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Removes the box from the HudManager
        /// </summary>
        public void close()
        {
            HudManager.Instance.removeBox(this);
        }

    }
}
