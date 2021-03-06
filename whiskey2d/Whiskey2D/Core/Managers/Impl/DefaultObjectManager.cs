﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Managers.Impl
{

    /// <summary>
    /// Manages all GameObjects in the Whiskey Game. 
    /// </summary>
    [Serializable]
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
        private int objectCounter;
        

        public DefaultObjectManager()
        {
            objectCounter = 0;
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
            List<GameObject> didntDie = new List<GameObject>();

            foreach (GameObject gob in gameObjects.Where( g => g.Active ))
            {
                gob.update();
            }
            foreach (GameObject gob in deadObjects)
            {
                if (!gameObjects.Remove(gob))
                {
                    didntDie.Add(gob);
                }
            }


            List<GameObject> gobsThatWereNew = new List<GameObject>();
            foreach (GameObject gob in newObjects)
            {
                gameObjects.Add(gob);
                gobsThatWereNew.Add(gob);
                
            }
            newObjects.Clear();

            gobsThatWereNew.ForEach(g => g.init());


            deadObjects.Clear();
            deadObjects.AddRange(didntDie);
            didntDie.Clear();

            
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
            gob.Name = getDefaultNameFor(gob);
        }

        public string getDefaultNameFor(GameObject gob)
        {
            int counter = 1;
            string defaultName = gob.getTypeName() + counter;
            
            GameObject alreadyExists = getObject(defaultName);
            while (alreadyExists != null)
            {
                counter++;
                defaultName = gob.getTypeName() + counter;
                alreadyExists = getObject(defaultName);
            }

            return defaultName;
            //return gob.getTypeName() + (objectCounter++);
        }
        public GameObject getObject(string name)
        {
            foreach (GameObject gob in getAllObjects())
            {
                if (gob.Name.Equals(name))
                {
                    return gob;
                }
            }
           // GameManager.Log.error("Object Manager could not find : " + name);
            return null;
        }
        public G getObject<G>(string name) where G : GameObject
        {
            GameObject gob = getObject(name);
            if (gob.getTypeName().Equals(typeof(G).Name))
                return (G)getObject(name);
            else
                return null;
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

        public virtual void setState(State state)
        {

            if (state == null)
            {
                return;
            }

            close();
            GameObject[] objs = new GameObject[state.GameObjects.Count];
            state.GameObjects.CopyTo(objs);
            newObjects = objs.ToList();
            
        }
    }
}
