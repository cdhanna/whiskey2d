using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyService
{
    public abstract class DefaultService : MarshalByRefObject, Service
    {
        public object invoke(string commandName, object[] parameters)
        {
            Type[] types = new Type[parameters.Length];
            for (int i = 0 ; i < parameters.Length ; i ++){
                types[i] = parameters[i].GetType();
            } 
            return this.GetType().GetMethod(commandName, types).Invoke(this, parameters);
        }

        public string AssemblyPath
        {
            get;
            set;
        }
    }
}
