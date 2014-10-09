using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public PropertyDescriptorControl()
        {
            InitializeComponent();
            this.typeBox.Items.Clear();
            this.typeBox.Items.Add(typeof(int));
            this.typeBox.Items.Add(typeof(string));
            prop = new Prop("x", typeof(int), 0);
           
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
