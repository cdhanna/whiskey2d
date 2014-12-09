using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class ScriptDescriptorPropertyEditor : FileDescriptorPropertyEditor<ScriptDescriptor>
    {
        public ScriptDescriptorPropertyEditor(ScriptDescriptor desc)
            : base(desc)
        {
            Title = "Script Properties";
        }

    }
}
