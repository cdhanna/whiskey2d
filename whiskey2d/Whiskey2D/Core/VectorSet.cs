using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    [Serializable]
    public class VectorSet
    {
        public static VectorSet Dodecahedren
        {
            get
            {
                VectorSet vecSet = new VectorSet();
                int sides = 12;
                float angleInc = ((float)Math.PI*2) / sides;
                for (int i = 0; i < sides; i++)
                {
                    float angle = i * angleInc;
                    Vector v = new Vector((float)Math.Cos(angle), (float)Math.Sin(angle));
                    vecSet.add(v);
                }
                return vecSet;
            }
        }
        public static VectorSet Diamond
        {
            get
            {
                return new VectorSet(new Vector(-1, 0), new Vector(0, 1), new Vector(1, 0), new Vector(0, -1));
            }
        }
        public static VectorSet Square
        {
            get
            {
                Matrix m = Matrix.Identity;
                m *= Matrix.CreateRotationZ((float)Math.PI/4);
                VectorSet d = new VectorSet();
                Diamond.forEachVector(v =>
                {
                    d.add( Vector2.Transform(v, m));
                });
                return d;
            }
        }

        private List<Vector> vecs;

        public int Size { get { return vecs.Count; } } 

        public VectorSet()
        {
            vecs = new List<Vector>();
        }
        

        public VectorSet(params Vector[] vset) : this()
        {
            for (int i = 0; i < vset.Length; i++)
            {
                add(vset[i]);
            }
        }

        public void insert(Vector v, int index)
        {
            vecs.Insert(index, v);
        }
        public void add(Vector v)
        {
            vecs.Add(v);
        }
        public void remove(int index)
        {
            vecs.RemoveAt(index);
        }
        public void remove(Vector v)
        {
            vecs.Remove(v);
        }
        public Vector get(int index)
        {
            return vecs[index];
        }

        public void forEachVector(Action<Vector> action)
        {
            vecs.ForEach(action);
        }

        public void forEachEdge(Action<Edge> action)
        {
            getAllEdges().ForEach(action);
        }
        public Vector[] toArray()
        {
            return findAll(v => true).ToArray();
        }
        public List<Vector> findAll(Predicate<Vector> action)
        {
            return vecs.FindAll(action);
        }
        public List<Vector> getAllVectors()
        {
            return vecs.FindAll(v => {return true; });
        }
        public List<Edge> getAllEdges()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < vecs.Count; i++)
            {
                edges.Add(new Edge(vecs[i], vecs[ (i + 1) % vecs.Count ]));
            }
            return edges;
        }
        public VectorSet transform(Matrix m)
        {
            VectorSet s = new VectorSet();
            vecs.ForEach(v => s.add(Vector2.Transform(v, m)));
            return s;
        }
        public VectorSet rotate(float angle)
        {
            Matrix m = Matrix.Identity * Matrix.CreateRotationZ(angle);
            return transform(m);
        }

        public static VectorSet operator * (VectorSet vSet, float mag){

            VectorSet oSet = new VectorSet();
            vSet.forEachVector(v =>
            {
                oSet.add(v * mag);
            });
            return oSet;

        }
        

    }
}
