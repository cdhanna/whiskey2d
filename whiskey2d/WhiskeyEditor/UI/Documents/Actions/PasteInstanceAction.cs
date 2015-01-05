using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class PasteInstanceAction : AbstractAction
    {
        public LevelDescriptor Descriptor { get; private set; }

        public PasteInstanceAction(LevelDescriptor level)
            : base("Paste", Assets.AssetManager.ICON_PASTE)
        {
            Descriptor = level;
        }

        protected override void run()
        {

            InstanceDescriptor inst = CopyPasteManager.Instance.pasteFromBuffer(Descriptor.Level);
            inst.X = 0;
            inst.Y = 0;

        }
    }
}
