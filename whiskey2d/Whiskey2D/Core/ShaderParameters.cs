using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    [Serializable]
    public class ShaderParameters
    {

        public const String PARAM_MOUSE_POS = "mousePosition";
        public const String PARAM_CAMERA = "cameraTranslation";
        public const String PARAM_CAMERA_ZOOM = "cameraZoom";
        public const String PARAM_TIME = "time";

        private Dictionary<String, object> table;



        public void updateEffect(Effect fx)
        {
            if (fx != null)
            {
                foreach (String name in table.Keys)
                {
                    EffectParameter param = fx.Parameters[name];
                    object value = table[name];
                    if (param != null && value != null)
                    {
                        if (value is float)
                        {
                            param.SetValue((float)value);
                        }
                        else if (value is int)
                        {
                            param.SetValue((int)value);
                        }
                        else if (value is Vector)
                        {
                            param.SetValue((Vector)value);
                        }
                        
                    }
                }
            }
        }

        public void setFloat(String name, float value)
        {
            set(name, value);
        }

        public void setInt(String name, int value)
        {
            set(name, value);
        }

        public void setVector(String name, Vector value)
        {
            set(name, value);
        }
     

        private void ensureTable()
        {
            if (table == null)
            {
                table = new Dictionary<string, object>();
                setFloat(PARAM_TIME, 0f);
                setVector(PARAM_MOUSE_POS, Vector.Zero);
            }
        }

        private void set(String name, object value)
        {
            ensureTable();
            if (value != null)
            {
                if (!table.ContainsKey(name))
                {
                    table.Add(name, value);
                }
                else
                {
                    table[name] = value;
                }
            }
        }

    }
}
