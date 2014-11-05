using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyService
{
    class DefaultLibrary : Library
    {

        private AppDomain app;
        private List<Service> services;

        public DefaultLibrary()
        {
            services = new List<Service>();
            app = AppDomain.CreateDomain("whiskeyAppLibrary", null, AppDomain.CurrentDomain.SetupInformation);
        }


        public S addService<S>(ServiceHandle<S> handle) where S : Service
        {

            object obj = app.CreateInstanceFromAndUnwrap(handle.AssemblyPath, handle.TypeName);
            S service = (S)obj;
            service.AssemblyPath = handle.AssemblyPath;
            services.Add(service);
            return service;

        }


        public void unload()
        {
            AppDomain.Unload(app);
            string badFiles = "";
            while (services.Count > 0)
            {
                Service service = services[0];
                try
                {
                    File.Delete(service.AssemblyPath);
                }
                catch (Exception e)
                {
                    badFiles += service.AssemblyPath + Environment.NewLine;
                }
            }
            if (badFiles.Length > 0)
            {
                throw new Exception("Bad Files : " + badFiles);
            }
        }
    }
}
