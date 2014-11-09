using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Services
{
    [Serializable]
    class ServicePropertyImpl : ServiceProperty
    {

        private string name;
        //private Type propType;
        //private Type declareType;
        
        
        public ServicePropertyImpl(PropertyInfo prop)
        {
            name = prop.Name;
          //  propType = prop.PropertyType;
        }


        public string Name
        {
            get { return name; }
        }

        //public Type PropertyType
        //{
        //    get { return propType; }
        //}

        public object get(Service service)
        {
            object val = service.getServicePropertyValue(this);
            return val;
        }

        public void set(Service service, object value)
        {
            service.setServicePropertyValue(this, value);
        }

    }
}
