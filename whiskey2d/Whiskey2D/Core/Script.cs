using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{



    /// <summary>
    /// The Script gives control to GameObjects.
    /// </summary>
    /// <typeparam name="G">The type of GameObject this Script is built to run for</typeparam>
    [Serializable]
    public abstract class Script<G> : Script where G : GameObject
    {

        /// <summary>
        /// The type of the GameObject this Script is running for.
        /// </summary>
        public override Type GobType
        {
            get
            {
                return typeof(G);
            }
        }

        /// <summary>
        /// Gets or Sets the GameObject this Script is running for.
        /// </summary>
        public new virtual G Gob
        {

            get
            {
                return (G)base.Gob;
            }
            set
            {
                if (value != null)
                {
                    if (!typeof(G).IsSubclassOf(value.GetType()) &&
                         !typeof(G).Equals(value.GetType()))
                    {
                        throw new WhiskeyRunTimeException("Cannot assigned script GOB of " + typeof(G) + " as a " + value.GetType());
                    }
                }
                base.Gob = value;
            }
        }

    }


}