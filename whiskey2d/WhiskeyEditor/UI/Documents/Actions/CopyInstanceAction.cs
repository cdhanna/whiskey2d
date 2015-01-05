using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class CopyInstanceAction : AbstractAction
    {
        public CopyInstanceAction()
            : base("Copy", Assets.AssetManager.ICON_COPY)
        {

        }

        protected override void run()
        {
            CopyPasteManager.Instance.copyToBuffer(SelectionManager.Instance.SelectedInstance);
        }
    }
}
