using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Managers
{
    /// <summary>
    /// An ObjectManager contains a set of GameObjects. Primarily, the ObjectManager updates all of its GameObjects.
    /// </summary>
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

        /// <summary>
        /// Get the default name for a GameObject. The Default name should be determined from the GameObject's type, and the number of GameObjects existing in the ObjectManager
        /// </summary>
        /// <param name="gob">Some GameObject</param>
        /// <returns>A name</returns>
        string getDefaultNameFor(GameObject gob);

        /// <summary>
        /// Gets a list of all existing GameObejcts, that are NOT of the given type
        /// </summary>
        /// <typeparam name="G">Some GameObject type</typeparam>
        /// <returns>A list of GameObjects</returns>
        List<GameObject> getAllObjectsNotOfType<G>() where G : GameObject;

        /// <summary>
        /// Gets a list of all existing GameObjects, that are of the given type
        /// </summary>
        /// <typeparam name="G">Some GameObejct type</typeparam>
        /// <returns>a list of GameObjects</returns>
        List<G> getAllObjectsOfType<G>() where G : GameObject;

        /// <summary>
        /// Get a GameObject by the given name
        /// </summary>
        /// <param name="name">The name of some GameObject</param>
        /// <returns>The GameObject whose name is equal to the given name, or null, if there is no GameObject by that name</returns>
        GameObject getObject(string name);
        
        /// <summary>
        /// Get a GameObject by the given name, and type
        /// </summary>
        /// <typeparam name="G">The type of the GameObejct</typeparam>
        /// <param name="name">The name of the GameObject</param>
        /// <returns>The GameObject whose name is equal to the given name, or null, if there is no GameObject by that name</returns>
        G getObject<G>(string name) where G : GameObject;

        /// <summary>
        /// Get a State, representing the entire ObjectManager
        /// </summary>
        /// <returns>a new State</returns>
        State getState();

        /// <summary>
        /// Set the State of the ObjectManager. Doing so will remove all GameObjects, and replace them with the GameObjects found in the State
        /// </summary>
        /// <param name="state"></param>
        void setState(State state);

    }
}
