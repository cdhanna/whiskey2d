using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor
{
    class ProxyDomainManager
    {

        private static ProxyDomainManager instance = new ProxyDomainManager();
        public static ProxyDomainManager Instance { get { return instance; } }

        private ProxyDomainManager()
        {
            badProxies = new List<ProxyDomain>();
            pathTable = new Dictionary<string, ProxyDomain>();
        }

        private Dictionary<string, ProxyDomain> pathTable;
        private List<ProxyDomain> badProxies;

        public void addProxyDomain(ProxyDomain proxy)
        {
            pathTable.Add(proxy.AssemblyPath, proxy);
        }

        public void sceduleForClose(string proxyPath)
        {
            ProxyDomain badProxy = pathTable[proxyPath];
            badProxies.Add(badProxy);
        }

        public void update()
        {
            while (badProxies.Count > 0)
            {
                ProxyDomain badProxy = badProxies[0];
                pathTable.Remove(badProxy.AssemblyPath);
                badProxies.Remove(badProxy);
                badProxy.closeDomain();



            }

        }

    }
}
