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
    class ScriptContentFactory : IDocumentContentFactory
    {

        public ScriptDocumentInfo Info { get; private set; }
        public DocumentView DocumentView { get; private set; }
        public ScriptContentFactory(DocumentView docView, ScriptDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyContent generatePropertyContent()
        {
            return new ScriptDescriptorPropertyEditor(Info.ScriptDescriptor);
        }

        public DocumentTab generateDocumentTab()
        {
            CodeDocument doc = (CodeDocument) DocumentView.getTab(Info.ScriptDescriptor.Name);
            if (doc == null)
            {
                doc = new CodeDocument(Info.ScriptDescriptor, DocumentView);
            }
            return doc;
        }
    }
}
