using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    public class Rand
    {

        private static Rand instance = new Rand();
        public static Rand getInstance()
        {
            return instance;
        }


        private int seed;
        private Random r;


        private Rand()
        {
            seed = new Random().Next();
            r = new Random(seed);
            
        }

        public int getSeed()
        {
            return seed;
        }

        public void setSeed(int seed)
        {
            this.seed = seed;
            r = new Random(seed);
        }

        public void reSeed()
        {
            this.seed = r.Next();
            r = new Random(this.seed);
        }

        public int Next()
        {
            return r.Next();
        }

        public int Next(int max)
        {
            return r.Next(max);
        }

        public int Next(int min, int max)
        {
            return r.Next(min, max);
        }

        public float nextFloat()
        {
            return (float)r.NextDouble();
        }

        public Vector nextUnit2()
        {
            float angle = nextFloat() * (float)(Math.PI*2);
            return new Vector((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

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
