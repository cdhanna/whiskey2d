using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallMVC;

namespace WhiskeyEditor.Backend.Events
{
    class PropertyChangedEvent : Event
    {

        private PropertyDescriptor prop;
        private String oldPropertyName;

        public PropertyChangedEvent(String oldName, PropertyDescriptor newProp)
        {
            this.oldPropertyName = oldName;
            this.prop = newProp.clone();
        }

        public String OldName { get { return oldPropertyName; } }
        public PropertyDescriptor NewProperty { get { return prop; } }

    }
}
