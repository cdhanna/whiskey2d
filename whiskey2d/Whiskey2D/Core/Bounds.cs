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
        public float Bottom { get { return BottomLeft.Y; } }

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


        /// <summary>
        /// Gets the TopLeft coordinate of the Bound
        /// </summary>
        public Vector TopLeft { get { return convex.TranslatedVectors.get(0); } }

        /// <summary>
        /// Gets the TopRight coordinate of the Bound
        /// </summary>
        public Vector TopRight { get { return convex.TranslatedVectors.get(1); } }

        /// <summary>
        /// Gets the BottomLeft coordinate fo the Bound
        /// </summary>
        public Vector BottomLeft { get { return convex.TranslatedVectors.get(3); } }

        /// <summary>
        /// Gets the BottomRight coordinate of the Bounds
        /// </summary>
        public Vector BottomRight { get { return convex.TranslatedVectors.get(2); } }

        /// <summary>
        /// Gets the Center coordinate of the Bound
        /// </summary>
        public Vector Center { get { return position + (size / 2); } }

        /// <summary>
        /// Gets the Convex associated with this Bound
        /// </summary>
        public Convex Convex { get { return convex; } }

        /// <summary>
        /// Create a Bound from a top-left position, and a width-height
        /// </summary>
        /// <param name="position">The top-left position of the bounds</param>
        /// <param name="size">The width-height of the bounds, in terms of X and Y</param>
        /// <param name="rotation">The rotation of the Bound, in radians</param>
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
        }


        /// <summary>
        /// Determines if a given Bound is within this Bound
        /// </summary>
        /// <param name="bound">some other Bound</param>
        /// <returns>True, if the bounds overlap, False otherwise</returns>
        public virtual Boolean boundWithin(Bounds bound)
        {
            return convex.isWithin(bound.convex);
        }


        /// <summary>
        /// Retrieve the CollisionInfo for a collision between another Bounds. 
        /// </summary>
        /// <param name="bound">some other Bound</param>
        /// <returns>The collision info between the two Bounds, or null, if no Collision is happening</returns>
        public CollisionInfo getCollisionInfo(Bounds bound)
        {
            return convex.getCollisionInfo(bound.convex);
        }


        public RayCollisionInfo getRayCollisionInfo(Vector rayStart, Vector rayDir)
        {
            return convex.getRayCollisionInfo(rayStart, rayDir);

        }

        /// <summary>
        /// Draws the Bound
        /// </summary>
        /// <param name="info">The RenderInfo needed to draw an object</param>
        /// <param name="hints">any special instructions for drawing</param>
        public void draw(RenderInfo info, RenderHints hints)
        {
            //convex.Origin -= Vector.One;
            convex.render(info.SpriteBatch, info.Transform, hints);
        }


        /// <summary>
        /// Draws the Bound
        /// </summary>
        /// <param name="info">The RenderInfo needed to draw an object</param>
        public void draw(RenderInfo info)
        {
            this.draw(info, new RenderHints());
        }



    }
}
