using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;
namespace WhiskeyEditor.UI.Documents.Info
{
    class SoundDocumentInfo : DocumentContentInfo
    {

        public SoundDescriptor Sound { get; private set; }
        public SoundDocumentInfo(SoundDescriptor sound)
        {
            Sound = sound;
        }
        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new SoundContentFactory(DocumentView, this);
        }
    }
}
