using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.compile_types;

namespace WhiskeyEditor.UI.Documents.Info
{
    class DocumentInfoFactory
    {

        public DocumentContentInfo generateDocumentContentInfo(Descriptor descriptor)
        {
            DocumentContentInfo info = null;


            if (descriptor is CoreDescriptor)
            {
                info = new CoreDocumentInfo((CoreDescriptor)descriptor);
            }
            else if (descriptor is TypeDescriptor)
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
            else if (descriptor is Project)
            {
                info = new ProjectSettingsDocumentInfo((Project)descriptor);
            }
            else if (descriptor is ArtDescriptor)
            {
                info = new ArtDocumentInfo((ArtDescriptor)descriptor);
            }
            else if (descriptor is SoundDescriptor)
            {
                info = new SoundDocumentInfo((SoundDescriptor)descriptor);
            }

            return info;
        }




    }
}
