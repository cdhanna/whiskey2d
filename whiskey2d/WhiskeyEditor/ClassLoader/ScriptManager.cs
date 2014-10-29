using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System;
using System.IO;
using Whiskey2D.Core;



namespace WhiskeyEditor.ClassLoader
{
    class ScriptManager
    {



        private static ScriptManager instance = new ScriptManager();

        public static ScriptManager Instance { get { return instance; } }

        private ScriptManager()
        {

        }


        public void writeToDisc(ScriptDescriptor sdesc)
        {
            string sourceName = Project.ProjectManager.Instance.ActiveProject.PathSrcScripts + Path.DirectorySeparatorChar + sdesc.Name + ".cs";

            FileStream stream = File.Create(sourceName);

            string src = sdesc.generateScript();
            foreach (char c in src.ToCharArray())
            {
                stream.WriteByte((byte)c);
            }

            stream.Close();
        }

        //public void compileScript(ScriptDescriptor sdesc)
        //{

        //    string dllName = Project.ProjectManager.Instance.ActiveProject.PathScrScripts + Path.DirectorySeparatorChar + sdesc.Name + ".cs";

        //    System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
        //    parameters.GenerateExecutable = false;
        //    parameters.OutputAssembly = dllName + ".dll";

        //    //add required refs
        //    parameters.ReferencedAssemblies.Add("System.dll");
        //    parameters.ReferencedAssemblies.Add("System.Linq.dll");

        //    //add custom references
        //    foreach (string assmeblyRef in assemblyRefs)
        //    {
        //        parameters.ReferencedAssemblies.Add(assmeblyRef + ".dll");
        //    }

        //    //create the code provider
        //    CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

        //    //get all files in directory
        //    string[] files = Directory.GetFiles(path);

        //    //run compilation
        //    CompilerResults results = provider.CompileAssemblyFromFile(parameters, files);

        //    //display results
        //    foreach (String line in results.Output)
        //    {
        //        Console.WriteLine(line);
        //    }

        //}
        


    }
}
