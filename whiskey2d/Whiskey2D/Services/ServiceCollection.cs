using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Services
{
    public class ServiceCollection
    {
        private List<ServiceHandle<Service>> handles;
        private List<Service> services;
        private List<ServiceDescriptor> descriptors;

        public ServiceCollection()
        {
            services = new List<Service>();
            handles = new List<ServiceHandle<Service>>();
            descriptors = new List<ServiceDescriptor>();
        }


        public void addServiceDescriptor(ServiceDescriptor sDesc)
        {
            descriptors.Add(sDesc);
        }

        public void addServiceInstance<S>(S serv, ServiceHandle<S> handle) where S : Service
        {
           
            services.Add(serv);
            handles.Add(handle.convertUp());
        }




        public Service get(int index)
        {
            return services[index];
        }

        public void ForEach(Action<Service> func)
        {
            services.ForEach(func);
        }


        private void addToDoomed(List<Service> doomed, Service service)
        {

        }

        private List<ServiceDescriptor> contaminate(ServiceDescriptor descr, List<ServiceDescriptor> contaminated)
        {
            contaminated.Add(descr);

            foreach (ServiceDescriptor sd in descriptors){


                if (!contaminated.Contains(sd))
                {
                    List<ServiceDescriptor> refs = sd.getReferences(new List<ServiceDescriptor>());

                    foreach (ServiceDescriptor r in refs)
                    {

                        if (r.QualifiedTypeName.Equals(descr.QualifiedTypeName))
                        {
                            contaminate(sd, contaminated);
                            break;
                        }

                    }
                }

            }

            return contaminated;
        }


        public List<ServiceDescriptor> buildDoomedTypeSet(ServiceDescriptor descr) 
        {

            List<ServiceDescriptor> badDescs = contaminate(descr, new List<ServiceDescriptor>());
            return badDescs;


        }

    }
}
