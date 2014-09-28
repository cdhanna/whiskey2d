using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Managers.Impl
{

    /// <summary>
    /// Manages all GameObjects in the Whiskey Game. 
    /// </summary>
    public class DefaultObjectManager : ObjectManager
    {
        //private static DefaultObjectManager instance;

        ///// <summary>
        ///// Retrieves the ObjectManager
        ///// </summary>
        ///// <returns>The ObjectManager</returns>
        //public static DefaultObjectManager getInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new DefaultObjectManager();
        //    }
        //    return instance;
        //}

        protected List<GameObject> gameObjects;
        protected List<GameObject> deadObjects;
        protected List<GameObject> newObjects;

        public DefaultObjectManager()
        {
           
        }

        /// <summary>
        /// Initializes the ObjectManager
        /// </summary>
        public virtual void init()
        {
            gameObjects = new List<GameObject>();
            deadObjects = new List<GameObject>();
            newObjects = new List<GameObject>();
        }

        /// <summary>
        /// Closes out the ObjectManager
        /// </summary>
        public virtual void close()
        {
            gameObjects.Clear();
            deadObjects.Clear();
            newObjects.Clear();
        }

        /// <summary>
        /// Updates all Game Objects
        /// </summary>
        public virtual void updateAll()
        {
            foreach (GameObject gob in gameObjects)
            {
                gob.update();
            }
            foreach (GameObject gob in deadObjects)
            {
                gameObjects.Remove(gob);
            }
            foreach (GameObject gob in newObjects)
            {
                gameObjects.Add(gob);
                gob.init();
            }


            deadObjects.Clear();
            newObjects.Clear();

        }

        /// <summary>
        /// Adds a GameObject to the world. This is called internally by GameObject. 
        /// </summary>
        /// <param name="gob"></param>
        public virtual void addObject(GameObject gob)
        {
            //gameObjects.Add(gob);
            //gob.init();
            newObjects.Add(gob);
        }

        /// <summary>
        /// Removes a GameObject from the world. 
        /// </summary>
        /// <param name="gob"></param>
        public virtual void removeObject(GameObject gob)
        {
            //gameObjects.Remove(gob);
            deadObjects.Add(gob);
        }

        //todo add a removeObject by ID

        /// <summary>
        /// Get a list of all existing GameObjects
        /// </summary>
        /// <returns>A list of all known GameObjects</returns>
        public virtual List<GameObject> getAllObjects() 
        {
            return gameObjects;
        }

        /// <summary>
        /// Get a list of all GameObjects that are of the specified type. 
        /// </summary>
        /// <typeparam name="G">The specific type of GameObject to search for</typeparam>
        /// <returns>A list of all GameObjects that are of the specified type</returns>
        public virtual List<G> getAllObjectsOfType<G>() where G : GameObject
        {
            List<G> gobs = new List<G>();

            //TODO, make faster? maybe a map.
            gameObjects.ForEach((gob) =>
            {
                if (gob is G)
                {
                    gobs.Add((G)gob);
                }
            });


            return gobs;

        }

        /// <summary>
        /// Get a list of all GameObjects that are of the specified type. 
        /// </summary>
        /// <typeparam name="G">The specific type of GameObject to search for</typeparam>
        /// <returns>A list of all GameObjects that are of the specified type</returns>
        public virtual List<GameObject> getAllObjectsNotOfType<G>() where G : GameObject
        {
            List<GameObject> gobs = new List<GameObject>();

            //TODO, make faster? maybe a map.
            gameObjects.ForEach((gob) =>
            {
                if (!(gob is G))
                {
                    gobs.Add(gob);
                }
            });


            return gobs;

        }



        public virtual State getState()
        {
            State state = new State();
            GameObject[] objs = new GameObject[gameObjects.Count];
            gameObjects.CopyTo(objs);
            state.GameObjects = objs.ToList();
            return state;
        }

        public void setState(State state)
        {
            
        }
    }
}
