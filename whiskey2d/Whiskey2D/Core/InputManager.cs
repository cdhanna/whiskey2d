using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace Whiskey2D.Core
{

    /// <summary>
    /// Manages all input for the game. Only keyboard input is supported
    /// </summary>
    public class InputManager
    {

        private static InputManager instance;

        /// <summary>
        /// retrieves the InputManager
        /// </summary>
        /// <returns>The InputManager</returns>
        public static InputManager getInstance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }


        //private KeyboardState currentState, oldState;

        private InputSourceManager sourceMan;

        private Dictionary<Keys, bool> currentState, oldState;

        private InputManager()
        {
        }

        /// <summary>
        /// Initializes the InputManager
        /// </summary>
        public void init()
        {
            sourceMan = InputSourceManager.getInstance();

            
            currentState = sourceMan.getSource().getAllKeysDown();
        }

        /// <summary>
        /// Closes the InputManager
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Updates the InputManager, allowing keyboard events to be heard and registered
        /// </summary>
        public void update()
        {
            oldState = currentState;
            currentState = sourceMan.getSource().getAllKeysDown();

            //oldState = currentState;
            //currentState = Keyboard.GetState();
            
        }

        /// <summary>
        /// Determines if a single key is being pressed at the moment
        /// </summary>
        /// <param name="key">A key to check if it is down</param>
        /// <returns>True if the key is down, false otherwise</returns>
        public Boolean isKeyDown(Keys key)
        {
            return currentState[key];
           // return currentState.IsKeyDown(key);
        }

        /// <summary>
        /// Determines if a single key has been newly pressed. Holding down a key will only trigger this method once.
        /// </summary>
        /// <param name="key">A key to check if it is newly down</param>
        /// <returns>True if the key was just pressed, false otherwise</returns>
        public Boolean isNewKeyDown(Keys key)
        {
            return currentState[key] && !oldState[key];
            //return (currentState.IsKeyDown(key) && oldState.IsKeyUp(key));
        }
    }
}
