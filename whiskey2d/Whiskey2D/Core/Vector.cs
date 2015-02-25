using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    using VectorX = Microsoft.Xna.Framework.Vector2;

    /// <summary>
    /// The Vector type holds two pieces of information, an X value, and a Y value. Both values are floats. 
    /// The Vector type is an extension from the MonoGame, Vector2 type, and can be used interchangably in many cases.
    /// </summary>
    [Serializable]
    public struct Vector
    {
        /// <summary>
        /// Return a new Vector with a value of {0, 0}
        /// </summary>
        public static Vector Zero { get { return new Vector(0, 0); } }
        
        /// <summary>
        /// Return a new Vector with a value of {1, 1}
        /// </summary>
        public static Vector One { get { return new Vector(1, 1); } }

        /// <summary>
        /// Return a new Vector with a value of {1, 0}
        /// </summary>
        public static Vector UnitX { get { return new Vector(1, 0); } }

        /// <summary>
        /// Return a new Vector with a value of {0, 1}
        /// </summary>
        public static Vector UnitY { get { return new Vector(0, 1); } }


        private float x, y;

        /// <summary>
        /// Gets or sets the value of the X component
        /// </summary>
        public float X { get { return x; } set { x = value; } }

        /// <summary>
        /// Gets or sets the value of the Y component
        /// </summary>
        public float Y { get { return y; } set { y = value; } }

        /// <summary>
        /// Transforms the Vector into a String representation. The resulting string will be "X, Y". 
        /// For example, the vector, {43, -2} would become "43, -2".
        /// </summary>
        /// <returns>The string representation of the Vector</returns>
        public override string ToString()
        {
            return X + ", " + Y;
        }

        /// <summary>
        /// Creates a new Vector where both X and Y are the same value. 
        /// </summary>
        /// <param name="v">The value for both X, and Y.</param>
        public Vector(float v)
            : this(v, v)
        {
        }

        /// <summary>
        /// Creates a new Vector.
        /// </summary>
        /// <param name="_x">The value for the X component</param>
        /// <param name="_y">The value for the Y component</param>
        public Vector(float _x, float _y)
        {            
            x = _x;
            y = _y;
        }

        /// <summary>
        /// Checks if another object is equal to this Vector. If the object passed to this function is a Vector, then the check will defer to the Equals(Vector other) function.
        /// Otherwise, the object is not equal to this Vector.
        /// </summary>
        /// <param name="obj">Some object to test for equality</param>
        /// <returns>True if the object is equal to this Vector, and false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                return Equals((Vector)obj);
            }
            return false;
        }

        /// <summary>
        /// Checks if another Vector is equal to this Vector. To be considered equal, the X and Y components of each Vector must be equal. 
        /// Equality is done through float math, meaning that if the floating point values between the two Vectors are at all different, even by a tiny amount, the Vectors will not be
        /// considered equal.
        /// </summary>
        /// <param name="other">Some Vector to test for equality</param>
        /// <returns>True if the Vector is equal to this Vector, and false otherwise</returns>
        public bool Equals(Vector other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        /// <summary>
        /// Calculates the HashCode for this Vector. The HashCode is generated from the X and Y component values.
        /// </summary>
        /// <returns>The HashCode value</returns>
        public override int GetHashCode()
        {
            return 31 * X.GetHashCode() + 17 * Y.GetHashCode();
        }

        /// <summary>
        /// Gets the length of the Vector. This calculation is done with Euclidean distance. 
        /// Length = sqrt( (x*x) + (y*y) )
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(LengthSquared);
            }
        }

        /// <summary>
        /// Gets the squared length of the Vector. This is a cheaper alternative to the regular Length property, because it does not invoke a sqrt operation.
        /// </summary>
        public float LengthSquared
        {
            get
            {
                return (X * X) + (Y * Y);
            }
        }

        /// <summary>
        /// Gets a perpendicular Vector. The resulting Vector will always be {-Y, X} of this Vector. For example, 
        /// the Vector {5, 10}'s Perpendicular would be {-10, 5}. 
        /// </summary>
        public Vector Perpendicular
        {
            get
            {
                return new Vector(-Y, X);
            }
        }

        /// <summary>
        /// Gets the normalized Vector. A normal Vector will have the same direction as the original Vector, but the length is garunteed to be equal to 1.0. 
        /// For example, the Vector {5, 0}'s Normal would be {1, 0}. 
        /// </summary>
        public Vector Normal
        {
            get
            {
                Vector v = this;
                v.Normalize();
                return v;
            }
        }

        /// <summary>
        /// Gets the angle of the Vector. 
        /// </summary>
        public float Angle
        {
            get
            {
                return (float)Math.Atan2(Y, X);
            }
        }


        /// <summary>
        /// Calculates a Vector equal to the reflection of the current Vector, given an axis. If the axis is perpendicular to the Vector, then the resultant Vector will be equal and opposite. 
        /// Only direction is changed by calculating a reflection Vector. Length stays the same. 
        /// </summary>
        /// <param name="axis">The axis Vector to reflect the Vector on. It is recommended that the axis Vector be normalized.</param>
        /// <returns>The resulting reflected Vector</returns>
        public Vector reflect(Vector axis)
        {
            return VectorX.Reflect(this, axis);
        }

        /// <summary>
        /// Calculates the dot product between the given Vector and the current Vector.
        /// Given two Vectors, a, and b, the dot product is always dot(a, b) = (a.x * b.x) + (a.y * b.y).
        /// </summary>
        /// <param name="v">A vector to use in the dot product calculation.</param>
        /// <returns>The dot product between the two Vectors.</returns>
        public float dot(Vector v)
        {
            return (X * v.x) + (Y * v.Y);
        }

        /// <summary>
        /// Normalize the Vector. see the Normal property for further details.
        /// </summary>
        public void Normalize()
        {
            float val = 1.0f / (float)Math.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
        }

        public static Vector Normalize(Vector value)
        {
            float val = 1.0f / (float)Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
            value.X *= val;
            value.Y *= val;
            return value;
        }

        #region converters

        public static implicit operator VectorX(Vector v)
        {
            return new VectorX(v.X, v.Y);
        }
        
        public static implicit operator Vector(VectorX v)
        {
            return new Vector(v.X, v.Y);
        }

        //public Vector copy()
        //{
        //    return new Vector(X, Y);
        //}

        #endregion

        #region Operators

        public static Vector operator -(Vector value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }


        public static bool operator ==(Vector value1, Vector value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }


        public static bool operator !=(Vector value1, Vector value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }


        public static Vector operator +(Vector value1, Vector value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }


        public static Vector operator -(Vector value1, Vector value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static Vector operator *(Vector value1, Vector value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }


        public static Vector operator *(Vector value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vector operator *(float scaleFactor, Vector value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vector operator /(Vector value1, Vector value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }


        public static Vector operator /(Vector value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        #endregion Operators

    }
}
