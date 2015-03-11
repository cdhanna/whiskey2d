using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class ShaderDescriptorPropertyEditor : FileDescriptorPropertyEditor<ShaderDescriptor>
    {
        public ShaderDescriptorPropertyEditor(ShaderDescriptor desc)
            : base(desc)
        {
            Title = "Shader Properties";
        }

    }
}
