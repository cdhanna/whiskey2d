using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Info
{
    class InstanceDocumentInfo : DocumentContentInfo
    {
        public InstanceDescriptor Instance { get; private set; }
        public InstanceDocumentInfo(InstanceDescriptor instance)
        {
            Instance = instance;
        }
    }
}
