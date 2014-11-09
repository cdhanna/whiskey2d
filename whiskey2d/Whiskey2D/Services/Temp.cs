using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Whiskey2D.Services
{
    [Serializable]
    public class Temp : MarshalByRefObject
    {


        private Dictionary<String, object> propertyMap;

        public Temp(Service service)
        {
            propertyMap = new Dictionary<string, object>();
            foreach (ServiceProperty prop in service.getServiceProperties())
            {
                propertyMap.Add(prop.Name, prop.get(service));
            }
        }


        public S populateFromMemory<S>(S service) where S : Service
        {

            foreach (ServiceProperty prop in service.getServiceProperties())
            {
                if (propertyMap.ContainsKey(prop.Name))
                {
                    prop.set(service, propertyMap[prop.Name]);
                }
            }

            return service;
        }

    }
}
