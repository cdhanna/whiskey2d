using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    public class Bounds
    {

        private Vector2 position;
        private Vector2 size;

        public Vector2 Position { get { return position; } }
        public Vector2 Size { get { return size; } }

        public float Bottam { get { return position.Y + size.Y; } }
        public float Top { get { return position.Y; } }
        public float Right { get { return position.X + size.X; } }
        public float Left { get { return position.X; } }

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
