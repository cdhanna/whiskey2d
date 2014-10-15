using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.Controls.ValueEditor
{
    public partial class ObjectEditor : ValueEditor
    {
        public ObjectEditor(object value)
        {
            InitializeComponent();

            whiskeyPropertyGrid1.SelectedObject = value;

           

        }
        public override object getValue()
        {
            return whiskeyPropertyGrid1.SelectedObject;
        }
        public override void setValue(object value)
        {
            whiskeyPropertyGrid1.SelectedObject = value;
        }

    }
}
