using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.Core.LogCommands
{
    /// <summary>
    /// The InputCommand tells the log what keys from the inputsource are being pressed.
    /// </summary>
    class InputCommand : LogCommand
    {

        private List<Keys> keys;
        private long duration;
        private MouseState mouseState;

        /// <summary>
        /// Create a new InputCommand
        /// </summary>
        /// <param name="time">The time at which the Command happened</param>
        /// <param name="duration">the duration of the input event</param>
        /// <param name="keys">the keys that are being pressed</param>
        /// <param name="mouseState">the mouseState at the moment</param>
        public InputCommand(long time, long duration, List<Keys> keys, MouseState mouseState)
            : base(time, "KEY")
        {
            this.keys = keys;
            this.duration = duration;
            this.mouseState = mouseState;

        }

        /// <summary>
        /// The duration of the event
        /// </summary>
        public long Duration { get { return this.duration; } }
        
        /// <summary>
        /// The keys that are down for the event
        /// </summary>
        public List<Keys> KeysDown { get { return this.keys; } }

        public MouseState MouseState { get { return this.mouseState; } }


        protected override string toCommandText()
        {
            string line = "";
            string delim = " # ";
            line += duration + "\t\t" + delim + " ";
            foreach (Keys key in keys)
            {
                line += key.ToString() + delim;
            }
            line += " * ";

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                line += "LM" + delim;
            }
            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                line += "MM" + delim;
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                line += "RM" + delim;
            }
            line += " * ";

            line += "MX: " + mouseState.X + delim;
            line += "MY: " + mouseState.Y + delim;

            return line;
        }

        protected override LogCommand fromCommand(long time, string text)
        {

            string[] masterParts = text.Split('*');






            string[] parts = masterParts[0].Split('#');
            string durationPart = parts[0].Trim();
            long d = long.Parse(durationPart);
            List<Keys> ks = new List<Keys>();
            for (int i = 1; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
                if (parts[i] != "")
                {
                    Keys k = (Keys)Enum.Parse(typeof(Keys), parts[i].Trim());
                    ks.Add(k);
                }
            }

            ButtonState lmState = ButtonState.Released;
            ButtonState mmState = ButtonState.Released;
            ButtonState rmState = ButtonState.Released;

            string[] mouseButtonsPart = masterParts[1].Split('#');
            for (int i = 0; i < mouseButtonsPart.Length; i++)
            {
                string mouseButton = mouseButtonsPart[i].Trim();
                if (mouseButton.Equals("LM"))
                {
                    lmState = ButtonState.Pressed;
                }
                else if (mouseButton.Equals("MM"))
                {
                    mmState = ButtonState.Pressed;
                }
                else if (mouseButton.Equals("RM"))
                {
                    rmState = ButtonState.Pressed;
                }
            }


            int mx = 0;
            int my = 0;
            string[] mouseLocPart = masterParts[2].Split('#');
            for (int i = 0; i < mouseLocPart.Length; i++)
            {
                string mouseLoc = mouseLocPart[i].Trim();
                if (mouseLoc.StartsWith("MX:"))
                {
                    mouseLoc = mouseLoc.Replace("MX: ", "").Trim();
                    mx = int.Parse(mouseLoc);
                }
                else if (mouseLoc.StartsWith("MY:"))
                {
                    mouseLoc = mouseLoc.Replace("MY: ", "").Trim();
                    my = int.Parse(mouseLoc);
                }
            }

            MouseState mouseState = new MouseState(mx, my, 0, lmState, mmState, rmState, ButtonState.Released, ButtonState.Released);


            return new InputCommand(time, d, ks, mouseState);
        }
    }
}
