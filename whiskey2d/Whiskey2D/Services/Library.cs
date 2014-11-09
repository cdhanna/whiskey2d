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
        private List< ServiceHandle<Service> > serviceHandles;
        private String libraryName;


        public Library(string libraryName)
        {
           
            this.libraryName = libraryName;
            refreshApp();
            serviceHandles = new List<ServiceHandle<Service>>();

        }

        public S instantiate<S>(ServiceHandle<S> handle) where S : Service
        {

            
            object obj = app.CreateInstanceFromAndUnwrap(handle.AssemblyName, handle.TypeName);
            S serv = (S)obj;

            addHandle(handle);
            
            
            return serv;

        }

        private void addHandle<S>(ServiceHandle<S> handle) where S : Service
        {
            if (0 == serviceHandles.Where(t => t.HandleID == handle.HandleID).ToList().Count)
            {
                serviceHandles.Add(handle.convertUp());
               
            }
        }

        private void refreshApp()
        {
            if (app != null)
            {
                AppDomain.Unload(app);
            }
            app = AppDomain.CreateDomain(libraryName, null, AppDomain.CurrentDomain.SetupInformation);
        }

        public void unload()
        {

            refreshApp();

            while (serviceHandles.Count > 0)
            {
                ServiceHandle<Service> handle = serviceHandles[0];
                serviceHandles.RemoveAt(0);
                File.Delete(handle.AssemblyName);
            }

          
        }
    }
}
