using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{

    /// <summary>
    /// Simple solution for rectangular collision.
    /// </summary>
    public class Bounds
    {

        private Vector2 position;
        private Vector2 size;

        /// <summary>
        /// The top-left position of the bounds
        /// </summary>
        public Vector2 Position { get { return position; } }

        /// <summary>
        /// The width and height of the bounds, in terms of X and Y 
        /// </summary>
        public Vector2 Size { get { return size; } }

        /// <summary>
        /// Tthe bottam Y location of the bound
        /// </summary>
        public float Bottam { get { return position.Y + size.Y; } }

        /// <summary>
        /// Tthe top Y location of the bound
        /// </summary>
        public float Top { get { return position.Y; } }

        /// <summary>
        /// Tthe right X location of the bound
        /// </summary>
        public float Right { get { return position.X + size.X; } }

        /// <summary>
        /// Tthe left X location of the bound
        /// </summary>
        public float Left { get { return position.X; } }

        /// <summary>
        /// Create a Bound from a top-left position, and a width-height
        /// </summary>
        /// <param name="position">The top-left position of the bounds</param>
        /// <param name="size">The width-height of the bounds, in terms of X and Y</param>
        public Bounds(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        /// <summary>
        /// Determines if a given vector is within the bounds
        /// </summary>
        /// <param name="vec">some vector to check</param>
        /// <returns>true if the point is within the bounds, false otherwise</returns>
        public virtual Boolean vectorWithin(Vector2 vec)
        {

            return vec.X > position.X
                && vec.X < position.X + size.X
                && vec.Y > position.Y
                && vec.Y < position.Y + size.Y;
                
        }

    }
}
