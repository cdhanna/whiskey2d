using System;
using System.Collections.Generic;
using System.Linq;
using System.CodeDom;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace WhiskeyEditor.Backend.Managers
{
    class CompileManager
    {

        private static CompileManager instance = new CompileManager();
        public static CompileManager Instance { get { return instance; } }


        private List<FileDescriptor> fileDescs;

        private CompileManager()
        {
            fileDescs = new List<FileDescriptor>();
        }

        public void addFileDescriptor(FileDescriptor fileDesc)
        {
            fileDescs.Add(fileDesc);
        }


        public void compile()
        {


            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters options = new CompilerParameters();

            options.GenerateInMemory = false;
            options.OutputAssembly = "TESTOUT.dll";
            options.ReferencedAssemblies.Add(ResourceFiles.DllMonoGame);
            options.ReferencedAssemblies.Add(ResourceFiles.DllSystem);
            options.ReferencedAssemblies.Add(ResourceFiles.DllWhiskeyCore);


            List<string> filePaths = new List<string>();
            foreach (FileDescriptor fileDesc in fileDescs)
            {
                fileDesc.ensureFileExists();
                filePaths.Add(fileDesc.FilePath);
            }

            CompilerResults results = compiler.CompileAssemblyFromFile(options, filePaths.ToArray());

            Console.WriteLine("Compiler Out; Error Count = " + results.Errors.Count);
            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }

        }


    }
}
