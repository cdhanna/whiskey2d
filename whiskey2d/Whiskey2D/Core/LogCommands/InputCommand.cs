using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.Core.LogCommands
{
    class InputCommand : LogCommand
    {

        private List<Keys> keys;
        private long duration;
        public InputCommand(long time, long duration, List<Keys> keys)
            : base(time, "KEY")
        {
            this.keys = keys;
            this.duration = duration;
        }

        public long Duration { get { return this.duration; } }
        public List<Keys> KeysDown { get { return this.keys; } }
        protected override string toCommandText()
        {
            string line = "";
            line += duration + "\t\t# ";
            foreach (Keys key in keys)
            {
                line += key.ToString() + "#";
            }
            return line;
        }

        protected override LogCommand fromCommand(long time, string text)
        {

            string[] parts = text.Split('#');
            string durationPart = parts[0].Trim();

            long d = long.Parse(durationPart);
            List<Keys> ks = new List<Keys>();
            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i] != "")
                {
                    Keys k = (Keys)Enum.Parse(typeof(Keys), parts[i].Trim());
                    ks.Add(k);
                }
            }


            return new InputCommand(time, d, ks);
        }
    }
}
