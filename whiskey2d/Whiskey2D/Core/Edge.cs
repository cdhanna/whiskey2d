using System;
using System.Collections.Generic;

namespace Whiskey2D.Core
{
    [Serializable]
    public class Edge
    {

        public Vector V1 { get; private set; }
        public Vector V2 { get; private set; }
        public Vector Unit { get; private set; }
        public Vector Normal { get; private set; }
        public float Length { get { return (V2 - V1).Length; } }


        public Edge(Vector v1, Vector v2)
        {
            V1 = v1;
            V2 = v2;
            Unit = (v2 - v1).Unit;
            Normal = Unit.Perpendicular;
        }

    }
}
