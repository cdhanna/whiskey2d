using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Documents.ContentFactories;
using WhiskeyEditor.UI.Documents.Info;
namespace WhiskeyEditor.UI.Documents
{
    interface IDocumentContentFactoryBuilder
    {

        IDocumentContentFactory generateDocumentContentFactory(DocumentContentInfo info);

    }
}
