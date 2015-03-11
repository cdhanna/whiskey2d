using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;
using System.Globalization;
using WhiskeyEditor.Backend;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;

namespace WhiskeyEditor.UI.Properties.Converters
{
    class WhiskeyTypeEditors
    {

        private static Dictionary<string, UITypeEditor> map;
        static WhiskeyTypeEditors()
        {
            map = new Dictionary<string, UITypeEditor>();

            map.Add("Color", new ColorPicker());
            map.Add("Light", new LightColorPicker());
            map.Add("ShadowProperties", new ShadowPropertiesPicker());
            map.Add("Sprite", new SpritePathPicker());
            map.Add("Boolean", new BoolPicker());
            map.Add("InstanceDescriptor", new InstancePicker());
            map.Add("Keys", new KeyTypeEditor());
            map.Add("Layer", new LayerPicker());
        }


        public static UITypeEditor lookUp(string name)
        {

            if (TypeBank.Instance.getObjectNames().Contains(name))
                return lookUp("InstanceDescriptor");

            if (map.ContainsKey(name))
                return map[name];
            else return null;
        }

    }
}
