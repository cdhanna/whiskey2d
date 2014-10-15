using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WhiskeyEditor.Controls.ValueEditor
{
    public class ValueEditor : UserControl
    {
        public virtual object getValue() { return null; }
        public virtual void setValue(object value) { }
    }
}
