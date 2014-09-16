using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyEditor
{
    class Project
    {
        private ScriptGenerator scriptMaker;

        private string directory;
        private string name;


        public Project(string directory, string name)
        {
            this.directory = directory;
            this.name = name;
            this.scriptMaker = new ScriptGenerator(name);
        }

        public string Directory { get { return this.directory; } }
        public string Name { get { return this.name; } }
        public string NameNoExt { get { return Name.Substring(0, Name.Length - 4); } }


        public void addNewScript(string scriptName)
        {
            //File.Create(directory + "\\Src\\" + scriptName + ".cs");
            this.scriptMaker.writeShell(directory + "\\Src", scriptName);
        }

        public void addNewGameObject(string objectName)
        {
            File.Create(directory + "\\Src\\" + objectName + ".cs");
        }

    }
}
