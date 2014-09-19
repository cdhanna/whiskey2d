using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Whiskey2D.Core.LogCommands;

namespace Whiskey2D.Core
{
    public class LogManager
    {
        public enum LogLevel
        {
            DEBUG,
            ERROR,
            RELEASE,
            WARNING
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

        public void init(InputSource inputSource)
        {
            this.inputSource = inputSource;
            currentState = inputSource.getAllKeysDown();
            writer = File.CreateText( "whiskey.txt"); //TODO make naming unique between runs
            oldActiveKeys = new List<Keys>();
            currentActiveKeys = new List<Keys>();
        }

        public void close()
        {
            writer.Close();
        }

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

        

        private void writeCommand(LogCommand command)
        {
            string line = command.toCommand();
            writer.WriteLine(line);
        }


        public void debug(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.DEBUG, message));
        }
        public void error(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.ERROR, message));
        }
        public void warning(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.WARNING, message));
        }
        public void release(string message)
        {
            writeCommand(new LogMessage(masterCount, LogLevel.RELEASE, message));
        }

    }
}
