using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;
using WhiskeyEditor.compile_types;

namespace WhiskeyEditor.UI.Documents.Info
{
    class CoreDocumentInfo : DocumentContentInfo
    {
         public CoreDescriptor CoreDescriptor { get; private set; }

         public CoreDocumentInfo(CoreDescriptor descriptor)
        {
            CoreDescriptor = descriptor;
        }


        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new CoreContentFactory(DocumentView, this);
        }


    }
}
