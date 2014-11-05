using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace WhiskeyEditor
{
    class ProxyDomain : MarshalByRefObject
    {

        private AppDomain app;
        private string asmPath;
        private object obj;

        public Assembly Assembly { get { return getAssembly(); } }
        public string AssemblyPath { get { return asmPath; } }

        public ProxyDomain(string assemblyPath, string typeName)
        {
            AppDomainSetup domainSetup = AppDomain.CurrentDomain.SetupInformation;
            domainSetup.PrivateBinPath += ";" + Project.ProjectManager.Instance.ActiveProject.PathBin;
            domainSetup.PrivateBinPathProbe += ";" + Project.ProjectManager.Instance.ActiveProject.PathBin;
            
            app = AppDomain.CreateDomain("someApp", null, domainSetup);
            try
            {
                obj = app.CreateInstanceFromAndUnwrap(assemblyPath, typeName);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            asmPath = assemblyPath;
            ProxyDomainManager.Instance.addProxyDomain(this);
        }

        private Assembly getAssembly()
        {
            return obj.GetType().Assembly;
        }


        public void closeDomain()
        {
            obj = null;
            AppDomain.Unload(app);
            File.Delete(AssemblyPath);
        }

   
    }
}
