using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Core
{
  
    /// <summary>
    /// Collisions is a collection of Collision objects. Collisions is a sub-clss of the standard List type in System.Collections.Generic, so
    /// all of the List functions you are used to will still work. 
    /// </summary>
    /// <typeparam name="G">G must a GameObject. The type of G represents what kind of GameObjects the Collisions are in concern to.
    /// For example, if G is some GameObject, Car, then all Collisions will be about Car objects. </typeparam>
    [Serializable]
    public class Collisions<G> : List<Collision<G>> where G : GameObject
    {

        

    }



    /// <summary>
    /// RayCollisions is a collection of RayCollision objects. RayCollisions is a sub-clss of the standard List type in System.Collections.Generic, so
    /// all of the List functions you are used to will still work. 
    /// </summary>
    /// <typeparam name="G">G must a GameObject. The type of G represents what kind of GameObjects the RayCollisions are in concern to.
    /// For example, if G is some GameObject, Car, then all RayCollisions will be about Car objects. </typeparam>
    [Serializable]
    public class RayCollisions<G> : List<RayCollision<G>> where G : GameObject
    {

    }

}
