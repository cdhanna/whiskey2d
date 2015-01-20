using System;
using System.Collections.Generic;
using System.Linq;


namespace WhiskeyEditor.Backend
{
    [Serializable]
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

        public Type Type { get { return type; } }

        public string TypeName
        {
            get { return type.Name; }
        }

        object TypeVal.Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value != null && !value.GetType().Equals(type))
                {

                    //check for enum
                    if (type.IsEnum)
                    {
                        
                        object converted = Enum.Parse(type, value.ToString());
                        this.value = value;
                        if (ValueChanged != null)
                            ValueChanged(this, new EventArgs());
                    }
                    else
                    {
                        throw new WhiskeyException("Cannot set value of " + value.GetType().Name + " to a typeval of " + TypeName);
                    }
                }
                else
                {
                    this.value = value;
                    if (ValueChanged != null)
                        ValueChanged(this, new EventArgs());
                }
            }
        }


        public TypeVal clone()
        {
            object clone = null;
            if (!value.GetType().IsEnum)
            {
                clone = Nuclex.Cloning.ReflectionCloner.DeepPropertyClone(value);
            }
            else
            {
                clone = value;
            }
            return new RealType(type, clone);
        }


        public event EventHandler ValueChanged = new EventHandler( (s, a) => {});
    }
}
