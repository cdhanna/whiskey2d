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

namespace WhiskeyEditor.Controls
{
    public partial class ScriptIcon : UserControl
    {
        public ScriptIcon(ScriptDescriptor sdesc)
        {
            InitializeComponent();
            scriptNameLabel.Text = sdesc.Name;
        }

        //public ScriptDescriptor ScriptDescriptor { get; set; }

    }
}
