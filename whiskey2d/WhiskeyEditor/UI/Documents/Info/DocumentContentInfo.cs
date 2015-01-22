using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Documents.ContentFactories;

namespace WhiskeyEditor.UI.Documents.Info
{
    interface DocumentContentInfo
    {

        IDocumentContentFactory generateContentFactory(DocumentView DocumentView);

    }
}
