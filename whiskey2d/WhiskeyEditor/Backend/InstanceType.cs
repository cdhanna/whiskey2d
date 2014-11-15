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
            get { throw new NotImplementedException(); }
        }

        public object value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public TypeVal clone()
        {
            return new InstanceType(descr, instance);
        }
    }
}
