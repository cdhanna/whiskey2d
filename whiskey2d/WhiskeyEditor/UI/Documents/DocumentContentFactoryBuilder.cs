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

            if (info != null)
            {
                factory = info.generateContentFactory(DocumentView);
            }

            return factory;
        }


    }
}
