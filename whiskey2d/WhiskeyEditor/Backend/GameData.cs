using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class GameData
    {

        public List<FileDescriptor> Files { get; set; }
        
        public GameData()
        {
            Files = new List<FileDescriptor>();
            
        }


        public static readonly object gameDataLocker = new Object();
        public static string serialize(GameData data, string filePath)
        {
            lock (gameDataLocker)
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);

                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, data);
                }
                catch (SerializationException e)
                {
                    throw new WhiskeyException("Could not serialize game data " + e.Message);
                }
                finally
                {
                    fs.Close();
                }
            
                return filePath;
            }
        }

        public static GameData deserialize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new WhiskeyException("Could not deserialize game data " + filePath);
            }

            // Open the file containing the data that you want to deserialize.
            GameData data;
            FileStream fs = new FileStream(filePath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (GameData)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                throw new WhiskeyException("Could not deserialize " + filePath + " :: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
            return data;
        }


    }
}
