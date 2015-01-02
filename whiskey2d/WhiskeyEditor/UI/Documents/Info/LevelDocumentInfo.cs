using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Info
{
    class LevelDocumentInfo : DocumentContentInfo
    {

        public LevelDescriptor LevelDescriptor { get; private set; }

        public LevelDocumentInfo(LevelDescriptor desc)
        {
            LevelDescriptor = desc;
        }
    }
}
