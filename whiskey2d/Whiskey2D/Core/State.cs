using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Whiskey2D.Core
{
    [Serializable]
    public class State
    {

        public List<GameObject> GameObjects { get; set; }

        public string Name { get; set; }
        public Color BackgroundColor { get; set; }

        public State()
        {
            GameObjects = new List<GameObject>();
            BackgroundColor = Color.Orange;
            Name = "unnamed";
        }

        public static string serialize(State state, string fileName)
        {

            // To serialize the hashtable and its key/value pairs,   
            // you must first open a stream for writing.  
            // In this case, use a file stream.
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
               

                Vector v = new Vector(2, 4);

                formatter.Serialize(fs, state);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return fileName;
           
        }

        public static State deserialize(string fileName)
        {
           // GameManager.Log.debug("State loading : " + fileName);
            if (!File.Exists(fileName))
            {
                throw new WhiskeyRunTimeException("State does not exist : " + fileName);
            }

            // Open the file containing the data that you want to deserialize.
            State state;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                state = (State)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
             //   GameManager.Log.error("Cannot deserialize");
                throw new WhiskeyRunTimeException("Failed to deserialize. Reason: " + e.Message);
                
            }
            finally
            {
                fs.Close();
            }
            return state;
            //return (State)s.Deserialize(fileName);
        }

    }
}
