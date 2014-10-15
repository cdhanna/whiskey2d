using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor.Controls.TypeEditor
{
    public partial class TypeEditor : UserControl
    {
        private GameObjectDescriptor desc;
        public GameObjectDescriptor Descriptor { get { return getDescriptor(); } set { setDescriptor(value); } }

        private List<PropertyEditor> propControls;

        public TypeEditor()
        {
            InitializeComponent();

            propControls = new List<PropertyEditor>();
            Descriptor = null;
        }

        private void addProperty(PropertyDescriptor pdesc)
        {
            PropertyEditor pc = new PropertyEditor(pdesc);
            propControls.Add(pc);
            pc.closeBt.Click +=(snd, args)=>{
                propControls.Remove(pc);
                flowPanel.Controls.Remove(pc);
            };
            flowPanel.Controls.Add(pc);
           
        }

        private void addProperty()
        {
            addProperty(new PropertyDescriptor("Unnamed", typeof(int), 0));
        }

        private void addPropertyBtn_Click(object sender, EventArgs e)
        {


            addProperty();
           

            //addPropertyBtn.Location = new Point(addPropertyBtn.Location.X, addPropertyBtn.Location.Y + 40);

        }
        public void Clear()
        {
            flowPanel.Controls.Clear();
            propControls.Clear();
            Descriptor = null;
        }
        private GameObjectDescriptor getDescriptor()
        {
            desc.Name = nameBox.Text;
            desc.Properties.Clear();
            propControls.ForEach((p) => 
            {
                desc.Properties.Add(p.Property);
                //PropertyDescriptor pd = p.Property;
                //if (!desc.Properties.Contains(pd))
                //{
                //    desc.Properties.Add(pd);
                //}
            });



            return desc;
        }

        private void setDescriptor(GameObjectDescriptor desc)
        {


            this.desc = desc;
            if (desc == null) //disable controls
            {
                this.Enabled = false;

            }
            else
            {
                Clear();
                this.desc = desc;
                this.Enabled = true;
                nameBox.Text = desc.Name;

                foreach (PropertyDescriptor pdesc in desc.Properties)
                {
                    addProperty(pdesc);
                }


            }
            
        }

    }
}
