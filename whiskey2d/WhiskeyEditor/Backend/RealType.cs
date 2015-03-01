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


        string typeName = "";

        public RealType(Type type, object value)
        {

            value = Convert.ChangeType(value, type);

            if (value != null && !value.GetType().IsAssignableFrom(type))
            {
                throw new WhiskeyException("Given type does not match given value: " + type.Name + " versus value of " + value.GetType().Name);
            }

            this.type = type;
            this.value = value;

            if (this.type.GenericTypeArguments.Length > 0)
            {
                string name = type.Name;
                int index = name.IndexOf('`');
                name = index == -1 ? name : name.Substring(0, index);

                typeName = name;

                typeName += "<";
                foreach (var t in type.GenericTypeArguments){
                    typeName += t.Name + ",";
                }
                typeName = typeName.Substring(0, typeName.Length - 1);
                typeName += ">";

            }
            else
            {
                typeName = type.Name;
            }

        }

       

        public Type Type { get { return type; } }

        public string TypeName
        {
            get { return typeName; }
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
            if (value != null && !value.GetType().IsEnum)
            {
                try
                {
                    clone = Nuclex.Cloning.ReflectionCloner.DeepPropertyClone(value);
                }
                catch (Exception e)
                {
                    clone = value;
                }
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
