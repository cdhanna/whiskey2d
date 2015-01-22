using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;
namespace WhiskeyEditor.UI.Documents.Info
{
    class ScriptDocumentInfo : DocumentContentInfo
    {

        public ScriptDescriptor ScriptDescriptor { get; private set; }

        public ScriptDocumentInfo(ScriptDescriptor scriptDescriptor)
        {
            ScriptDescriptor = scriptDescriptor;
        }
        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new ScriptContentFactory(DocumentView, this);
        }
    }
}
