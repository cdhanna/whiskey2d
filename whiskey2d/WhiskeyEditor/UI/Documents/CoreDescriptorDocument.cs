using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.compile_types;
using System.Windows.Forms;

namespace WhiskeyEditor.UI.Documents
{
    class CoreDescriptorDocument : DocumentTab
    {

        public CoreDescriptor Descriptor { get; private set; }
        public CoreDescriptorControl Control { get; private set; }

        public CoreDescriptorDocument(CoreDescriptor coreDesc, DocumentView dView)
            : base(coreDesc.FilePath, dView)
        {
            Text = coreDesc.Name;
            Descriptor = coreDesc;

            Control = new CoreDescriptorControl();
            
            Control.setForDescriptor(this);

            Control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Control);
        }



    }
}
