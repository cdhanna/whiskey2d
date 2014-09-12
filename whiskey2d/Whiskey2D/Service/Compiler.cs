using System.CodeDom.Compiler;
using System.Reflection;
using System;
using System.IO;

namespace Whiskey2D.Service
{
    public class Compiler
    {

        private static Compiler instance;
        public static Compiler getInstance()
        {
            if (instance == null)
            {
                instance = new Compiler();
            }
            return instance;
        }

      
        private Compiler()
        {
            
        }

        /// <summary>
        /// Compile a folder into a dll
        /// </summary>
        /// <param name="dllName">The name of the dll that will get made</param>
        /// <param name="path">The path to the directory that is going to be compiled</param>
        /// <param name="assemblyRefs">A set of dlls that are referenced in the compile code</param>
        public void compileDirectory(string dllName, string path, params string[] assemblyRefs)
        {
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
          
            parameters.OutputAssembly = dllName + ".dll";
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Linq.dll");
           
            foreach (string assmeblyRef in assemblyRefs)
            {
                parameters.ReferencedAssemblies.Add(assmeblyRef + ".dll");
            }
          //  parameters.

            //CompilerResults r = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(parameters, "public class B {public static int k=7;}");
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            

            //list all files in directory
            string[] files = Directory.GetFiles(path);

            CompilerResults results = provider.CompileAssemblyFromFile(parameters, files);

            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }

        }


    }
}
