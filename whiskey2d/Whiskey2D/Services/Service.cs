using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Whiskey2D.Services
{

  
    public interface Service
    {

        object invoke(string commandName, object[] parameters);

        string getServiceTypeName();
        object getServiceInstance();
     
        ServiceProperty[] getServiceProperties();
        ServiceProperty getServiceProperty(string name);
        //ServiceProperty getServiceProperty(string name, Type type);
        object getServicePropertyValue(ServiceProperty property);
        void setServicePropertyValue(ServiceProperty property, object value);

      

    }
}
