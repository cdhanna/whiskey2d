using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;
namespace WhiskeyEditor.UI.Documents.Info
{
    class ArtDocumentInfo : DocumentContentInfo
    {
        public ArtDescriptor Art { get; private set; }
        public ArtDocumentInfo(ArtDescriptor art)
        {
            Art = art;
        }
        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new ArtContentFactory(DocumentView, this);
        }
    }
}
