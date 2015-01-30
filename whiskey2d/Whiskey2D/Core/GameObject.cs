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

        private Sprite sprite;
        private int id;
        private bool active;

        /// <summary>
        /// The list of Scripts that the GameObject is currently running
        /// </summary>
        protected List<Script> scripts;

        /// <summary>
        /// The ObjectManager that this GameObject belongs to
        /// </summary>
        protected ObjectManager objectManager;
        
        /// <summary>
        /// The position of the GameObject
        /// </summary>
        public Vector Position;


        /// <summary>
        /// Creates a GameObject. This GameObject is created with an ObjectManager to store it in.
        /// Every GameObject must belong to an ObjectManager. If no ObjectManager is given, then 
        /// the GameManager's default ObjectManager will be used.
        /// It is important to remember that Levels are ObjectManagers, and can be passed in to the 
        /// GameObject constructor.
        /// </summary>
        /// <param name="objMan">An ObjectManager to store the GameObject in.</param>
        public GameObject(ObjectManager objMan)
        {
            scripts = new List<Script>();

            ID = idCounter++;
            Position = Vector.Zero;
            Sprite = new Sprite();

            active = true;
            this.initProperties();
            this.addInitialScripts();

            if (objMan == null)
            {
                objMan = GameManager.Objects;
            }
            objMan.addObject(this);
            objectManager = objMan;
            
        }

        /// <summary>
        /// Creates a GameObject. This GameObject is created with an ObjectManager to store it in.
        /// Every GameObject must belong to an ObjectManager. If no ObjectManager is given, then 
        /// the GameManager's default ObjectManager will be used.
        /// It is important to remember that Levels are ObjectManagers, and can be passed in to the 
        /// GameObject constructor.
        /// </summary>
        public GameObject() : this(GameManager.Objects)
        {
            //do nothing, as subclasses will fill in implementation
            /*
                scripts = new List<Script>();
                ID = idCounter++;
                Position = Vector.Zero;
                Sprite = new Sprite();

                active = true;
                this.initProperties();
                this.addInitialScripts();
             */
        }

        /// <summary>
        /// The initializeObject method is called by Whiskey immediately after the object is instantiated. 
        /// Subclasses of GameObject should use this function to specify details
        /// </summary>
        public virtual void initializeObject()
        {

        }

        /// <summary>
        /// Gets or Sets the x-position of the GameObject. This property automatically reads into the Position's X coordiate.
        /// </summary>
        public virtual float X { get { return Position.X; } set { Position = new Vector(value, Position.Y); } }
        
        /// <summary>
        /// Gets or Sets the y-position of the GameObject. This property automatically reads into the Position's y coordinate.
        /// </summary>
        public virtual float Y { get { return Position.Y; } set { Position = new Vector(Position.X, value); } }

        /// <summary>
        /// Gets or Sets the unique name of the GameObject. The name must be unique accross all GameObjects, reglardless of type.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets if the GameObject is Active.
        /// If the GameObject is Active, then it is drawn and updated by the Whiskey engine, otherwise, 
        /// it will lay dorment until Activated.
        /// </summary>
        public virtual Boolean Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        /// <summary>
        /// Gets or Sets The Debug state of the GameObject. if The GameObject is in Debug, then it is only 
        /// not drawn normally.
        /// </summary>
        public virtual Boolean IsDebug { get; set; }

        /// <summary>
        /// Gets or Sets the Sprite of the GameObject. 
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
        /// Get or Set the unique ID of the GameObject
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
        /// Get the bounds of the GameObject. The Bounds are computed from the Position 
        /// and Sprite of the GameObject. 
        /// If there is no Sprite, do not call this method.
        /// </summary>
        public Bounds Bounds
        {
            get
            {
                if (Sprite == null)
                {
                    return new Bounds(Position, Vector.Zero, 0);
                }
                else return new Bounds(Position - Sprite.FrameOffsetScaled, Sprite.FrameSizeScaled, Sprite.Rotation);
            }
        }

        /// <summary>
        /// Initializes the GameObject. This will call onStart() on any Scripts attached to the GameObject.
        /// </summary>
        public void init()
        {
            getActiveScripts().ForEach(s => s.onStart());
        }

        /// <summary>
        /// Closes out the GameObject, and removes it from the ObjectManager
        /// This will call onClose() on any Script attached to the GameObject.
        /// </summary>
        public void close()
        {
            objectManager.removeObject(this);
            getActiveScripts().ForEach(s => s.onClose());
        }

        /// <summary>
        /// Remove all Scripts from the GameObject
        /// </summary>
        public virtual void clearScripts()
        {
            this.scripts.Clear();
        }

        /// <summary>
        /// Remove a script from the GameObject.
        /// Calling this function WILL NOT call onClose() on the given Script
        /// </summary>
        /// <typeparam name="G">The type of this GameObject</typeparam>
        /// <param name="script">A Script that is currently attached to the GameObject</param>
        public void removeScript<G>(Script<G> script) where G : GameObject
        {
            this.scripts.Remove(script);
        }

        /// <summary>
        /// Remove a script from the GameObject.
        /// Calling this function will NOT call onClose() on the given Script
        /// </summary>
        /// <param name="script">A Script that is currently attached to the GameObject</param>
        public void removeScript(Script script)
        {
            this.scripts.Remove(script);
        }

        /// <summary>
        /// Add a Script
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="script"></param>
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
            if (!this.GetType().IsSubclassOf(typeof(G)) && (this.GetType() != typeof(G)))
            {
                throw new WhiskeyRunTimeException("Cannot add a script of type " + typeof(G) + " to a game object of type " + this.GetType());
            }

            script.Gob = (G)this;
            this.scripts.Add(script);
        }

        /// <summary>
        /// Update all of the GameObject's scripts, as well as the GameObject's Sprite
        /// </summary>
        public void update()
        {
            getActiveScripts().ForEach(s => s.onUpdate());
            sprite.update();
        }

        /// <summary>
        /// Render the GameObject. If the GameObject has a sprite, it will be rendered here. 
        /// This is called from the RenderManager.
        /// Override if needed to draw something fancy.
        /// </summary>
        /// <param name="info">The info needed to render</param>
        public virtual void render(RenderInfo info)
        {
            if (Sprite != null)
            {
                Sprite.setRender(info.Renderer);
                Sprite.setResources(info.Resources);
                Sprite.draw(info.SpriteBatch, info.Transform, Position);
            }
        }


        /// <summary>
        /// Get the set of all Scripts that are currently set to Active. 
        /// </summary>
        /// <returns></returns>
        protected List<Script> getActiveScripts()
        {
            return scripts.FindAll(s => s.Active);
        }

        /// <summary>
        /// Called upon initialization. Used to retrieve a set of start up scripts for the object. 
        /// </summary>
        /// <returns>A list of scripts to be run by the GameObject, or null if no scripts should be run</returns>
        // protected abstract List<Script> getInitialScripts();
        protected abstract void addInitialScripts();

        //Used by subclasses to setup properties of the GameObject
        protected virtual void initProperties()
        {

        }

        /// <summary>
        /// Get the typename of the GameObject
        /// </summary>
        /// <returns>Returns the typename of the GameObject</returns>
        public virtual string getTypeName()
        {
            return GetType().Name;
        }




        /// <summary>
        /// Every GameObject has a set of Bounds that can be used to collect collision info.
        /// This function returns a list of collision information objects with a particular other kind of GameObject
        /// 
        /// An example way to call this function looks like
        /// List\GameObject, G/ collInfos = Gob.currentCollisions \G/ ();
        /// </summary>
        /// <typeparam name="G">The kind of GameObject to check for collisions with</typeparam>
        /// <returns>The list of COllisionInfo</returns>
        //public List<CollisionInfo<GameObject, G>> currentCollisions<G>() where G : GameObject
        //{
        //    List<CollisionInfo<GameObject, G>> collisionInfos = new List<CollisionInfo<GameObject, G>>();

        //    List<G> all = objectManager.getAllObjectsOfType<G>();
        //    all.ForEach((gob) =>
        //    {
        //        if (gob != this)
        //        {
        //            Vector normal = gob.Bounds.getNormalOfCollision(Bounds);
        //            if (normal != Vector.Zero)
        //            {
        //                collisionInfos.Add(new CollisionInfo<GameObject, G>(this, gob, normal));
        //            }
        //        }
        //    });
        //    return collisionInfos;
        //}

        public List<G> currentCollisions<G>() where G : GameObject
        {
            List<G> collList = new List<G>();

            List<G> all = objectManager.getAllObjectsOfType<G>();
            all.ForEach((gob) =>
            {
                if (gob != this)
                {
                    if (gob.Bounds.boundWithin(Bounds))
                    {
                        collList.Add(gob);
                    }
                }

            });

            return collList;
        }

    }
}