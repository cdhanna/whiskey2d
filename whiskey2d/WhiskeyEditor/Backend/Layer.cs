using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;



namespace WhiskeyEditor.Backend
{

    [Serializable]
    [TypeConverter (typeof(WhiskeyEditor.UI.Properties.Converters.LayerTypeConverter))]
    [Editor (typeof(WhiskeyEditor.UI.Properties.LayerPicker), typeof(UITypeEditor))]
    public class Layer
    {

       

        public Boolean Visible { get; set; }
        public Boolean Locked { get; set; }

        public string Name { get; set; }


        public Layer(string name) : this()
        {
            Name = name;
        }

        public Layer()
        {
            Visible = true;
            Locked = false;
        }

    }
}
