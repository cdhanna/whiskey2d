using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Info
{
    class DocumentInfoFactory
    {

        public DocumentContentInfo generateDocumentContentInfo(Descriptor descriptor)
        {
            DocumentContentInfo info = null;
            if (descriptor is TypeDescriptor)
            {
                info = new TypeDocumentInfo((TypeDescriptor)descriptor);
            }
            else if (descriptor is ScriptDescriptor)
            {
                info = new ScriptDocumentInfo((ScriptDescriptor)descriptor);
            }
            else if (descriptor is LevelDescriptor)
            {
                info = new LevelDocumentInfo((LevelDescriptor)descriptor);
            }
            else if (descriptor is InstanceDescriptor)
            {
                info = new InstanceDocumentInfo((InstanceDescriptor)descriptor);
            }

            return info;
        }




    }
}
