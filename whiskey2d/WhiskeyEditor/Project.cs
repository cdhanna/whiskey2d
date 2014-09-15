using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor
{
    class Project
    {
        private string directory;
        private string name;


        public Project(string directory, string name)
        {
            this.directory = directory;
            this.name = name;

        }

        public string Directory { get { return this.directory; } }
        public string Name { get { return this.name; } }


    }
}
