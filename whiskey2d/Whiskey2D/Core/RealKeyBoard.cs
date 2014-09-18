using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.Core
{
    class RealKeyBoard : InputSource
    {


        public Dictionary<Keys, bool> getAllKeysDown()
        {

            KeyboardState state = Keyboard.GetState();

            Keys[] all = (Keys[])Enum.GetValues(typeof(Keys));
            Dictionary<Keys, bool> keyMap = new Dictionary<Keys, bool>();
            foreach (Keys key in all)
            {
                keyMap.Add(key, state.IsKeyDown(key));
            }


            return keyMap;
        }
    }
}
