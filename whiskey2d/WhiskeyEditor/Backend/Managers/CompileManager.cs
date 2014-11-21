using System;
using System.Collections.Generic;
using System.Linq;
using System.CodeDom;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using WhiskeyEditor.Project;
using System.Threading;
using SmallMVC;
using WhiskeyEditor.Backend.Events;

namespace WhiskeyEditor.Backend.Managers
{

    #region Event Setup
    public delegate void CompileEventHandler(object sender, CompileEventArgs args);
    public class CompileEventArgs : EventArgs
    {
        private CompilerErrorCollection errors;
        public CompilerErrorCollection Errors { get { return this.errors; } }

        public CompileEventArgs(CompilerErrorCollection errors)
        {
            this.errors = errors;
        }
    }
    #endregion 

    class CompileManager
    {

        private static CompileManager instance = new CompileManager();
        public static CompileManager Instance { get { return instance; } }

        #region Events

        public event CompileEventHandler Compiled;

        private void fireCompiled(CompileEventArgs args)
        {
            if (Compiled != null)
            {
                Compiled(this, args);
            }
        }

        #endregion

        public CompileManager() : base()
        {
            
        }



        public string compile()
        {
            return compile(true, ProjectManager.Instance.ActiveProject.PathLib + Path.DirectorySeparatorChar + "GameData.dll");
        }
        public string compile(bool ensured, string dllOutputPath)
        {

           
            CSharpCodeProvider compiler = new CSharpCodeProvider();
            CompilerParameters options = new CompilerParameters();

            options.GenerateInMemory = false;
            options.OutputAssembly = dllOutputPath;
            options.ReferencedAssemblies.Add(ResourceFiles.DllMonoGame);
            options.ReferencedAssemblies.Add(ResourceFiles.DllSystem);
            options.ReferencedAssemblies.Add(ResourceFiles.DllWhiskeyCore);


            List<string> filePaths = new List<string>();
            foreach (FileDescriptor fileDesc in FileManager.Instance.FileDescriptors)
            {
                if (ensured)
                {
                    fileDesc.ensureFileExists();
                }

                if (File.Exists(fileDesc.FilePath))
                {
                    filePaths.Add(fileDesc.FilePath);
                }
            }
            try
            {
                CompilerResults results = compiler.CompileAssemblyFromFile(options, filePaths.ToArray());

                fireCompiled( new CompileEventArgs(results.Errors));
                
                return options.OutputAssembly;

            }
            catch (Exception e)
            {
                throw new WhiskeyException("Could not compile" + e.Message);
                //return "CouldNotBuild";
            }

        }


    }
}
