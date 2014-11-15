using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class RealType : TypeVal
    {

        private Type type;
        private object value;

        public RealType(Type type, object value)
        {
            if (value != null && !value.GetType().Equals(type))
            {
                throw new WhiskeyException("Given type does not match given value: " + type.Name + " versus value of " + value.GetType().Name);
            }

            this.type = type;
            this.value = value;
        }

    }
}
