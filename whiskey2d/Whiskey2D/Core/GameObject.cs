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

        private Sprite sprite;
        private Light light;
       
        private ShadowProperties shadows;

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
            shadows = new ShadowProperties();
            Position = Vector.Zero;
            Sprite = new Sprite();
            Light = new Light();
            Light.Visible = false;

            Active = true;
            HudObject = false;
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



        private Layer layer;
        public virtual Layer Layer
        {
            get
            {
                if (layer == null)
                {
                    return GameManager.Level.Layers.Find(l => l.Name.Equals("Default"));
                }
                else return layer;
            }
            set
            {
                layer = value;
            }
        }

        /// <summary>
        /// Gets or sets if the GameObject is Active.
        /// If the GameObject is Active, then it is drawn and updated by the Whiskey engine, otherwise, 
        /// it will lay dorment until Activated.
        /// </summary>
        public virtual Boolean Active
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if the GameObject is a HudObject. If the GameObject is a HudObject, then it will appear on the HUD, and not in GameSpace
        /// </summary>
        public virtual Boolean HudObject
        {
            get;
            set;
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
        /// Get or Sets the Light of the GameObject
        /// </summary>
        public virtual Light Light
        {
            get
            {
                return light;
            }
            set
            {
                light = value;
            }

        }

        /// <summary>
        /// Gets or Sets the Shadows of the GameObject
        /// </summary>
        public virtual ShadowProperties Shadows
        {
            get
            {
                return shadows;
            }
            set
            {
                shadows = value;
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

        [System.ComponentModel.Browsable(false)]
        public List<Script> Scripts
        {
            get { return scripts; }
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
            List<Script> actives = getActiveScripts();

            actives.ForEach(s => s.onUpdate());

            //actives.RemoveAll(s => getActiveScripts().Contains(s));
            //actives.ForEach(s => s.onStart());

            getActiveScripts().ForEach(s =>
            {
                if (!actives.Contains(s))
                {
                    s.onStart();
                }


            });


            sprite.update();
        }

        /// <summary>
        /// Render the GameObject. If the GameObject has a sprite, it will be rendered here. 
        /// This is called from the RenderManager.
        /// Override if needed to draw something fancy.
        /// </summary>
        /// <param name="info">The info needed to render</param>
        public virtual void renderImage(RenderInfo info)
        {
            if (Sprite != null)
            {
                Sprite.setRender(info.Renderer);
                Sprite.setResources(info.Resources);
                Sprite.draw(info.SpriteBatch, info.Transform, Position);
            }
            
        }

        /// <summary>
        /// Render the GameObject's Light. If the GameObject has a Light, it will be rendered here.
        /// This is called from the RenderManager.
        /// Override fi needed to render a fancy light.
        /// </summary>
        /// <param name="info">The info needed to render</param>
        public virtual void renderLight(RenderInfo info)
        {
            if (Light != null)
            {
                Light.Position = Position;
                Light.render(info);
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

        public Script getScript(String name)
        {
            return scripts.Find(s => s.GetType().Name.Equals(name));
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

        public RayCollisions<G> currentRayCollisions<G>(Vector offset, Vector direction) where G : GameObject
        {
            Vector rayStart = Position + offset;
            Vector rayDir = direction.UnitSafe;

            RayCollisions<G> rayList = new RayCollisions<G>();

            List<G> all = objectManager.getAllObjectsOfType<G>();
            all.ForEach(gob =>
            {
                if (gob != this)
                {
                    RayCollisionInfo info = gob.Bounds.getRayCollisionInfo(rayStart, rayDir);
                    if (info != null)
                    {
                        rayList.Add(new RayCollision<G>(info, gob));
                    }
                }
            });

            rayList.Sort((rc1, rc2) => { if (rc1.Length > rc2.Length) return 1; else return -1; });

            return rayList;
        }

        public RayCollisions<G> currentRayCollisions<G>(Vector direction) where G : GameObject
        {
            return this.currentRayCollisions<G>(Vector.Zero, direction);
        }



        /// <summary>
        /// Get a set of Collisions with a given kind of GameObejct
        /// </summary>
        /// <typeparam name="G">Some type of GameObject</typeparam>
        /// <returns>A set of Collisions</returns>
        public Collisions<G> currentCollisions<G>() where G : GameObject
        {
            Collisions<G> collList = new Collisions<G>();
            List<G> all = objectManager.getAllObjectsOfType<G>();
            all.ForEach((gob) =>
            {
                if (gob != this)
                {
                    CollisionInfo info = gob.Bounds.getCollisionInfo(Bounds);
                    if (info != null)
                    {
                        collList.Add(new Collision<G>(info, gob));
                    }
                }
            });
            return collList;
        }

    }
}