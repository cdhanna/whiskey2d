using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    using VectorX = Microsoft.Xna.Framework.Vector2;

    [Serializable]
    public struct Vector
    {

        public static Vector Zero { get { return new Vector(0, 0); } }
        public static Vector One { get { return new Vector(1, 1); } }
        public static Vector UnitX { get { return new Vector(1, 0); } }
        public static Vector UnitY { get { return new Vector(0, 1); } }


        private float x, y;

        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }

        public override string ToString()
        {
            return X + ", " + Y;
            //return base.ToString();
        }

        public Vector(float v)
        {
           
            x = v;
            y = v;
            //myX = v;
        }

        public Vector(float _x, float _y)
        {
            
            x = _x;
            y = _y;
            //myX = x;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                return Equals((Vector)obj);
            }

            return false;
        }

        public bool Equals(Vector other)
        {
            return (X == other.X) && (Y == other.Y);
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

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
