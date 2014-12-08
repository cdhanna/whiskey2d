using System;
using System.Collections.Generic;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{

    /// <summary>
    /// The base class for all user game components. 
    /// </summary>
    /// 
    [Serializable]
    public abstract class GameObject 
    {
        private static int idCounter = 0;

        public GameObject(ObjectManager objMan)
        {
            Position = Vector.Zero;
            Sprite = new Sprite();
            Sprite.Scale *= 50;
            ID = idCounter++;
            scripts = new List<Script>();
            //objectScriptTable = new Dictionary<object, ScriptBundle<GameObject>>();
            
            this.initProperties();
            this.addInitialScripts();

            objMan.addObject(this);

        }

        /// <summary>
        /// Create a new Game Object
        /// </summary>
        public GameObject() : this(GameManager.Objects)
        {
        }

        public virtual void initializeObject()
        {

        }

        private Sprite sprite;
        private int id;
        private List<Script> scripts;
       // private Dictionary<object, ScriptBundle<GameObject>> objectScriptTable;
        /// <summary>
        /// The position of the Game Object
        /// </summary>
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //public Vector2 Position;
        public Vector Position;

        public virtual float X { get { return Position.X; } set { Position = new Vector(value, Position.Y); } }
        public virtual float Y { get { return Position.Y; } set { Position = new Vector(Position.X, value); } }
        /// <summary>
        /// The Sprite of the Game Object. By default, this will start as null, and the GameObject will have no visuals.
        /// To give the Game Object visuals, set this to a new Sprite()
        /// </summary>

        public virtual Sprite Sprite
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
        [System.ComponentModel.ReadOnly(true)]
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
                if (Sprite == null)
                {
                    return new Bounds(Position, Vector.Zero);
                }
                else return new Bounds(Position - Sprite.Offset, Sprite.ImageSize);
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
            GameManager.Objects.removeObject(this);
        }

        public List<Script> getScripts()
        {
            return this.scripts;
        }

        //public List<ScriptBundle<GameObject>> getScriptBundles()
        //{
        //    return this.scripts;
        //}

        //public void removeScript(ScriptBundle<GameObject> script)
        //{
        //    this.scripts.Remove(script);
        //    this.objectScriptTable.Remove(script);
        //}

        //public void removeScript(object script)
        //{
        //    if (!this.objectScriptTable.ContainsKey(script))
        //    {
        //        return;
        //    }
        //    ScriptBundle<GameObject> converted = this.objectScriptTable[script];
        //    this.scripts.Remove(converted);
        //    this.objectScriptTable.Remove(script);
        //}

        public void clearScripts()
        {
            this.scripts.Clear();
           // this.objectScriptTable.Clear();
        }

        //public void addScript(object script)
        //{

        //    script.GetType().GetProperty(ScriptBundle<GameObject>.CODE_GOB).GetSetMethod().Invoke(script, new object[] { this });

        //    ScriptBundle<GameObject> converted = ScriptBundle<GameObject>.createFrom(script);
        //    this.scripts.Add(converted);
        //    this.objectScriptTable.Add(script, converted);
        //}

        public void removeScript<G>(Script<G> script) where G : GameObject
        {
            this.scripts.Remove(script);
            //removeScript((object)script);
        }

        public void removeScript(Script script)
        {
            this.scripts.Remove(script);
        }

        public void addScript<S>(S script) where S : Script
        {
            if (script.GobType.Equals(typeof(GameObject)) || 
                (!this.GetType().IsSubclassOf(script.GobType)) &&
                 !this.GetType().Equals(script.GobType))
            {
                throw new WhiskeyRunTimeException("Cannot add a script of type " + script.GobType + " to a game object of type " + this.GetType());
            } else {
                script.Gob = this;
                this.scripts.Add(script);
            }
            

        }
        /// <summary>
        /// Add a script to the GameObject's behaviour
        /// </summary>
        /// <param name="script"></param>
        public void addScript<G>(Script<G> script) where G : GameObject
        {
            //if (this.GetType() != typeof(G)) //runtime exception
            if (!this.GetType().IsSubclassOf(typeof(G)) && (this.GetType() != typeof(G)))
            {
                throw new WhiskeyRunTimeException("Cannot add a script of type " + typeof(G) + " to a game object of type " + this.GetType());
            }

            script.Gob = (G)this;
            this.scripts.Add(script);
            //ScriptBundle<GameObject> converted = ScriptBundle<GameObject>.createFrom(script);

            //this.objectScriptTable.Add(script, converted);
            //this.scripts.Add(converted);
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
        // protected abstract List<Script> getInitialScripts();

        protected abstract void addInitialScripts();


        protected virtual void initProperties()
        {
        }

        

    }
}