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

            
            //scripts = new List<Script <t> >();
            ObjectManager.getInstance().addObject(this);

        }

        private Vector2 position;
        private Sprite sprite;
        private int id;
        private Type type;
       
        private List<Script<GameObject>> scripts; //TODO fix

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
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

        protected void addScript<T>(Script<T> script) where T : GameObject
        {
            script.GameObject = (T)this;

            
            
            //scripts.Add(script);
        }


        public void update()
        {
            foreach (Script<GameObject> script in scripts)
            {
                script.onUpdate();
            }
        }

        //public abstract void update();

    }
}
