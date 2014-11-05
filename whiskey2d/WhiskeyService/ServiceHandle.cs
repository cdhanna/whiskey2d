using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyService
{
    public class ServiceHandle<S> where S : Service
    {

        private string assemblyPath;
        private string typeName;

        public string AssemblyPath { get { return assemblyPath; } }
        public string TypeName { get { return typeName; } }

        public ServiceHandle(string assemblyPath, string typeName)
        {
            this.assemblyPath = assemblyPath;
            this.typeName = typeName;

        }

    }
}
