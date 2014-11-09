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
        private List<S> instances;

        private int handleId;
        private static int sid = 0;

        public int HandleID { get { return handleId; } }
        public string AssemblyName { get { return assemblyName; } }
        public string TypeName { get { return typeName; } }

        private ServiceHandle<Service> converted;

        public ServiceHandle(string assemblyName, string typeName)
        {
            instances = new List<S>();
            this.assemblyName = assemblyName;
            this.typeName = typeName;
            this.handleId = sid++;

        }

        private ServiceHandle(string assemblyName, string typeName, int id, List<S> servs, ServiceHandle<Service> self)
        {
            instances = servs;
            this.assemblyName = assemblyName;
            this.typeName = typeName;
            this.handleId = id;
            this.converted = self;
        }

        public List<Temp> buildTempSet()
        {
            List<Temp> temps = new List<Temp>();
            foreach (S inst in instances)
            {
                Temp temp = new Temp(inst);
                temps.Add(temp);
            }

            return temps;
        }


        public ServiceHandle<Service> convertUp()
        {
            if (converted == null)
            {
                List<Service> servList = new List<Service>();
                instances.ForEach((e) => { servList.Add(e); });

                ServiceHandle<Service> serv = new ServiceHandle<Service>(assemblyName, typeName, handleId, servList, converted);
                converted = serv;
                return serv;
            }
            else
            {
                return converted;
            }
            
        }

        public S createServiceInstance(Library library)
        {
            S serv = library.instantiate(this);

            

            instances.Add(serv);
            return serv;
        }

 

    }
}
