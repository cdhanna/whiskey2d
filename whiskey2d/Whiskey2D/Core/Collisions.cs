using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Core
{
    /*
     * 
     * Collisions<Wall> colls = Gob.currentCollisions<Wall>();
     * colls.process( c => {
     * 
     *      c.Info.MTV;....
     * 
     * });
     * 
     */


   

    [Serializable]
    public class Collisions<G> : List<Collision<G>> where G : GameObject
    {

        public void process(Action<Collision<G>> action)
        {
            
            foreach (Collision<G> c in this)
            {
                action(c);
            }
        }

    //    public virtual List<ObjectCollisionInfo<G>> Infos { get; protected set; }

    //    public Collisions(List<ObjectCollisionInfo<G>> infos)
    //    {
    //        Infos = infos;

    }


    // CollisionsWith wallColls = Gob.currentCollisionsWith()){
    // foreach (Collision c in wallColls){ if (c is Wall) { }}
    //}


    //    public IEnumerator<ObjectCollisionInfo<G>> GetEnumerator()
    //    {
    //        for (int i = 0; i < Infos.Count; i++)
    //        {
    //            yield return Infos[i];
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}

    

}
