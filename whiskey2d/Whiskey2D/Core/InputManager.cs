using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace Whiskey2D.Core
{
    public class InputManager
    {

        private static InputManager instance;
        public static InputManager getInstance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }


        private KeyboardState currentState, oldState;

        private InputManager()
        {
        }

        public void init()
        {
            currentState = Keyboard.GetState();
        }

        public void close()
        {
        }

        public void update()
        {
            oldState = currentState;
            currentState = Keyboard.GetState();
            
        }

        public Boolean isKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public Boolean isNewKeyDown(Keys key)
        {
            return (currentState.IsKeyDown(key) && oldState.IsKeyUp(key));
        }
    }
}
