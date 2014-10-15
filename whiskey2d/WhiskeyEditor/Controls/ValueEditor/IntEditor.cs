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
    public partial class IntEditor : ValueEditor
    {
        public IntEditor()
        {
            InitializeComponent();
        }

        private void valueBox_TextChanged(object sender, EventArgs e)
        {

        }


        public override object getValue()
        {
            try
            {
                return int.Parse(valueBox.Text);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public override void setValue(object value)
        {
            if (value is Int32)
            {
                valueBox.Text = "" + value.ToString();
            }
            else
            {
                valueBox.Text = "0";
            }
        }

    }
}
