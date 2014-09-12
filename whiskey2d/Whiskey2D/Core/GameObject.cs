using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
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

            Script initScript = getInitialScript();
            if (initScript != null)
            {
                this.addScript(initScript);
            }


            

            ObjectManager.getInstance().addObject(this);

        }

        public  Vector2 Position;
        private Sprite sprite;
        private int id;

        private List<Script> scripts; 

        //public Vector2 Position
        //{
        //    get
        //    {
        //        return position;
        //    }
        //    set
        //    {
        //        position = value;
        //    }
        //}
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
        /// Initializes the GameObject
        /// </summary>
        public void init()
        {

            //foreach (Script script in scripts)
            //{
            //    script.onStart();
            //}


        }

        /// <summary>
        /// Closes out the GameObject, and removes it from the ObjectManager
        /// </summary>
        public void close()
        {
            ObjectManager.getInstance().removeObject(this);
        }

        //public void addScript(Script<GameObject> s) 
        //{
        //    this.scripts.Add(s);
        //}

        protected void addScript(Script script) 
        {
            script.Gob = this;
            scripts.Add(script);
        }


        public void update()
        {
            foreach (Script script in scripts)
            {
                script.onUpdate();
            }
        }

        protected abstract Script getInitialScript();


        //public abstract void update();

    }
}
