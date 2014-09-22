using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.ex2
{

    /// <summary>
    /// A delegate that supports a no-arg call to a void return type. A.K.A., the update function
    /// </summary>
    public delegate void UpdateFunctionPointer();

    /// <summary>
    /// A ScriptBundle gives access to the GameObject, and the function that updates that GameObject
    /// </summary>
    /// <typeparam name="T">The type of GameObject that the script works with</typeparam>
    public class ScriptBundle<T> where T : Gob
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

        public void updateMe()
        {
            
            Console.WriteLine("GOB IS " + Gob);
            UpdatePointer();
        }

        public static ScriptBundle<Gob> createFrom<X>(Script<X> script) where X : Gob
        {
            ScriptBundle<Gob> gs = new ScriptBundle<Gob>();
            gs.UpdatePointer = new UpdateFunctionPointer(script.updatePlease);
            gs.StartPointer = new UpdateFunctionPointer(script.onStart);
            gs.Gob = script.Gob;

            return gs;
        }


    }

    public abstract class Script<T> : ScriptBundle<T> where T : Gob
    {

        
        public abstract void updatePlease();

        public abstract void onStart();
        

    }
}
