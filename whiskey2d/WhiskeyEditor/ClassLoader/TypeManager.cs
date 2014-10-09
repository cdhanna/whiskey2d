using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Whiskey2D.Core;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;



namespace WhiskeyEditor.ClassLoader
{

    delegate void DescriptorAddedListener(GameObjectDescriptor descr, Type type);

    class TypeManager
    {

        List<GameObjectDescriptor> descritors;
        List<Type> gobTypes;


        List<DescriptorAddedListener> addListeners;

        private static TypeManager instance = new TypeManager();
        public static TypeManager getInstance()
        {
            return instance;
        }

        private TypeManager()
        {
            descritors = new List<GameObjectDescriptor>();
            gobTypes = new List<Type>();
            addListeners = new List<DescriptorAddedListener>();
        }


        public void addDescriptor(GameObjectDescriptor descr)
        {
            descritors.Add(descr);
            Type type = convertDescriptorToType(descr);
            gobTypes.Add(type);
            Console.WriteLine("hello " + addListeners.Count);
            foreach (DescriptorAddedListener da in addListeners)
            {
                da(descr, type);

            }

        }

        public void addDescriptorAddedListener( DescriptorAddedListener addedListener )
        {
            addListeners.Add(addedListener);
        }



        public static Type convertDescriptorToType(GameObjectDescriptor desc)
        {
            Assembly asm = desc.generateSourceInMem();
            Type type = asm.GetType(desc.QualifiedName);

            return type;

        }




        public static string convertDescriptorToFile(GameObjectDescriptor desc)
        {

            string filePath = desc.QualifiedName;
            filePath = filePath.Replace('.', '\\');
            filePath = filePath + ".cs";

            desc.generateSource(filePath);

            return filePath;
        }

        public static Type convertFileToType(string filePath)
        {

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = true;
            options.GenerateExecutable = false;
            // options.LinkedResources.Add("Whiskey2D.Core");
            options.ReferencedAssemblies.Add("Whiskey2D.dll");
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Linq.dll");


            CompilerResults results = provider.CompileAssemblyFromFile(options, filePath);
            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }

            Assembly asm = results.CompiledAssembly;

            string className = filePath.Replace('\\', '.');
            className = className.Replace(".cs", "");

            return asm.GetType(className);
        }

        public static GameObjectDescriptor convertTypeToDescriptor(Type gobType)
        {

            if (gobType.IsSubclassOf(typeof(GameObject))){


                string nameSpace = gobType.Namespace;
                string name = gobType.Name;

                GameObjectDescriptor gobd = new GameObjectDescriptor(nameSpace, name);
                GameObject gobInstance = (GameObject)gobType.GetConstructor(new Type[] { }).Invoke(new object[] { });


                PropertyInfo[] pis = gobType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                
                foreach (PropertyInfo p in pis)
                {

                    string pName = p.Name;
                    Type pType = p.PropertyType;
                    object pValue = p.GetValue(gobInstance);
                    

                    PropertyDescriptor pd = new PropertyDescriptor(pName, pType, pValue );
                    gobd.addProperty(pd);
                }

                gobInstance.close();
                return gobd;

            }


            return null;
        }


    }
}
