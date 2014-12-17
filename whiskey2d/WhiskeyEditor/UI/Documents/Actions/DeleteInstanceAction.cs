using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class DeleteInstanceAction : AbstractAction
    {

        public InstanceDescriptor Descriptor { get; private set; }

        public DeleteInstanceAction(InstanceDescriptor descriptor)
            : base("Delete", Assets.AssetManager.ICON_MINUS)
        {
            Descriptor = descriptor;
        }

        protected override void run()
        {
            SelectionManager.Instance.SelectedInstance = null;
            Descriptor.close();
            
        }
    }
}
