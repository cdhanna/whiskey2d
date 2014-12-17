using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;

namespace Whiskey2D.Core
{

    [Serializable]
    public class Level : DefaultObjectManager
    {

        public Color BackgroundColor { get; set; }
        public string Name { get; private set; }

        public Level(string name)
            : base()
        {
            Name = name;
        }

        public static string serialize(Level lvl, string fileName)
        {

            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, lvl);
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

        public static Level deserialize(string fileName)
        {
            // GameManager.Log.debug("State loading : " + fileName);
            if (!File.Exists(fileName))
            {
                throw new WhiskeyRunTimeException("Level does not exist : " + fileName);
            }

            // Open the file containing the data that you want to deserialize.
            Level lvl;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                lvl = (Level)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                throw new WhiskeyRunTimeException("Failed to deserialize. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
            return lvl;
        }

    }
}
