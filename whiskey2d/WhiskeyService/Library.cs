using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyService
{
    public interface Library
    {

        S addService<S>(ServiceHandle<S> handle) where S : Service;

        //Service addService(Type serviceType);

       
        void unload();

    }
}
