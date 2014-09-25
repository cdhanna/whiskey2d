using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Whiskey2D.Core.LogCommands;

namespace Whiskey2D.Core.Inputs
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

        /// <summary>
        /// Create a new ReplayService
        /// </summary>
        /// <param name="logFilePath">A logFile to create a set of input events from</param>
        public ReplayService(string logFilePath)
        {
            this.logFilePath = logFilePath;

        }

        /// <summary>
        /// initializes the ReplayService. This is when the log file is actually loaded into memory.
        /// </summary>
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

        /// <summary>
        /// get the string on a specified line number
        /// THIS WILL THROW A LOGOVEREXCEPTION if the index is greater than the number of lines. Catch this
        /// </summary>
        /// <param name="index">the line number index to read</param>
        /// <returns>the text on the line</returns>
        private string readLine(long index) 
        {
            if (index >= allLines.Length)
            {
                replayOver = true;
                GameManager.getInstance().TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 17);
                throw new LogOverException();
                
            }
            else
            {
                return allLines[index];
            }

        }

        public MouseState getMouseState()
        {
            return new MouseState(); //TODO FIX
        }


        /// <summary>
        /// utility exception
        /// </summary>
        private class LogOverException : Exception
        {
         
        }


    }
}
