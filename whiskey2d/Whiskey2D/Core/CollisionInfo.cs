using System;
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
            Normal = MTV.normalize();
            Overlap = MTV.Length();
        }
    }

    [Serializable]
    public class ObjectCollisionInfo<G> where G : GameObject
    {
        public G Gob { get; private set; }
        public CollisionInfo Info { get; private set; }
        internal ObjectCollisionInfo(CollisionInfo info, G gob)
        {
            Gob = gob;
            Info = info;
        }
    }

}
