using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;

using Whiskey2D.Core.LogCommands;
using Whiskey2D.Core.Hud;

namespace Whiskey2D.Core.Managers.Impl
{
    /// <summary>
    /// The logManager is responsible for logging any user messages. 
    /// The logger is also capable of tracking all input events, and creating a log file that can be replayed in the future
    /// </summary>
    public class DefaultLogManager : LogManager
    {
       

        /// <summary>
        /// an empty input source for the LogManager to use if no valid inputsource is given
        /// </summary>
        private class EmptyInput : InputSource
        {
            public void init()
            {
            }
            public Dictionary<Keys, bool> getAllKeysDown()
            {
                return new Dictionary<Keys, bool>();
            }
        }



        private const string COMMAND_DELIM = "|";


        private static DefaultLogManager instance = new DefaultLogManager();
        public static DefaultLogManager getInstance()
        {
            return instance;
        }

        private Dictionary<Keys, bool> oldState, currentState;
        private long masterCount;
        private StreamWriter writer;
        private List<Keys> oldActiveKeys, currentActiveKeys;
        private long activeKeyCounter;

        private InputSourceManager sourceMan;
      

        private DefaultLogManager()
        {
        }

        /// <summary>
        /// Initializes the LogManager. This is when the log file is created.
        /// </summary>
        public void init()
        {

            sourceMan = GameManager.InputSource;
            currentState = sourceMan.getSource().getAllKeysDown();
            oldActiveKeys = new List<Keys>();
            currentActiveKeys = new List<Keys>();
            oldActiveKeys.Clear();
            currentActiveKeys.Clear();
            activeKeyCounter = 0;
            masterCount = 0;

            writer = File.CreateText( getCurrentLogPath() );
            writer.AutoFlush = true;
            
            this.writeCommand(new RandCommand(-1, Rand.getInstance().getSeed()));
        }

        /// <summary>
        /// Close the LogManager
        /// </summary>
        public void close()
        {
            writer.Close();

            File.Delete(getOldLogPath());
            File.Copy(getCurrentLogPath(), getOldLogPath());
        }

        public string getCurrentLogPath()
        {
            return "whiskey_log_current.txt";
        }
        public string getOldLogPath()
        {
            return "whiskey_log_last.txt";
        }

        private string generateLogPath()
        {
            string name = "whiskey_";

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            name += secondsSinceEpoch;
            name += ".txt";

            return name;

        }


        /// <summary>
        /// updates the LogManager, so that input may be tracked
        /// </summary>
        public void update()
        {
            
            oldState = currentState;
            currentState = sourceMan.getSource().getAllKeysDown();

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
            LogMessage msg = new LogMessage(masterCount, LogLevel.DEBUG, message);
            writeCommand(msg);
            HudManager.getInstance().writeMessage(msg);
        }

        /// <summary>
        /// Writes an error message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void error(string message)
        {
            LogMessage msg = new LogMessage(masterCount, LogLevel.ERROR, message);
            writeCommand(msg);
            HudManager.getInstance().writeMessage(msg);
        }

        /// <summary>
        /// Writes a warning message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void warning(string message)
        {
            LogMessage msg = new LogMessage(masterCount, LogLevel.WARNING, message);
            writeCommand(msg);
            HudManager.getInstance().writeMessage(msg);
        }

        /// <summary>
        /// Writes a release message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        public void release(string message)
        {
            LogMessage msg = new LogMessage(masterCount, LogLevel.RELEASE, message);
            writeCommand(msg);
            HudManager.getInstance().writeMessage(msg);
        }

    }
}
