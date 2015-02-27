using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.Core.Inputs
{
    /// <summary>
    /// An InputSource that gets input directly from the keyboard
    /// </summary>
    class RealKeyBoard : InputSource
    {

        private static Dictionary<Keys, String> ks = new Dictionary<Keys, string>();

        static RealKeyBoard()
        {

           
            ks.Add(Keys.OemPeriod, ".");
         
            ks.Add(Keys.A, "a");
            ks.Add(Keys.B, "b");
            ks.Add(Keys.C, "c");
            ks.Add(Keys.D, "d");
            ks.Add(Keys.E, "e");
            ks.Add(Keys.F, "f");
            ks.Add(Keys.G, "g");
            ks.Add(Keys.H, "h");
            ks.Add(Keys.I, "i");
            ks.Add(Keys.J, "j");
            ks.Add(Keys.K, "k");
            ks.Add(Keys.L, "l");
            ks.Add(Keys.M, "m");
            ks.Add(Keys.N, "n");
            ks.Add(Keys.O, "o");
            ks.Add(Keys.P, "p");
            ks.Add(Keys.Q, "q");
            ks.Add(Keys.R, "r");
            ks.Add(Keys.S, "s");
            ks.Add(Keys.T, "t");
            ks.Add(Keys.U, "u");
            ks.Add(Keys.V, "v");
            ks.Add(Keys.W, "w");
            ks.Add(Keys.X, "x");
            ks.Add(Keys.Y, "y");
            ks.Add(Keys.Z, "z");
            ks.Add(Keys.D0, "0");
            ks.Add(Keys.D1, "1");
            ks.Add(Keys.D2, "2");
            ks.Add(Keys.D3, "3");
            ks.Add(Keys.D4, "4");
            ks.Add(Keys.D5, "5");
            ks.Add(Keys.D6, "6");
            ks.Add(Keys.D7, "7");
            ks.Add(Keys.D8, "8");
            ks.Add(Keys.D9, "9");
            ks.Add(Keys.Space, " ");
        }

        /// <summary>
        /// initializes the keyboard
        /// </summary>
        public void init()
        {
            //do nothing
        }

        /// <summary>
        /// Get all keys that are being pressed.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Keys, bool> AllDownKeys
        {
            get
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

        public MouseState MouseState
        {
            get
            {
                MouseState state = Mouse.GetState();

                return state;
            }
        }


        /// <summary>
        /// Utility function to map a Keys enum to a string. 
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static String keyToString(Keys k, bool isShift)
        {
            String s = "";
            if (ks.ContainsKey(k))
                s =  ks[k];

            if (isShift)
                s = s.ToUpper();

            return s;
        }

    }
}
