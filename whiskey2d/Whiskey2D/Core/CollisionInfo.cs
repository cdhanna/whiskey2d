﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Core
{
    [Serializable]
    public class CollisionInfo
    {

        public Vector Normal { get; private set; }
        public Vector MTV { get; private set; }
        public float Overlap { get; private set; }

        internal CollisionInfo(Vector MTV)
        {
            this.MTV = MTV;
            Normal = MTV.Normal;
            Overlap = MTV.Length;
        }
    }

    [Serializable]
    public class Collision<G> where G : GameObject
    {
        public G Gob { get; private set; }
        private CollisionInfo Info { get; set; }

        public Vector MTV { get { return Info.MTV; } }
        public Vector Normal { get { return Info.Normal; } }
        public float Overlap { get { return Info.Overlap; } }


        internal Collision(CollisionInfo info, G gob)
        {
            Gob = gob;
            Info = info;
        }
    }

}
