using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Whiskey2D.Core.LogCommands;

namespace Whiskey2D.Core
{
    /// <summary>
    /// The ReplayService is an InputSource and creates input events from a log file
    /// </summary>
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

            RandCommand rc = (RandCommand) LogCommand.parse(allLines[0]);
            Rand.getInstance().setSeed(rc.Seed);


            lineNumber = 1;

        }

        /// <summary>
        /// Updates the replay service. This will read through the log file and determine what keys ought to be down at the moment
        /// </summary>
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

        /// <summary>
        /// Gets all of the keys on a line in the log file. Reads the log file until a valid command is found
        /// </summary>
        /// <returns></returns>
        private List<Keys> getKeysOnLine()
        {
            string line = allLines[lineNumber];

            LogCommand command = LogCommand.parse(line);
            while (!(command is InputCommand))
            {
                command = LogCommand.parse(allLines[++lineNumber]);
            }

            InputCommand io = (InputCommand)command;
            return io.KeysDown;
            
        }

        /// <summary>
        /// Gets the number of times to spend on the current line in the log file
        /// </summary>
        /// <returns></returns>
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
           
        }

        /// <summary>
        /// part of the InputSource interface. Returns a set of all keys that should be down at the current moment.
        /// </summary>
        /// <returns></returns>
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
