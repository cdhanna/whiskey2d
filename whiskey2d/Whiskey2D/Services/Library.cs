using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Whiskey2D.Services
{
    public class Library
    {
        private AppDomain app;


       // private List<string> serviceHandles;
        private List<Service> services;
        private Dictionary<Service, string> serviceDllTable;


        public Library()
        {
            app = AppDomain.CreateDomain("app", null, AppDomain.CurrentDomain.SetupInformation);
            services = new List<Service>();
            serviceDllTable = new Dictionary<Service, string>();
           // serviceHandles = new List<ServiceHandle>();

        }

        public S add<S>(ServiceHandle<S> handle) where S : Service
        {
            
            
            object obj = app.CreateInstanceFromAndUnwrap(handle.AssemblyName, handle.TypeName);
            S serv = (S)obj;
            services.Add(serv);
            serviceDllTable.Add(serv, handle.AssemblyName);
            return serv;


          //  serviceHandles.Add(handle);

            
        }

        public void unload()
        {
            List<String> values = new List<string>();

            foreach (Service serv in serviceDllTable.Keys)
            {
                values.Add(serviceDllTable[serv]);
            }

            AppDomain.Unload(app);
            while (values.Count > 0)
            {
                File.Delete(values[0]);
                values.RemoveAt(0);
            }
        }
    }
}
