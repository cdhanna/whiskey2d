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
            map.Add("Sprite", new SpritePathPicker());

        }


        public static UITypeEditor lookUp(string name)
        {
            if (map.ContainsKey(name))
                return map[name];
            else return null;
        }

    }
}
