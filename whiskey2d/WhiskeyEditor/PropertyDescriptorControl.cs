using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whiskey2D.Core;
namespace WhiskeyEditor
{
    using Prop = ClassLoader.PropertyDescriptor;

    public partial class PropertyDescriptorControl : UserControl
    {

        private Prop prop;

        public Prop Property
        {
            get
            {
                setPropertyToVisuals();
                return prop;
            }
            
        }


        private void setPropertyToVisuals()
        {
            prop.Name = this.nameBox.Text;
            prop.Type = (Type)this.typeBox.SelectedItem ;

        }


        public PropertyDescriptorControl(Prop property)
        {
            InitializeComponent();
            this.typeBox.Items.Clear();
            this.typeBox.Items.Add(typeof(int));
            this.typeBox.Items.Add(typeof(string));
            this.typeBox.Items.Add(typeof(float));
            this.typeBox.Items.Add(typeof(Sprite));
            this.typeBox.Items.Add(typeof(Vector));
            this.typeBox.Items.Add(typeof(Whiskey2D.Core.Color));
            prop = property;


            nameBox.Text = prop.Name;
            typeBox.SelectedItem = property.Type;
            valueGrid.SelectedObject = property.Value;

        }

        public PropertyDescriptorControl()
            : this(new Prop("x", typeof(int), 0))
        {
           
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            
            
        }

        private void PropertyDescriptorControl_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
