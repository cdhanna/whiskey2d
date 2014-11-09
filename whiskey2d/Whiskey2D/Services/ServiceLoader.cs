using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Services
{
    public class ServiceLoader
    {

        private static ServiceLoader instance = new ServiceLoader();
        public static ServiceLoader Instance { get { return instance; } }

        private ServiceLoader()
        {


        }


        public Library createLibrary(string libraryName)
        {
            Library lib = new Library(libraryName);
            return lib;
        }

    }
}
