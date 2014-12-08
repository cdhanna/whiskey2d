using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Properties;
namespace WhiskeyEditor.UI.Properties.Editors
{
    class RemovePropertyAction : AbstractAction
    {

        public TypeDescriptor Descriptor { get; private set; }
        public PropertyDescriptor Property { get; private set; }

        public RemovePropertyAction(TypeDescriptor tDesc, PropertyDescriptor pDesc) :
            base (pDesc.Name, Assets.AssetManager.ICON_MINUS)
        {
            Descriptor = tDesc;
            
            Property = pDesc;
        }

        protected override void run()
        {

            Descriptor.removePropertyDescriptor(Property);


        }
    }
}
