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

        public GameObject A { get; protected set; }
        public GameObject B { get; protected set; }

        public Vector Normal { get; private set; }

        internal CollisionInfo(GameObject a, GameObject b, Vector normal)
        {
            A = a;
            B = b;
            Normal = normal;
        }

    }

    [Serializable]
    public class CollisionInfo<G1, G2> : CollisionInfo where G1 : GameObject where G2 : GameObject
    {
        public new G1 A { get; private set; }
        public new G2 B { get; private set; }


        internal CollisionInfo(G1 a, G2 b, Vector normal)
            : base(a, b, normal)
        {
            A = a;
            B = b;
        }

    }

}
