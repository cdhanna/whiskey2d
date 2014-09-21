using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        

    }
}
