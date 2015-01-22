using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
namespace WhiskeyEditor.UI.Properties.Editors
{
    class MediaDescriptorPropertyEditor : FileDescriptorPropertyEditor<MediaDescriptor>
    {

        public MediaDescriptorPropertyEditor(MediaDescriptor mDesc)
            : base(mDesc)
        {
            
            Title = "Media Properties";

 
            
        }

    }
}
