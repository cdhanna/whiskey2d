using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.UI.Properties
{
    public partial class TypePickerControl : UserControl
    {

        public ComboBox PrimitiveBox { get { return primsTypeBox; } }
        public ComboBox ObjectsBox { get { return objsTypeBox; } }

        public TypePickerControl()
        {
            InitializeComponent();

            primsTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            objsTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;

            

        }

        private void onClick_updateBoxes(object sender, EventArgs args)
        {
            updateBoxes();
        }

        public void updateBoxes()
        {
            //primitives
            string[] primNames = TypeBank.Instance.getPrimitiveNames();
            primsTypeBox.Items.Clear();
            foreach (string primName in primNames)
            {
                primsTypeBox.Items.Add(primName);
            }

            //objects
            string[] objNames = TypeBank.Instance.getObjectNames();
            objsTypeBox.Items.Clear();
            foreach (string objName in objNames)
            {
                objsTypeBox.Items.Add(objName);
            }


        }
    }
}
