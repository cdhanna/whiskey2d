using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyService
{
    public interface Service
    {

        string AssemblyPath { get; set; }

        object invoke(string commandName, object[] parameters);
        


    }
}
