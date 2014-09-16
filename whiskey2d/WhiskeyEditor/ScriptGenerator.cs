using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WhiskeyEditor
{
    class ScriptGenerator
    {

        private string nameSpace;

        public ScriptGenerator(string nameSpace)
        {
            this.nameSpace = nameSpace;
        }

        public void writeShell(string path, string scriptName)
        {
            StreamWriter writer = File.CreateText(path + "\\" + scriptName + ".cs");

            GeneratorHelper.getCommonUsingStatements().ForEach((line) =>
            {
                writer.WriteLine(line);
            });

            writer.WriteLine("namespace " + nameSpace);
            writer.WriteLine("{");
            writer.WriteLine("public class " + scriptName + " : Script" );
            writer.WriteLine("{");
            writer.Write(@"
        public override void onStart()
        {
            //TODO add init logic
        }

        public override void onUpdate()
        {
            GameObject gob = Gob;
            //TODO add update logic
        }
");

            writer.WriteLine("}");
            writer.WriteLine("}");


            writer.Close();
        }

    }
}
