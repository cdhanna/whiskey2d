using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// A delegate that supports a no-arg call to a void return type. A.K.A., the update function
    /// </summary>
    public delegate void UpdateFunctionPointer();

    /// <summary>
    /// A ScriptBundle gives access to the GameObject, and the function that updates that GameObject
    /// </summary>
    /// <typeparam name="T">The type of GameObject that the script works with</typeparam>
    public class ScriptBundle<T> where T : GameObject
    {

        protected ScriptBundle() { }

        /// <summary>
        /// The GameObject
        /// </summary>
        public T Gob { get; set; }

        /// <summary>
        /// The pointer to a function
        /// </summary>
        private UpdateFunctionPointer UpdatePointer { get; set; }

        private UpdateFunctionPointer StartPointer { get; set; }

        public void start()
        {
            StartPointer();
        }

        public void update()
        {
            UpdatePointer();
        }

        public static ScriptBundle<GameObject> createFrom<X>(Script<X> script) where X : GameObject
        {
            ScriptBundle<GameObject> gs = new ScriptBundle<GameObject>();
            gs.UpdatePointer = new UpdateFunctionPointer(script.onUpdate);
            gs.StartPointer = new UpdateFunctionPointer(script.onStart);
            gs.Gob = script.Gob;

            return gs;
        }


    }


    /// <summary>
    /// The base class for all scripts.
    /// Scripts control GameObject behavour.
    /// </summary>
    public abstract class Script<G> : ScriptBundle<G> where G : GameObject
    {

        //private GameObject gob;


        /// <summary>
        /// creates a new Script
        /// </summary>
        public Script()
        {
            
        }

        ///// <summary>
        ///// The GameObject that the script is controlling
        ///// </summary>
        //public GameObject Gob
        //{
        //    get
        //    {
        //        return gob;
        //    }
        //    set
        //    {
        //        gob = value;
        //    }
        //}

        /// <summary>
        /// Called once upon startup of the GameObject
        /// </summary>
        public abstract void onStart();

        /// <summary>
        /// Called upon every update cycle of the GameObject
        /// </summary>
        public abstract void onUpdate();

        
    }
}
