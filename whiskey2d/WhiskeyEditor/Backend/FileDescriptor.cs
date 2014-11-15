using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class FileDescriptor
    {

        private string filePath;
        private string name;


        public FileDescriptor(string filePath, string name)
        {
            this.filePath = filePath;
            this.name = name;

        }

        public String FilePath
        {
            get
            {
                return this.filePath;
            }
        }
        public String Name
        {
            get
            {
                return this.name;
            }
        }
    }
}
