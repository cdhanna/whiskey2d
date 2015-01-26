using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.compile_types.Types;
using WhiskeyEditor.compile_types.Scripts;

namespace WhiskeyEditor.compile_types
{
    
    class CoreTypes
    {
        public static readonly string corePath = "compile-types" + Path.DirectorySeparatorChar;
        public static readonly string corePathTypes = corePath + "Types" + Path.DirectorySeparatorChar;
        public static readonly string corePathScripts = corePath + "Scripts" + Path.DirectorySeparatorChar;
        
        public static readonly List<String> names;
        public static readonly List<String> filepaths;
        
        public static readonly Dictionary<string, CoreTypeDescriptor> typeFileNameMap;
        public static readonly Dictionary<Type, CoreTypeDescriptor> typeMap;


        public static readonly Dictionary<string, CoreScriptDescriptor> scriptFileNameMap;
        public static readonly Dictionary<Type, CoreScriptDescriptor> scriptMap;

        static CoreTypes()
        {
            names = new List<string>();
            filepaths = new List<string>();
            typeFileNameMap = new Dictionary<string, CoreTypeDescriptor>();
            typeMap = new Dictionary<Type, CoreTypeDescriptor>();

            scriptFileNameMap = new Dictionary<string, CoreScriptDescriptor>();
            scriptMap = new Dictionary<Type, CoreScriptDescriptor>();

            addCoreScript<TriggerActivate>();
            addCoreType<SimpleObject>();
            addCoreType<DebugObject>();
            addCoreType<TriggerZone>();

           

        }

        public static void addCoreTypesToFileManager()
        {
            foreach (CoreTypeDescriptor f in typeFileNameMap.Values)
            {
                FileManager.Instance.addFileDescriptor(f); //add type
            }
            foreach (CoreScriptDescriptor f in scriptFileNameMap.Values)
            {
                FileManager.Instance.addFileDescriptor(f); //add script
            }
        }

        private static void addCoreType<C>() where C : CoreTypeDescriptor
        {

            
            C coreDesc = (C)typeof(C).GetConstructor(new Type[] {  }).Invoke(new object[]{});
            coreDesc.configure();

            names.Add(typeof(C).Name);
            
            string filePathWithoutSlashes = corePathTypes + typeof(C).Name;
            filepaths.Add(filePathWithoutSlashes);
            while (filePathWithoutSlashes.EndsWith("\\"))
                filePathWithoutSlashes = filePathWithoutSlashes.Substring(0, filePathWithoutSlashes.Length - 1);

            typeFileNameMap.Add(filePathWithoutSlashes + ".cs", coreDesc);
            typeMap.Add(typeof(C), coreDesc);
            

        }

        private static void addCoreScript<C>() where C : CoreScriptDescriptor
        {
            Type t = typeof(C);

            C coreDesc = (C)t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            coreDesc.configure();

            names.Add(t.Name);

            

            string filePathWithoutSlashes = corePathScripts + t.Name;
            filepaths.Add(filePathWithoutSlashes);
            while (filePathWithoutSlashes.EndsWith("\\"))
                filePathWithoutSlashes = filePathWithoutSlashes.Substring(0, filePathWithoutSlashes.Length - 1);

            scriptFileNameMap.Add(filePathWithoutSlashes + ".cs", coreDesc);
            scriptMap.Add(typeof(C), coreDesc);

            coreDesc.createFile();
        }


        public static CoreTypeDescriptor lookUpType(string fileName)
        {
            if (typeFileNameMap.ContainsKey(fileName))
            {
                return typeFileNameMap[fileName];
            }
            else return null;
        }
        public static CoreScriptDescriptor lookUpScript(string fileName)
        {
            if (scriptFileNameMap.ContainsKey(fileName))
            {
                return scriptFileNameMap[fileName];
            }
            else return null;
        }

        public static CoreTypeDescriptor getType<C>() where C : CoreTypeDescriptor
        {
            return typeMap[typeof(C)];
        }
        public static CoreScriptDescriptor getScript<C>() where C : CoreScriptDescriptor
        {
            return scriptMap[typeof(C)];
        }

    }
}
