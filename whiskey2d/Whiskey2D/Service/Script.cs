using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// The base class for all scripts.
    /// Scripts control GameObject behavour.
    /// </summary>
    public abstract class Script
    {

        private GameObject gob;


        /// <summary>
        /// creates a new Script
        /// </summary>
        public Script()
        {
            
        }

        /// <summary>
        /// The GameObject that the script is controlling
        /// </summary>
        public GameObject Gob
        {
            get
            {
                return gob;
            }
            set
            {
                gob = value;
            }
        }

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
