using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// Simple solution for rectangular collision.
    /// </summary>
    public class Bounds
    {

        private Vector position;
        private Vector size;
        private float rotation;

        private Convex convex;

        /// <summary>
        /// The top-left position of the bounds
        /// </summary>
        public Vector Position { get { return position; } }

        /// <summary>
        /// The width and height of the bounds, in terms of X and Y 
        /// </summary>
        public Vector Size { get { return size; } }

        /// <summary>
        /// Tthe bottam Y location of the bound
        /// </summary>
        public float Bottam { get { return BottomLeft.Y; } }

        /// <summary>
        /// Tthe top Y location of the bound
        /// </summary>
        public float Top { get { return TopRight.Y; } }

        /// <summary>
        /// Tthe right X location of the bound
        /// </summary>
        public float Right { get { return TopRight.X; } }

        /// <summary>
        /// Tthe left X location of the bound
        /// </summary>
        public float Left { get { return TopLeft.X; } }

        public Vector TopLeft { get { return convex.TranslatedVectors.get(0); } }
        public Vector TopRight { get { return convex.TranslatedVectors.get(1); } }
        public Vector BottomLeft { get { return convex.TranslatedVectors.get(3); } }
        public Vector BottomRight { get { return convex.TranslatedVectors.get(2); } }



        /// <summary>
        /// Create a Bound from a top-left position, and a width-height
        /// </summary>
        /// <param name="position">The top-left position of the bounds</param>
        /// <param name="size">The width-height of the bounds, in terms of X and Y</param>
        public Bounds(Vector position, Vector size, float rotation)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;

            float width = size.X; 
            float height = size.Y; 
            float widthBar = width / 2;
            float heightBar = height / 2;
            convex = new Convex(position + size/2, rotation, new VectorSet(
                new Vector(-widthBar, -heightBar),
                new Vector(widthBar, -heightBar),
                new Vector(widthBar, heightBar),
                new Vector(-widthBar, heightBar)));
        }

        /// <summary>
        /// Determines if a given vector is within the bounds
        /// </summary>
        /// <param name="vec">some vector to check</param>
        /// <returns>true if the point is within the bounds, false otherwise</returns>
        public virtual Boolean vectorWithin(Vector vec)
        {
            return convex.isWithin(vec);
            //return vec.X > position.X
            //    && vec.X < position.X + size.X
            //    && vec.Y > position.Y
            //    && vec.Y < position.Y + size.Y;
                
        }

        public Vector Center
        {
            get
            {
                return position + (size / 2);
            }
        }

        public virtual Boolean boundWithin(Bounds bound)
        {
            //return !getNormalOfCollision(bound).Equals(Vector.Zero);

            return convex.isWithin(bound.convex);
            
            //return _inBound(this, bound) || _inBound(bound, this);

        }


        public Convex Convex { get { return convex; } }

        public CollisionInfo getCollisionInfo(Bounds bound)
        {
            return convex.getCollisionInfo(bound.convex);
        }

        public virtual Vector getNormalOfCollision(Bounds other)
        {
            Bounds A = this;
            Bounds B = other;

            float w = 0.5f * (A.size.X + B.size.X);
            float h = 0.5f * (A.size.Y  + B.size.Y);
            float dx = (A.Center - B.Center).X;
            float dy = (A.Center - B.Center).Y;
            Vector normal = Vector.Zero;
            if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
            {
                /* collision! */
                
                float wy = w * dy;
                float hx = h * dx;

                if (wy > hx)
                    if (wy > -hx)
                        normal = -Vector.UnitY; //top
                    else
                        normal = -Vector.UnitX; //left
                else
                    if (wy > -hx)
                        normal = Vector.UnitX; /* on the right */
                    else
                        normal = Vector.UnitY; /* at the bottom */
            }
            return normal;
        }

        public void draw(RenderInfo info, RenderHints hints)
        {
            convex.render(info.SpriteBatch, info.Transform, hints);
        }
        public void draw(RenderInfo info)
        {
            this.draw(info, new RenderHints());
        }


        private static Boolean _inBound(Bounds a, Bounds b)
        {
            return (a.vectorWithin(b.TopLeft) ||
                    a.vectorWithin(b.TopRight) ||
                    a.vectorWithin(b.BottomLeft) ||
                    a.vectorWithin(b.BottomRight));

        }
    }
}
