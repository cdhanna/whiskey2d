using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// Manages all GameObjects in the Whiskey Game. 
    /// </summary>
    public class ObjectManager
    {
        private static ObjectManager instance;

        /// <summary>
        /// Retrieves the ObjectManager
        /// </summary>
        /// <returns>The ObjectManager</returns>
        public static ObjectManager getInstance()
        {
            if (instance == null)
            {
                instance = new ObjectManager();
            }
            return instance;
        }

        private List<GameObject> gameObjects;

        private ObjectManager()
        {
           
        }

        /// <summary>
        /// Initializes the ObjectManager
        /// </summary>
        public void init()
        {
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Closes out the ObjectManager
        /// </summary>
        public void close()
        {

        }

        /// <summary>
        /// Updates all Game Objects
        /// </summary>
        public void updateAll()
        {
            foreach (GameObject gob in gameObjects)
            {
                gob.update();
            }
        }

        /// <summary>
        /// Adds a GameObject to the world. This is called internally by GameObject. 
        /// </summary>
        /// <param name="gob"></param>
        public void addObject(GameObject gob)
        {
            gameObjects.Add(gob);
            gob.init();
        }

        /// <summary>
        /// Removes a GameObject from the world. 
        /// </summary>
        /// <param name="gob"></param>
        public void removeObject(GameObject gob)
        {
            gameObjects.Remove(gob);
        }

        //todo add a removeObject by ID

        /// <summary>
        /// Get a list of all existing GameObjects
        /// </summary>
        /// <returns>A list of all known GameObjects</returns>
        public List<GameObject> getAllObjects() 
        {
            return gameObjects;
        }

        /// <summary>
        /// Get a list of all GameObjects that are of the specified type. 
        /// </summary>
        /// <typeparam name="G">The specific type of GameObject to search for</typeparam>
        /// <returns>A list of all GameObjects that are of the specified type</returns>
        public List<G> getAllObjectsOfType<G>() where G : GameObject
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


    }
}
