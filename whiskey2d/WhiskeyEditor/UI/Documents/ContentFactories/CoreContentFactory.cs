using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Documents.Info;
using WhiskeyEditor.Backend;
namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    class CoreContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public CoreDocumentInfo Info { get; private set; } 

        public CoreContentFactory(DocumentView docView, CoreDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }


        public PropertyEditor generatePropertyContent()
        {
            return new PropertyEditor(Info.CoreDescriptor);
        }

        public DocumentTab generateDocumentTab()
        {
            CoreDescriptorDocument doc = (CoreDescriptorDocument)DocumentView.getTab(Info.CoreDescriptor.Name);
            if (doc == null)
            {
                doc = new CoreDescriptorDocument(Info.CoreDescriptor, DocumentView);
            }
            return doc;
        }

    }
}
