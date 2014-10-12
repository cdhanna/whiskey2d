using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.ValueEditors
{
    public partial class IntEditor : ValueEditor
    {
        public IntEditor()
        {
            InitializeComponent();
        }

        public override object getValue()
        {
            return null;
        }

        public override void setValue(object value)
        {
            throw new NotImplementedException();
        }
    }
}
