using System;
using System.Collections.Generic;
using System.Linq;

namespace WhiskeyEditor.Backend
{
    class InstanceType : TypeVal
    {
        private TypeDescriptor descr;
        private InstanceDescriptor instance;

        //private Insta
        public InstanceType(TypeDescriptor descr, InstanceDescriptor instance)
        {
            this.instance = instance;
            this.descr = descr;
        }


        public string TypeName
        {
            get { return this.descr.ClassName; }
        }

        public object value
        {
            get
            {
                return this.instance;
            }
            set
            {
                if (value is InstanceDescriptor)
                {
                    InstanceDescriptor newVal = (InstanceDescriptor)value;
                    if (newVal.TypeDescriptor.ClassName.Equals(TypeName))
                    {
                        this.instance = newVal;
                    }
                    else throw new WhiskeyException("Given Instance is of incorrect type: " + newVal.TypeDescriptor.ClassName + " versus " + TypeName);
                }
                else throw new WhiskeyException("Given value is not an Instance Descriptor");

                
            }
        }


        public TypeVal clone()
        {
            return new InstanceType(descr, instance);
        }
    }
}
