using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Services
{

    
    [Serializable]
    public class DefaultService : MarshalByRefObject , Service
    {

        private ServiceProperty[] serviceProps;

        public DefaultService()
        {
            PropertyInfo[] props = GetType().GetProperties();
            serviceProps = new ServiceProperty[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                ServicePropertyImpl servProp = new ServicePropertyImpl(props[i]);
                serviceProps[i] = servProp;
            }
        }

    
        public String getServiceTypeName()
        {
            return GetType().Namespace + "." + GetType().Name;
        }
        public object getServiceInstance()
        {

            return Convert.ChangeType(this, GetType());

            
        }

        public object invoke(string methodName, object[] parameters)
        {
            return GetType().GetMethod(methodName).Invoke(this, parameters);
        }

        public ServiceProperty[] getServiceProperties()
        {
            return serviceProps;
        }

        //public ServiceProperty getServiceProperty(string name, Type type)
        //{
        //    foreach (ServiceProperty serv in serviceProps)
        //    {
        //        if (serv.Name.Equals(name) && serv.PropertyType.Equals(type))
        //        {
        //            return serv;
        //        }
        //    }
        //    throw new Exception("Property " + name + " not found");
        //}

        public ServiceProperty getServiceProperty(string name)
        {
            foreach (ServiceProperty serv in serviceProps)
            {
                if (serv.Name.Equals(name))
                {
                    return serv;
                }
            }
            throw new Exception("Property " + name + " not found");
        }

        private PropertyInfo findPropertyInfo(ServiceProperty property)
        {
            PropertyInfo[] props = GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.Equals(property.Name))
                {
                    return prop;
                }
            }
            throw new Exception("Property does not exist");

        }

        public void setServicePropertyValue(ServiceProperty property, object value)
        {
            PropertyInfo prop = findPropertyInfo(property);
            if (prop.GetSetMethod() != null)
            {
                prop.GetSetMethod().Invoke(this, new object[] { value });
            }
        }

        public object getServicePropertyValue(ServiceProperty property)
        {

            PropertyInfo prop = findPropertyInfo(property);
            object val = prop.GetGetMethod().Invoke(this, new object[] { });
            return val;

        }


      

    }
}
