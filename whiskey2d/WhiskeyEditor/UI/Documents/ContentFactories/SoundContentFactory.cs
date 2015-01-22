using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Documents.Info;

namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    class SoundContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public SoundDocumentInfo Info { get; private set; }

        public SoundContentFactory(DocumentView doc, SoundDocumentInfo info)
        {
            DocumentView = doc;
            Info = info;
        }


        public Properties.Editors.PropertyEditor generatePropertyContent()
        {
            return new Properties.Editors.MediaDescriptorPropertyEditor(Info.Sound);
        }

        public DocumentTab generateDocumentTab()
        {
            return new SoundDocument(Info.Sound, DocumentView);
        }
    }
}
