using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Managers
{

    public interface ObjectManager
    {

        /// <summary>
        /// Initializes the ObjectManager
        /// </summary>
        void init();
        /// <summary>
        /// Closes out the ObjectManager
        /// </summary>
        void close();

        /// <summary>
        /// Updates all Game Objects
        /// </summary>
        void updateAll();

        /// <summary>
        /// Adds a GameObject to the world. This is called internally by GameObject. 
        /// </summary>
        /// <param name="gob"></param>
        void addObject(GameObject gob);
        /// <summary>
        /// Removes a GameObject from the world. 
        /// </summary>
        /// <param name="gob"></param>
        void removeObject(GameObject gob);

        //todo add a removeObject by ID

        /// <summary>
        /// Get a list of all existing GameObjects
        /// </summary>
        /// <returns>A list of all known GameObjects</returns>
        List<GameObject> getAllObjects();


        string getDefaultNameFor(GameObject gob);

        List<GameObject> getAllObjectsNotOfType<G>() where G : GameObject;

        List<G> getAllObjectsOfType<G>() where G : GameObject;


        GameObject getObject(string name);
        G getObject<G>(string name) where G : GameObject;

        State getState();

        void setState(State state);

    }
}
