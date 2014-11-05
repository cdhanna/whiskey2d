using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Services
{
    public class ServiceHandle <S> where S : Service
    {
        private string assemblyName;
        private string typeName;

        public string AssemblyName { get { return assemblyName; } }
        public string TypeName { get { return typeName; } }


        public ServiceHandle(string assemblyName, string typeName)
        {
            this.assemblyName = assemblyName;
            this.typeName = typeName;

        }

    }
}
