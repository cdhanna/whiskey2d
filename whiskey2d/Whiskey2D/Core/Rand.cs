using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    /// <summary>
    /// Rand is a utility Random service
    /// </summary>
    public class Rand
    {

        private static Rand instance = new Rand();

        /// <summary>
        /// Get the Rand instance
        /// </summary>
        public static Rand Instance { get { return instance; } }


        private int seed;
        private Random r;


        private Rand()
        {
            seed = new Random().Next();
            r = new Random(seed);
            
        }

        /// <summary>
        /// Get or Set the seed value
        /// </summary>
        public int Seed
        {
            get
            {
                return seed;
            }
            set
            {
                seed = value;
                r = new Random(seed);
            }
        }

     
        /// <summary>
        /// Randomly reseed the seed value
        /// </summary>
        public void reSeed()
        {
            this.seed = r.Next();
            r = new Random(this.seed);
        }

        /// <summary>
        /// Get the next random integer
        /// </summary>
        /// <returns>A nonnegative int</returns>
        public int Next()
        {
            return r.Next();
        }

        /// <summary>
        /// Get the next random integer, no bigger than max
        /// </summary>
        /// <param name="max">a max value</param>
        /// <returns>A nonnegative int</returns>
        public int Next(int max)
        {
            return r.Next(max);
        }

        /// <summary>
        /// Get the next random integer, no bigger than max, no smaller than min
        /// </summary>
        /// <param name="max">a max value</param>
        /// <param name="min">a min value</param>
        /// <returns>A nonnegative int</returns>
        public int Next(int min, int max)
        {
            return r.Next(min, max);
        }

        /// <summary>
        /// Get the next random float, between 0, and 1
        /// </summary>
        /// <returns>a float from 0 to 1</returns>
        public float nextFloat()
        {
            return (float)r.NextDouble();
        }

        /// <summary>
        /// Get the next random UnitVector
        /// </summary>
        /// <returns>a unit vector</returns>
        public Vector nextUnit2()
        {
            float angle = nextFloat() * (float)(Math.PI*2);
            return new Vector((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        /// <summary>
        /// Get the next random color variation
        /// </summary>
        /// <param name="baseColor">Some base color</param>
        /// <param name="redVar">the amount to vary the red component</param>
        /// <param name="greenVar">the amount to vary the green component</param>
        /// <param name="blueVar">the amount to vary the blue component</param>
        /// <param name="alphaVar">the amount to vary the alpha component</param>
        /// <returns></returns>
        public Color nextColorVariation(Color baseColor, float redVar, float greenVar, float blueVar, float alphaVar)
        {
            redVar = (nextFloat() * redVar) - (redVar / 2);
            greenVar = (nextFloat() * greenVar) - (greenVar / 2);
            blueVar = (nextFloat() * blueVar) - (blueVar / 2);
            alphaVar = (nextFloat() * alphaVar) - (alphaVar / 2);
            
            float r = redVar + (float)(baseColor.R / 255.0);
            float g = greenVar + (float)(baseColor.G / 255.0);
            float b = blueVar + (float)(baseColor.B / 255.0);
            float a = alphaVar + (float)(baseColor.A / 255.0);
            r = Math.Min(Math.Max(r, 0), 1);
            g = Math.Min(Math.Max(g, 0), 1);
            b = Math.Min(Math.Max(b, 0), 1);
            a = Math.Min(Math.Max(a, 0), 1);
            return new Color(r, g, b, a);
        }

    }
}
