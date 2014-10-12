using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor
{
    using Prop = ClassLoader.PropertyDescriptor;

    public partial class GameObjectTypeEditor : UserControl
    {

        GameObjectDescriptor descr;
        List<PropertyDescriptorControl> propControls;

        public GameObjectTypeEditor()
        {
            InitializeComponent();

            propControls = new List<PropertyDescriptorControl>();

        }

        public void createNewType()
        {

            descr = new GameObjectDescriptor("SampleProject", "NewType");

            foreach (Prop p in descr.Properties)
            {
                addProperty(p);

            }

           // descriptorGrid.SelectedObject = descr;


        }

        private void addPropertyButton_Click(object sender, EventArgs e)
        {
            if (descr != null)
            {

                addProperty();
               
               // descr.addProperty();


            }
        }

        private void addProperty(Prop property)
        {
            PropertyDescriptorControl pControl = new PropertyDescriptorControl(property);

            //pControl.closeBtn.Click += (s, evt) =>
            //{


            //    for (int i = propControls.IndexOf(pControl); i < propControls.Count; i++)
            //    {
            //        propControls[i].Location = new Point(propControls[i].Location.X, propControls[i].Location.Y - pControl.Size.Height);
            //    }

            //    propControls.Remove(pControl);
            //    propPanel.Controls.Remove(pControl);

            //};


            int heightSoFar = 0;
            for (int i = 0; i < propControls.Count; i++)
            {
                heightSoFar += propControls[i].Size.Height;
            }

            pControl.Location = new Point(pControl.Location.X, heightSoFar);
            Console.WriteLine(heightSoFar);
            // pControl.Location = new Point(pControl.Location.X, (pControl.Size.Height + 2) * propControls.Count);
            pControl.Width = this.Width - 60;
            propPanel.Controls.Add(pControl);
            propControls.Add(pControl);
        }

        private void addProperty()
        {
            addProperty(new Prop("x", typeof(int), 0));
        }

    }
}
