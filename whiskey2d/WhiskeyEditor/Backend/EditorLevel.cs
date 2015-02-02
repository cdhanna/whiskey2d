using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using WhiskeyEditor.Backend.Managers;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class EditorLevel : DefaultObjectManager
    {

        private Layer defaultLayer = new Layer("Default");

        //public List<InstanceDescriptor> Descriptors { get; private set; }
        public string LevelName { get; private set; }
        public Color BackgroundColor { get; set; }
        public Color BackgroundColorCompliment { get { return BackgroundColor.invert(); } }
        public Color AmbientLight { get; set; }
        public bool PreviewLighting { get; set; }
        public Camera Camera { get; set; }
        public List<Layer> Layers { get; set; }

        //Thresh  Blur Bloom  Base  BloomSat BaseSat
        public BloomSettings BloomSettings { get; set; }    


        public EditorLevel(string name)
        {
            init();
            Camera = new Camera();
            BackgroundColor = Color.Orange ;
            AmbientLight = Color.White;
            PreviewLighting = true;
            BloomSettings = BloomSettings.PresetSettings[5];
            LevelName = name;
            Layers = new List<Layer>();
            Layers.Add(defaultLayer);
            //Descriptors = new List<InstanceDescriptor>();
            InstanceManager.Instance.addLevel(this);
        }

        //public EditorLevel(State state) : this(state.Name)
        //{
        //    init();
        //    setInstanceLevelState(state);
        //}

        public Layer getLayer(string name)
        {
            return Layers.Find(l => l.Name.Equals(name));
        }

        public void syncObjectManager()
        {
            List<InstanceDescriptor> descs = getInstances();
            foreach (InstanceDescriptor i in descs)
            {
                i.updateObjectManager(this);
            }

        }

        public void syncAllTypesToInstances()
        {
            getInstances().ForEach((i) =>
            {
                i.syncType();
            });
        }

        public void syncTypeToInstances(TypeDescriptor typeDescriptor)
        {
            getInstances().ForEach((i) =>
            {

                if (i.TypeDescriptorInFileManager.Name.Equals(typeDescriptor.Name))
                {
                    i.syncType();
                }

            });

        }

   
        public override void addObject(GameObject gob)
        {

            
            base.addObject(gob);
            if (gob is InstanceDescriptor)
            {
                updateAll();
            }
        }
        

        public List<InstanceDescriptor> getInstances()
        {
            List<InstanceDescriptor> list = new List<InstanceDescriptor>();
            foreach (GameObject gob in getAllObjects())
            {
                list.Add((InstanceDescriptor)gob);
            }
            return list;
        }

        //public State getWhiskeyLevelState()
        //{

        //}


        public static string serialize(EditorLevel lvl, string fileName)
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

        public static EditorLevel deserialize(string fileName)
        {
            // GameManager.Log.debug("State loading : " + fileName);
            if (!File.Exists(fileName))
            {
                throw new WhiskeyException("Level does not exist : " + fileName);
            }

            // Open the file containing the data that you want to deserialize.
            EditorLevel lvl;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                lvl = (EditorLevel)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                throw new WhiskeyException("Failed to deserialize. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
            return lvl;
        }


    }
}
