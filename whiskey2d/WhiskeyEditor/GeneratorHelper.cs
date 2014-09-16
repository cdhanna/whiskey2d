using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyEditor
{
    class GeneratorHelper
    {

        public static List<String> getCommonUsingStatements()
        {
            List<String> statements = new List<string>();
            statements.Add("using System;");
            statements.Add("using System.Collections.Generic;");
            statements.Add("using System.Linq;");
            statements.Add("using System.Text;");
            statements.Add("using Whiskey2D.Core;");
            statements.Add("using Microsoft.Xna.Framework;");
            statements.Add("using Microsoft.Xna.Framework.Input;");
            return statements;

        }


        public static void generateStarter(string directory, string nameSpace)
        {
            StreamWriter writer = File.CreateText(directory + "\\Src\\Launch.cs");
            getCommonUsingStatements().ForEach((line) => { writer.WriteLine(line); });
            writer.Write("namespace " + nameSpace + @"
{
    class Launch : Starter
    {

        public override void start()
        {
        }
    }
}" );

            writer.Close();


        }


    }
}
