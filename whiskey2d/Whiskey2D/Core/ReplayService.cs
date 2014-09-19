using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Whiskey2D.Core.LogCommands;

namespace Whiskey2D.Core
{
    class ReplayService : InputSource
    {
        int totalTicks;
        
        string[] allLines;
        long lineNumber;
        long updatesLeft;
        public ReplayService(string logFilePath)
        {
            allLines = File.ReadAllLines(logFilePath);
            updatesLeft = getUpdateCount();
        }

        public void update()
        {
            

            

            if (totalTicks >= updatesLeft)
            {
                lineNumber += (lineNumber < allLines.Length-1 ? 1 : 0);
                totalTicks = 0;
                updatesLeft = getUpdateCount();
            }
            totalTicks++;
        }

        private List<Keys> getKeysOnLine( )
        {
            string line = allLines[lineNumber];

            LogCommand command = LogCommand.parse(line);
            while (!(command is InputCommand))
            {
                command = LogCommand.parse(allLines[++lineNumber]);
            }

            InputCommand io = (InputCommand)command;
            return io.KeysDown;
            //string line = allLines[fileLineNumber];
            //string[] components = line.Split('|');

            //string right = components[2].Trim();
            //string[] keys = right.Split('#');

            //List<Keys> allKeys = new List<Keys>();

            //if (keys.Length > 0)
            //{
            //    foreach (String key in keys)
            //    {
            //        if (key != "")
            //        {
            //            Keys k = (Keys)Enum.Parse(typeof(Keys), key.Trim());
            //            allKeys.Add(k);
            //        }
            //    }
            //}

            //return allKeys;
        }

        private long getUpdateCount()
        {
            string line = allLines[lineNumber];

            LogCommand command = LogCommand.parse(line);
            while (! (command is InputCommand) )
            {
                command = LogCommand.parse(allLines[++lineNumber]);
            }

            InputCommand io = (InputCommand)command;
            return io.Duration;
            //string[] components = line.Split('|');

            //string middle = components[1].Trim().Substring("for: ".Length);
            //return int.Parse(middle);

        }

        public Dictionary<Keys, bool> getAllKeysDown()
        {
            
           
            List<Keys> downed = getKeysOnLine(  );
            Keys[] all = (Keys[])Enum.GetValues(typeof(Keys));
            Dictionary<Keys, bool> keyMap = new Dictionary<Keys, bool>();
            foreach (Keys key in all)
            {
                keyMap.Add(key, downed.Contains(key) );
            }


            return keyMap;
        }
    }
}
