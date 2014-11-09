using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Services
{
    public interface ServiceProperty
    {

        String Name { get; }
        //Type PropertyType { get; }

        object get(Service service);
        void set(Service service, object value);
    }
}
