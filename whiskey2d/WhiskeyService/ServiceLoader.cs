using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyService
{
    public class ServiceLoader
    {

        private static ServiceLoader instance = new ServiceLoader();
        public static ServiceLoader Instance { get { return instance; } }

        private ServiceLoader()
        {

        }


        public Library createLibrary()
        {
            DefaultLibrary library = new DefaultLibrary();
            return library;
        }

        public void releaseLibrary(Library library)
        {
            library.unload();
        }



    }
}
