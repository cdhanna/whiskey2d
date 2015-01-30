using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    [Serializable]
    public class VectorSet
    {
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

        public void forEachVector(Action<Vector> action)
        {
            vecs.ForEach(action);
        }

        public void forEachEdge(Action<Edge> action)
        {
            getAllEdges().ForEach(action);
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
