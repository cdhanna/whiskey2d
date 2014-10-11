using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor
{


    public partial class EditorForm : Form
    {
        
        private List<PropertyDescriptorControl> pControls;

        public EditorForm()
        {
            pControls = new List<PropertyDescriptorControl>();

            InitializeComponent();




        }

        private void gameObjectTypeNameBox_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void addPropertyBtn_Click(object sender, EventArgs e)
        {
            PropertyDescriptorControl pControl = new PropertyDescriptorControl();

            pControl.closeBtn.Click += (s, evt) => {
                pControls.Remove(pControl);
                propertyScrollPanel.Controls.Remove(pControl);
                for (int i = 0; i < pControls.Count; i++)
                {
                    pControls[i].Location = new Point(pControls[i].Location.X, (pControls[i].Size.Height + 2) * i);
                }
            };

            pControl.Location = new Point(pControl.Location.X, (pControl.Size.Height +2)* pControls.Count);


            pControls.Add(pControl);


            propertyScrollPanel.Controls.Add(pControl);
        }



        private void acceptBtn_Click(object sender, EventArgs e)
        {

            string name = gameObjectTypeNameBox.Text;

            GameObjectDescriptor descr = new GameObjectDescriptor("SampleProject", name);

            foreach (PropertyDescriptorControl pControl in pControls)
            {
                descr.addProperty(pControl.Property);
            }

            TypeManager.getInstance().addDescriptor(descr);

        }

        private void whiskeyControl_DragDrop(object sender, DragEventArgs e)
        {
            object typeOfDrag = e.Data.GetData(DataFormats.Serializable);
            Console.WriteLine(typeOfDrag);
            Point p = new Point(e.X, e.Y);
            p = this.PointToClient(p);
            whiskeyControl.addNewGameObject((Type)typeOfDrag, p.X, p.Y);
        }

        private void whiskeyControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
