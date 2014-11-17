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

        public State()
        {
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
            fileName += ".state";
            GameManager.Log.debug("State loading : " + fileName);
            if (!File.Exists(fileName))
            {
                return null;
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
                GameManager.Log.error("Cannot deserialize");
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            GameManager.Log.debug("State has  " + state.GameObjects.Count);
            return state;
            //return (State)s.Deserialize(fileName);
        }

    }
}
