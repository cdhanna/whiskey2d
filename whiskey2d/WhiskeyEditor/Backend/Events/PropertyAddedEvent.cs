using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallMVC;


namespace WhiskeyEditor.Backend.Events
{
    class PropertyAddedEvent : Event
    {
        private PropertyDescriptor prop;

        public PropertyAddedEvent(PropertyDescriptor prop)
        {
            this.prop = prop.clone();
            
        }

        public PropertyDescriptor Property { get { return prop; } }

    }
}
