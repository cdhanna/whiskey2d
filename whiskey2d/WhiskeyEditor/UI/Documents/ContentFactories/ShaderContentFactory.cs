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
    class ShaderContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public ShaderDocumentInfo Info { get; private set; }

        public ShaderContentFactory(DocumentView docView, ShaderDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyEditor generatePropertyContent()
        {
            return new ShaderDescriptorPropertyEditor(Info.Shader);
        }

        public DocumentTab generateDocumentTab()
        {
            return new ShaderDocument(Info.Shader, DocumentView);
        }

    }
}
