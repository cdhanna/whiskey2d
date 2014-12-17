using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WhiskeyEditor.Backend.Managers
{
    class SelectionManager
    {
        private static SelectionManager instance = new SelectionManager();
        public static SelectionManager Instance { get { return instance; } }
        private SelectionManager() { }


        public event EventHandler<InstanceDescriptor> SelectedInstanceChanged = new EventHandler<InstanceDescriptor>((s, a) => { });
        public event EventHandler<InstanceDescriptor> SelectedInstanceUpdated = new EventHandler<InstanceDescriptor>((s, a) => { });
        private InstanceDescriptor selected;
        public InstanceDescriptor SelectedInstance
        {
            get
            {
                return selected;
            }
            set
            {
                InstanceDescriptor old = selected;
                selected = value;
                if (old != selected)
                {
                    SelectedInstanceChanged(this, old);
                }
                else
                {
                    SelectedInstanceUpdated(this, old);
                }
            }
        }

    }
}
