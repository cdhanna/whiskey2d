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
    /// The logManager is responsible for logging any user messages. 
    /// The logger is also capable of tracking all input events, and creating a log file that can be replayed in the future
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// A level of importance placed upon log messages.
        /// </summary>
        public enum LogLevel
        {
            DEBUG,
            ERROR,
            RELEASE,
            WARNING
        }

        /// <summary>
        /// an empty input source for the LogManager to use if no valid inputsource is given
        /// </summary>
        private class EmptyInput : InputSource
        {
            public Dictionary<Keys, bool> getAllKeysDown()
            {
                return new Dictionary<Keys, bool>();
            }
        }



        private const string COMMAND_DELIM = "|";


        private static LogManager instance = new LogManager();
        public static LogManager getInstance()
        {
            return instance;
        }

        private InputSource inputSource;
        private Dictionary<Keys, bool> oldState, currentState;
        private long masterCount;
        private StreamWriter writer;
        private List<Keys> oldActiveKeys, currentActiveKeys;
        private long activeKeyCounter;

        private LogManager()
        {
        }

        /// <summary>
        /// Init the LogManager with an input source.
        /// </summary>
        /// <param name="inputSource">This input source will be probed to generate a replayable log file. If the inputsource is given as null, then no replayable log will be created</param>
        public void init(InputSource inputSource)
        {
            if (inputSource == null)
            {
                inputSource = new EmptyInput();
            }
            this.inputSource = inputSource;
            currentState = inputSource.getAllKeysDown();
            writer = File.CreateText( "whiskey.txt"); //TODO make naming unique between runs
            writer.AutoFlush = true;
            oldActiveKeys = new List<Keys>();
            currentActiveKeys = new List<Keys>();
        }

        /// <summary>
        /// Close the LogManager
        /// </summary>
        public void close()
        {
            writer.Close();
        }

        /// <summary>
        /// updates the LogManager, so that input may be tracked
        /// </summary>
        public void update()
        {
            
            oldState = currentState;
            currentState = inputSource.getAllKeysDown();

            currentActiveKeys.Clear();
            
            if (oldState != currentState)
            {
                Keys[] all = (Keys[])Enum.GetValues(typeof(Keys));
                foreach (Keys key in all)
                {

                    if (currentState[key])
                    {
                        currentActiveKeys.Add(key);
                    }
                }
            }


            currentActiveKeys.Sort();
            bool listsEqual = true;
            if (currentActiveKeys.Count == oldActiveKeys.Count)
            {
                for (int i = 0; i < oldActiveKeys.Count; i++)
                {
                    if (currentActiveKeys[i] != oldActiveKeys[i])
                    {
                        listsEqual = false;
                        break;
                    }
                }
            }
            else
            {
                listsEqual = false;
            }

            if (!listsEqual)
            {
                writeCommand(new InputCommand(masterCount, activeKeyCounter, oldActiveKeys));
                activeKeyCounter = 0;
            }
            else
            {
                
            }
           activeKeyCounter++;


            oldActiveKeys.Clear();
            currentActiveKeys.ForEach((k) => { oldActiveKeys.Add(k); });

            oldActiveKeys.Sort();

            masterCount++;
        }

        
        /// <summary>
        /// writes an arbitrary logcommand to the log file
        /// </summary>
        /// <param name="command">Some LogCommand</param>
        private void writeCommand(LogCommand command)
        {
            string line = command.toCommand();
            writer.WriteLine(line);
        }

        /// <summary>
        /// Writes a debug message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void debug(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.DEBUG, message));
        }

        /// <summary>
        /// Writes an error message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void error(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.ERROR, message));
        }

        /// <summary>
        /// Writes a warning message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void warning(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.WARNING, message));
        }

        /// <summary>
        /// Writes a release message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void release(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.RELEASE, message));
        }

    }
}
