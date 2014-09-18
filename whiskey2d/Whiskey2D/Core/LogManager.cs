using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Whiskey2D.Core
{
    public class LogManager
    {

        private static LogManager instance = new LogManager();
        public static LogManager getInstance()
        {
            return instance;
        }

        private InputSource inputSource;
        private Dictionary<Keys, bool> oldState, currentState;
        private long tickCount, nothingCount, masterCount;
        private StreamWriter writer;
        private List<Keys> oldActiveKeys, currentActiveKeys;
        private int activeKeyCounter;

        private LogManager()
        {
        }

        public void init(InputSource inputSource)
        {
            this.inputSource = inputSource;
            currentState = inputSource.getAllKeysDown();
            this.tickCount = 0;
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
            //todo write our input tracking
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


                    //if (currentState[key] && !oldState[key]) //key is DOWN for the first time
                    //{
                    //    if (activeKeys.Count == 0)
                    //        writer.WriteLine(masterCount + "| nothing happened for " + nothingCount);
                    //    activeKeys.Add(key);
                    //    tickCount = 0;
                       
                    //}

                    //if (!currentState[key] && oldState[key]) //key is UP for the first time
                    //{
                       
                    //    writer.WriteLine(masterCount + "| "+ activeKeyString() + " was down for " + tickCount);
                    //    activeKeys.Remove(key);

                    //    if( activeKeys.Count == 0)
                    //        nothingCount = 0;
                    //}

                }

            }
            else
            {

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
                writer.WriteLine(masterCount + "\t\t| for: "+activeKeyCounter +"\t| " + activeKeyString());
                activeKeyCounter = 0;
            }
            else
            {
                
            }
           activeKeyCounter++;


            oldActiveKeys.Clear();
            currentActiveKeys.ForEach((k) => { oldActiveKeys.Add(k); });

            oldActiveKeys.Sort();

            tickCount++;
            nothingCount++;
            masterCount++;
        }

        private string activeKeyString()
        {
            string allKeys = "";
            oldActiveKeys.ForEach((k) => { allKeys += k.ToString() + "#"; });
            return allKeys;
        }

        private Boolean isKeyDown(Keys key)
        {
            return currentState[key];
        }

        private Boolean isNewKeyDown(Keys key)
        {
            return currentState[key] && !oldState[key];
        }

    }
}
