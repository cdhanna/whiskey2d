using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Whiskey2D.Core.Inputs;

namespace Whiskey2D.Core.Managers.Impl
{

    /// <summary>
    /// Manages all input for the game. Only keyboard input is supported
    /// </summary>
    [Serializable]
    public class DefaultInputManager : InputManager
    {

        //private static DefaultInputManager instance;

        ///// <summary>
        ///// retrieves the InputManager
        ///// </summary>
        ///// <returns>The InputManager</returns>
        //public static DefaultInputManager getInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new DefaultInputManager();
        //    }
        //    return instance;
        //}


        private InputSourceManager sourceMan;

        private Dictionary<Keys, bool> currentState, oldState;
        private MouseState currentMouse, oldMouse;


        public DefaultInputManager()
        {
        }

        /// <summary>
        /// Initializes the InputManager
        /// </summary>
        public void init()
        {
            init(GameManager.InputSource);

           
        }
        public void init(InputSourceManager source)
        {
            sourceMan = source;

            currentMouse = sourceMan.getSource().getMouseState();
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
            oldMouse = currentMouse;

            currentMouse = sourceMan.getSource().getMouseState();
            currentState = sourceMan.getSource().getAllKeysDown();
        }

        /// <summary>
        /// Determines if a single key is being pressed at the moment
        /// </summary>
        /// <param name="key">A key to check if it is down</param>
        /// <returns>True if the key is down, false otherwise</returns>
        public Boolean isKeyDown(Keys key)
        {
            return currentState[key];
        }

        /// <summary>
        /// Determines if a single key has been newly pressed. Holding down a key will only trigger this method once.
        /// </summary>
        /// <param name="key">A key to check if it is newly down</param>
        /// <returns>True if the key was just pressed, false otherwise</returns>
        public Boolean isNewKeyDown(Keys key)
        {
           
            return currentState[key] && !oldState[key];
        }


        public int getMouseWheelDelta()
        {

            return currentMouse.ScrollWheelValue;
            
        }

        public bool scrolledUp()
        {
            return currentMouse.ScrollWheelValue > oldMouse.ScrollWheelValue;
        }
        public bool scrolledDown()
        {
            return currentMouse.ScrollWheelValue < oldMouse.ScrollWheelValue;
        }


        public Vector getMousePosition()
        {
            return new Vector(currentMouse.Position.X, currentMouse.Position.Y);
        }


        private bool mouseDown(MouseButtons b, MouseState state)
        {
            bool answer = false;
            switch (b)
            {
                case MouseButtons.Left:
                    answer = state.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Right:
                    answer = state.RightButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Middle:
                    answer = state.MiddleButton == ButtonState.Pressed;
                    break;
                default:
                    break;
            }
            return answer;
        }

        public bool isMouseDown(MouseButtons b)
        {
            return mouseDown(b, currentMouse);
        }

        public bool isNewMouseDown(MouseButtons b)
        {
            return mouseDown(b, currentMouse) && !mouseDown(b, oldMouse);
        }
    }
}
