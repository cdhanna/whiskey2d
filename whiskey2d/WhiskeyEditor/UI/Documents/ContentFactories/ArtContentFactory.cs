using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Documents.Info;

namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    class ArtContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public ArtDocumentInfo Info { get; private set; }

        public ArtContentFactory(DocumentView docView, ArtDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyEditor generatePropertyContent()
        {
            return new MediaDescriptorPropertyEditor(Info.Art);
        }

        public DocumentTab generateDocumentTab()
        {
            return new ArtDocument(Info.Art, DocumentView);
        }

    }
}
