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

        string logFilePath;

        bool replayOver = false;

        public bool ReplayOver { get { return this.replayOver; } }

        public ReplayService(string logFilePath)
        {
            this.logFilePath = logFilePath;

        }

        public void init()
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
           
            if (totalTicks >= updatesLeft && !replayOver)
            {
                lineNumber++;
                
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
            try
            {
                string line = readLine(lineNumber);

                LogCommand command = LogCommand.parse(line);
                while (!(command is InputCommand))
                {
                    lineNumber++;
                    command = LogCommand.parse(readLine(lineNumber));

                }

                InputCommand io = (InputCommand)command;
                return io.KeysDown;
            }
            catch (LogOverException e)
            {
                replayOver = true;
                return new List<Keys>();
            }
           
        }

        /// <summary>
        /// Gets the number of times to spend on the current line in the log file
        /// </summary>
        /// <returns></returns>
        private long getUpdateCount()
        {
            try
            {
                string line = readLine(lineNumber);

                LogCommand command = LogCommand.parse(line);
                while (!(command is InputCommand))
                {
                    lineNumber++;
                    command = LogCommand.parse(readLine(lineNumber));
                }


                InputCommand io = (InputCommand)command;
                return io.Duration;
            }
            catch (LogOverException e)
            {
                
                replayOver = true;
                return -1;
            }
           
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


        private string readLine(long index) 
        {
            if (index >= allLines.Length)
            {
                replayOver = true;
                throw new LogOverException();
                
            }
            else
            {
                return allLines[index];
            }

        }

        private class LogOverException : Exception
        {
            public LogOverException()
            {
                

            }
        }


    }
}
