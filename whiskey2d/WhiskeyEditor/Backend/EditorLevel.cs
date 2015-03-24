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

    using CoreLayer = Whiskey2D.Core.Layer;

    [Serializable]
    public class EditorLevel : DefaultObjectManager
    {

        private CoreLayer defaultLayer = new CoreLayer("Default");
        public CoreLayer DefaultLayer { get { return defaultLayer; } }


        //public List<InstanceDescriptor> Descriptors { get; private set; }
        public string LevelName { get; private set; }
        public Color BackgroundColor { get; set; }
        public Color BackgroundColorCompliment { get { return BackgroundColor.Inverted; } }
        public Color AmbientLight { get; set; }

        public Boolean PreviewHud { get; set; }
        public bool PreviewLighting { get; set; }
        public bool PreviewShadowing { get; set; }
        public bool LightingEnabled { get; set; }
        public bool ShadowingEnabled { get; set; }

        public Camera Camera { get; set; }
        public List<CoreLayer> Layers { get; set; }

        private ShaderParameters _shaderParameters;
        public ShaderParameters ShaderParameters
        {
            get
            {
                if (_shaderParameters == null)
                {
                    _shaderParameters = new ShaderParameters();
                }
                return _shaderParameters;
            }
            set
            {
                _shaderParameters = value;
            }
        }

        //Thresh  Blur Bloom  Base  BloomSat BaseSat
        public BloomSettings BloomSettings { get; set; }

        public BloomSettings LightBloomSettings { get; set; }


        [NonSerialized]
        private float time = 0;

        public EditorLevel(string name)
        {
            init();
            Camera = new Camera();
            BackgroundColor = Color.Orange ;
            AmbientLight = Color.White;
            ShaderParameters = new ShaderParameters();
            
            PreviewLighting = true;
            PreviewShadowing = true;
            LightingEnabled = true;
            ShadowingEnabled = true;

            BloomSettings = new BloomSettings(BloomSettings.PresetSettings[5]);
            LightBloomSettings = new BloomSettings(BloomSettings.PresetSettings[5]);
            LevelName = name;
            Layers = new List<CoreLayer>();
            Layers.Add(defaultLayer);
            //Descriptors = new List<InstanceDescriptor>();
            InstanceManager.Instance.addLevel(this);
        }




        public CoreLayer getLayer(string name)
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


        public override void updateAll()
        {
            ShaderParameters.setFloat(ShaderParameters.PARAM_TIME, time);
            if (WhiskeyEditor.MonoHelp.WhiskeyControl.InputManager != null)
            {

               
            }
            time += .001f;
            base.updateAll();
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
