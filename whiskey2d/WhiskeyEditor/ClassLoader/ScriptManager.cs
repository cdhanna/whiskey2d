using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System;
using System.IO;
using Whiskey2D.Core;
using System.Collections.Generic;


namespace WhiskeyEditor.ClassLoader
{
    class ScriptManager
    {



        private static ScriptManager instance = new ScriptManager();

        public static ScriptManager Instance { get { return instance; } }


        private Dictionary<int, ScriptDescriptor> idScriptTable;
        private Dictionary<string, ScriptDescriptor> nameScriptTable;

        private ScriptManager()
        {
            idScriptTable = new Dictionary<int, ScriptDescriptor>();
            nameScriptTable = new Dictionary<string, ScriptDescriptor>();
        }


        public void addScriptDescriptor(ScriptDescriptor sdesc)
        {
            if (!idScriptTable.ContainsKey(sdesc.Id))
            {
                idScriptTable.Add(sdesc.Id, sdesc);
                nameScriptTable.Add(sdesc.Name, sdesc);
            }
            
        }


        public void writeToDisc(ScriptDescriptor sdesc)
        {
            string sourceName = Project.ProjectManager.Instance.ActiveProject.PathSrcScripts + Path.DirectorySeparatorChar + sdesc.Name + ".cs";

            FileStream stream = File.Create(sourceName);

            string src = sdesc.Code;
            foreach (char c in src.ToCharArray())
            {
                stream.WriteByte((byte)c);
            }

            stream.Close();
        }


        public void compile(ScriptDescriptor sdesc)
        {
            string dllName = Project.ProjectManager.Instance.ActiveProject.PathBin + Path.DirectorySeparatorChar + "Script." + sdesc.Name + ".dll";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters options = new CompilerParameters();

            options.OutputAssembly = dllName;
            options.GenerateInMemory = true;
            options.GenerateExecutable = false;
            options.ReferencedAssemblies.Add(ResourceFiles.DllMonoGame);
            options.ReferencedAssemblies.Add(ResourceFiles.DllSystem);
            options.ReferencedAssemblies.Add(ResourceFiles.DllWhiskeyCore);


            foreach (GameObjectDescriptor ds in GameObjectDescriptor.descToAsmMap.Keys)
            {
               // if (ds != this)
                {
                    Assembly GobdAsm = GameObjectDescriptor.descToAsmMap[ds];
                    Console.WriteLine("ASSEMBLY: " + GobdAsm.Location);
                    options.ReferencedAssemblies.Add(GobdAsm.Location);

                    

                }
            }


            CompilerResults results = provider.CompileAssemblyFromSource(options, sdesc.Code);

            

            Assembly asm = Assembly.LoadFrom(dllName);
            sdesc.LatestBuiltType = asm.GetType("Project.Scripts."+sdesc.Name);
            sdesc.LatestDllPath = dllName;



            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }

        }


        public ScriptDescriptor getFromName(string name)
        {
            if (nameScriptTable.ContainsKey(name))
            {
                return nameScriptTable[name];
            }
            else return null;
        }


    }
}
