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
    public class GameLevel : DefaultObjectManager
    {

        public Color BackgroundColor { get; set; }
        public Color AmbientLight { get; set; }
        public string Name { get; private set; }
        public Camera Camera { get; set; }
        public BloomSettings BloomSettings { get; set; }
        public BloomSettings BloomLightSettings { get; set; }
        public bool LightingEnabled { get; set; }
        public bool ShadowsEnabled { get; set; }


        private List<Layer> layers;
        public List<Layer> Layers
        {
            get
            {
                if (layers == null) layers = new List<Layer>();
                return layers;
            }
            set
            {
                layers = value;
            }
        }

        public Layer getLayer(String name)
        {
            Layer found = Layers.Find(l => l.Name.Equals(name));
            return found == null ? Layers.Find(l => l.Name.Equals("Default")) : found;

        }


        private ShaderParameters shaderParameters;
        public ShaderParameters ShaderParameters
        {
            get
            {
                if (shaderParameters == null) shaderParameters = new ShaderParameters();
                return shaderParameters;
            }
            set
            {
                shaderParameters = value;
            }
        }

        public float Time { get; set; }

        public GameLevel(string name)
            : base()
        {
            BackgroundColor = Color.White;
            AmbientLight = Color.White;
            Camera = new Camera();
            Name = name;
            BloomSettings = new BloomSettings(BloomSettings.PresetSettings[0]);
            BloomLightSettings = new BloomSettings(BloomSettings.PresetSettings[0]);
            
        }

        public override void updateAll()
        {
            if (Time == null)
            {
                Time = 0;
            }
            Time += .001f;

            shaderParameters.setFloat(ShaderParameters.PARAM_TIME, Time);
            Vector mouse = GameManager.Input.MousePosition;
            mouse.X /= GameManager.ScreenWidth;
            mouse.Y /= GameManager.ScreenHeight;
            shaderParameters.setVector(ShaderParameters.PARAM_MOUSE_POS, mouse);

            Microsoft.Xna.Framework.Vector3 translation = Camera.TranformMatrix.Translation;
            translation.X /= GameManager.ScreenWidth;
            translation.Y /= GameManager.ScreenHeight;
            ShaderParameters.setVector(ShaderParameters.PARAM_CAMERA, new Vector(translation.X, translation.Y));
            ShaderParameters.setFloat(ShaderParameters.PARAM_CAMERA_ZOOM, Camera.Zoom);


            Camera.update();
            base.updateAll();
        }

        public static string serialize(GameLevel lvl, string fileName)
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

        public static GameLevel deserialize(string fileName)
        {
            // GameManager.Log.debug("State loading : " + fileName);
            if (!File.Exists(fileName))
            {
                throw new WhiskeyRunTimeException("Level does not exist : " + fileName);
            }

            // Open the file containing the data that you want to deserialize.
            GameLevel lvl;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                lvl = (GameLevel)formatter.Deserialize(fs);
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
