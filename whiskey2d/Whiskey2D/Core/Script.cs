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
    [Serializable]
    public abstract class Script
    {

        public Script()
        {
            Active = true;
        }

        /// <summary>
        /// Gets the InputManager. The InputManager is what provides all of the Input assessment. Use InputManager to determine if specific keys are
        /// being pressed.
        /// </summary>
        public InputManager Input { get { return GameManager.Instance.InputManager; } }

        /// <summary>
        /// Gets the InputSourceManager. The InputSourceManager is what provides the InputManager with an InputSource. An InputSource can be a Log file,
        /// a keyboard+mouse, or something else.
        /// </summary>
        public InputSourceManager InputSource { get { return GameManager.Instance.InputSourceManager; } }
        
        /// <summary>
        /// Gets the LogManager. The LogManager can be used to display messages to the game's log file, as well as the game
        /// </summary>
        public LogManager Log { get { return GameManager.Instance.LogManager; } }

        /// <summary>
        /// Gets the ObjectManager. The ObjectManager is what controls all of the currently loaded GameObjects
        /// </summary>
        public ObjectManager Objects { get { return GameManager.Instance.ObjectManager; } }

        /// <summary>
        /// Gets the RenderManager. The RenderManager is what draws all GameObjects to the screen.
        /// </summary>
        public RenderManager Renderer { get { return GameManager.Instance.RenderManager; } }
       
        /// <summary>
        /// Gets the ResourceManager. The ResourceManager is an abstraction of MonoGame's ContentManager, and gives specific functions for
        /// loading sounds and sprites.
        /// </summary>
        public ResourceManager Resources { get { return GameManager.Instance.ResourceManager; } }

        /// <summary>
        /// Gets the object that is controlling the Game. In most cases, this will be the WhiskeyLauncher.exe 
        /// </summary>
        public GameController Controller { get { return GameManager.Instance.GameController; } }

        /// <summary>
        /// Gets the currently active Level.
        /// </summary>
        public GameLevel Level { get { return GameManager.Instance.ActiveLevel; } }

        /// <summary>
        /// Gets the screen width of the game window, or -1 if there is an error
        /// </summary>
        public int ScreenWidth { get { return GameManager.Instance.WindowScreenWidth; } }

        /// <summary>
        /// Gets the screen height of the game window, or -1 if there is an error
        /// </summary>
        public int ScreenHeight { get { return GameManager.Instance.WindowScreenHeight; } }


        /// <summary>
        /// Gets the Type of the GameObject this Script is running for.
        /// </summary>
        public virtual Type GobType
        {
            get
            {
                if (Gob == null)
                    return typeof(GameObject);
                else return Gob.GetType();
            }
        }

        private GameObject gob;

        /// <summary>
        /// Gets or Sets the GameObject that this Script is running for.
        /// </summary>
        public virtual GameObject Gob
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
        /// Gets or Sets if the Script is Script.
        /// </summary>
        public Boolean Active
        {
            get;
            set;
        }

        /// <summary>
        /// The Code that will be run when the Script is first started
        /// </summary>
        public virtual void onStart()
        {

        }

        /// <summary>
        /// The Code that will be run when the Script is updated from the GameObject
        /// </summary>
        public virtual void onUpdate()
        {

        }

        /// <summary>
        /// The Code that will be run the Script shuts down
        /// </summary>
        public virtual void onClose()
        {

        }


    }

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