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

        public State()
        {

        }
        static Polenter.Serialization.SharpSerializer s = new Polenter.Serialization.SharpSerializer();
        public static void serialize(State state, string fileName)
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

            //s = new Polenter.Serialization.SharpSerializer();
            //s.Serialize(state, fileName);

            //string delim = "#";
            //string nl = Environment.NewLine;
            //string ser = "";


            ////pass one. Create references
            //for (int i = 0; i < state.GameObjects.Count; i++)
            //{
            //    GameObject gob = state.GameObjects[i];
            //    ser += gob.ID + delim;
            //}


            //for (int i = 0; i < state.GameObjects.Count; i++)
            //{
            //    GameObject gob = state.GameObjects[i];
            //    ser += nl + "gob"+delim+gob.ID+delim;
            //    ser += gob.GetType()+delim;
            //    Type gobType = gob.GetType();
            //    List<FieldInfo> fields = gobType.GetFields().ToList();
            //    fields.ForEach((f) =>
            //    {
            //        ser += nl + f.Name;
            //    });
            //}

           
        }

        public static State deserialize(string fileName)
        {
            //FileStream fs = new FileStream(fileName, FileMode.Open);
            //StreamReader reader = new StreamReader(fs);

            //string json = reader.ReadToEnd();
            //State state = Newtonsoft.Json.JsonConvert.DeserializeObject<State>(json);

            //fs.Close();
            //return state;

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
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
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
