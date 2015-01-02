using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Documents.Info;
using WhiskeyEditor.UI.Documents.ContentFactories;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents
{
    class DocumentContentFactoryBuilder : IDocumentContentFactoryBuilder
    {
        public DocumentView DocumentView { get; private set; }



        public DocumentContentFactoryBuilder(DocumentView docView)
        {
            DocumentView = docView;
        }

        public IDocumentContentFactory generateDocumentContentFactory(DocumentContentInfo info)
        {
            IDocumentContentFactory factory = null;

            if (info is TypeDocumentInfo)
            {
                factory = new TypeContentFactory(DocumentView, (TypeDocumentInfo) info);
                
            }
            else if (info is ScriptDocumentInfo)
            {
                factory = new ScriptContentFactory(DocumentView, (ScriptDocumentInfo) info);
            }
            else if (info is LevelDocumentInfo)
            {
                factory = new LevelContentFactory(DocumentView, (LevelDocumentInfo)info);
            }
            else if (info is InstanceDocumentInfo)
            {
                factory = new InstanceContentFactory(DocumentView, (InstanceDocumentInfo)info);
            }
            return factory;
        }


    }
}
