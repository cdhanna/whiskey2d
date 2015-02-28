using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Core
{
    /// <summary>
    /// Collision keeps track of useful information about a collision, including the colliding object. 
    /// </summary>
    /// <typeparam name="G">The type of Gameobject that is colliding</typeparam>
    [Serializable]
    public class Collision<G> where G : GameObject
    {
        /// <summary>
        /// Gets the GameObject that is colliding
        /// </summary>
        public G Gob { get; private set; }
        private CollisionInfo Info { get; set; }

        /// <summary>
        /// Gets the MTV of the collision. The MTV is the vector required to push the colliding objects apart. The MTV is a vector pointing
        /// in the direction of the Normal, scaled by the Overlap
        /// </summary>
        public Vector MTV { get { return Info.MTV; } }

        /// <summary>
        /// Gets the Normal of the collision.The Normal is a unit a vector that points in the direction of the collision. For example, if an object hit
        /// the floor, then the Normal would be the vector, {0, -1}
        /// </summary>
        public Vector Normal { get { return Info.Normal; } }

        /// <summary>
        /// Gets the Overlap of the collision. The Overlap is the amount that the objects are colliding.
        /// </summary>
        public float Overlap { get { return Info.Overlap; } }


        internal Collision(CollisionInfo info, G gob)
        {
            Gob = gob;
            Info = info;
        }
    }


    /// <summary>
    /// RayCollision keeps track of useful information about a ray collision, including the colliding object. 
    /// </summary>
    /// <typeparam name="G">The type of Gameobject that is colliding</typeparam>
    [Serializable]
    public class RayCollision<G> where G : GameObject
    {
        /// <summary>
        /// Gets the GameObject that is colliding
        /// </summary>
        public G Gob { get; private set; }

        /// <summary>
        /// Gets the Normal of the collision. The Normal is a unit a vector that points in the direction of the collision. For example, if an object hit
        /// the floor, then the Normal would be the vector, {0, -1}
        /// </summary>
        public Vector Normal { get { return info.Normal; } }

        /// <summary>
        /// Gets the ContactPoint, The ContactPoint is the vector where the ray collides
        /// </summary>
        public Vector ContactPoint { get { return info.ContactPoint; } }

        /// <summary>
        /// Gets the start position of the Ray
        /// </summary>
        public Vector RayStart { get { return info.RayStart; } }

        /// <summary>
        /// Gets the direction of the Ray
        /// </summary>
        public Vector rayDirection { get { return info.RayDirection; } }

        /// <summary>
        /// Gets the Length from the ray start, to the contact point
        /// </summary>
        public float Length { get { return info.Length; } }


        private RayCollisionInfo info;

        internal RayCollision(RayCollisionInfo info, G gob)
        {
            this.info = info;
            this.Gob = gob;

        }
    }


}