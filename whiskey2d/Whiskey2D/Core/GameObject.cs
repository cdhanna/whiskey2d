using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{

    /// <summary>
    /// The base class for all user game components. 
    /// </summary>
    public abstract class GameObject 
    {
        private static int idCounter = 0;


        /// <summary>
        /// Create a new Game Object
        /// </summary>
        public GameObject()
        {
            Position = Vector2.Zero;
            Sprite = null;
            ID = idCounter++;
            scripts = new List<Script>();

            List<Script> initScripts = getInitialScripts();
            if (initScripts != null)
            {
                initScripts.ForEach((script) => { this.addScript(script); });
            }


            

            ObjectManager.getInstance().addObject(this);

        }


        private Sprite sprite;
        private int id;
        private List<Script> scripts;

        /// <summary>
        /// The position of the Game Object
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The Sprite of the Game Object. By default, this will start as null, and the GameObject will have no visuals.
        /// To give the Game Object visuals, set this to a new Sprite()
        /// </summary>
        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }

        /// <summary>
        /// The unique ID of the GameObject
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// The bounds of the GameObject. The Bounds are computed from the Position and Sprite of the GameObject. 
        /// If there is no Sprite, do not call this method.
        /// </summary>
        public Bounds Bounds
        {
            get
            {
                return new Bounds(Position, Sprite.ImageSize);
            } 
        }

        /// <summary>
        /// Initializes the GameObject
        /// </summary>
        public void init()
        {

            foreach (Script script in scripts)
            {
                script.onStart();
            }

        }

        /// <summary>
        /// Closes out the GameObject, and removes it from the ObjectManager
        /// </summary>
        public void close()
        {
            ObjectManager.getInstance().removeObject(this);
        }

        /// <summary>
        /// Add a script to the GameObject's behaviour
        /// </summary>
        /// <param name="script"></param>
        protected void addScript(Script script) 
        {
            script.Gob = this;
            scripts.Add(script);
        }

        /// <summary>
        /// Update all of the GameObject's scripts
        /// </summary>
        public void update()
        {
            foreach (Script script in scripts)
            {
                script.onUpdate();
            }
        }

        /// <summary>
        /// Called upon initialization. Used to retrieve a set of start up scripts for the object. 
        /// </summary>
        /// <returns>A list of scripts to be run by the GameObject, or null if no scripts should be run</returns>
        protected abstract List<Script> getInitialScripts();


    }
}
