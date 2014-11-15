using System;
using System.Collections.Generic;
using System.Linq;


namespace WhiskeyEditor.Backend
{
    class RealType : TypeVal
    {

        private Type type;
        private object value;

        public RealType(Type type, object value)
        {

            value = Convert.ChangeType(value, type);

            if (value != null && !value.GetType().IsAssignableFrom(type))
            {
                throw new WhiskeyException("Given type does not match given value: " + type.Name + " versus value of " + value.GetType().Name);
            }

            this.type = type;
            this.value = value;
        }


        public string TypeName
        {
            get { return type.Name; }
        }

        object TypeVal.value
        {
            get
            {
                return value;
            }
            set
            {
                if (value != null && !value.GetType().Equals(type))
                {
                    throw new WhiskeyException("Cannot set value of " + value.GetType().Name + " to a typeval of " + TypeName);
                }
                else
                {
                    this.value = value;
                }
            }
        }


        public TypeVal clone()
        {
            return new RealType(type, Nuclex.Cloning.ReflectionCloner.ShallowFieldClone(value) );
        }
    }
}
