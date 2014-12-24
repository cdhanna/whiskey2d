using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.UI.Properties
{
    public partial class RestrictedFloatPickerControl : UserControl
    {

        public TrackBar TrackBar { get { return trackBar1; } }
        public float Min { set { minLabel.Text = "" + value; } }
        public float Max { set { maxLabel.Text = "" + value; } }
        public float Value { set { valueLabel.Text = "" + value; } }

        public RestrictedFloatPickerControl()
        {
            InitializeComponent();
        }
    }
}
