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
    class TypeContentFactory : IDocumentContentFactory
    {

        public DocumentView DocumentView { get; private set; }
        public TypeDocumentInfo Info { get; private set; } 

        public TypeContentFactory(DocumentView docView, TypeDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }


        public PropertyContent generatePropertyContent()
        {
            return new TypeDescriptorPropertyEditor(Info.TypeDescriptor);
        }

        public DocumentTab generateDocumentTab()
        {
            CodeDocument doc = (CodeDocument) DocumentView.getTab(Info.TypeDescriptor.Name);
            if (doc == null)
            {
                doc = new CodeDocument(Info.TypeDescriptor, DocumentView);
            }
            return doc;
        }
    }
}
