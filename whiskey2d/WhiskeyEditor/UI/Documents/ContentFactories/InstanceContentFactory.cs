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
    class InstanceContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public InstanceDocumentInfo Info { get; private set; }

        public InstanceContentFactory(DocumentView docView, InstanceDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyEditor generatePropertyContent()
        {
            return new InstanceDescriptorPropertyEditor(Info.Instance);
        }

        public DocumentTab generateDocumentTab()
        {
            return new BlankDocument(DocumentView);
        }
    }
}
