using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.CodeDom;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Whiskey2D.Core;

namespace Whiskey2D.Services
{
    public abstract class ServiceDescriptor
    {
        public abstract String SrcPath { get; }
        public abstract String DllPath { get; }
        public abstract string QualifiedTypeName { get; }
        public abstract CodeCompileUnit Code { get; }
        public abstract String[] References { get; }
        protected Library library;
        protected ServiceCollection servColl;

        static int sid = 0;

        protected static Dictionary<string, ServiceDescriptor> typeNameTable = new Dictionary<string, ServiceDescriptor>();
        public ServiceDescriptor lookUpDescriptorByName(string qualifiedName)
        {
            List<String> oldKeys = typeNameTable.Keys.ToList();
            foreach (String oldKey in oldKeys)
            {
                ServiceDescriptor desc = typeNameTable[oldKey];
                typeNameTable.Remove(oldKey);
                typeNameTable.Add(desc.QualifiedTypeName, desc);
            }
            return typeNameTable[qualifiedName];
        }


        public ServiceDescriptor(ServiceCollection servColl)
        {
            library = new Library("APP." + QualifiedTypeName); //takes around 800 ms to create. Groooss
            this.servColl = servColl;
            this.servColl.addServiceDescriptor(this);
            typeNameTable.Add("temp" + (sid++), this);
        }

        
        public abstract List<ServiceDescriptor> getReferences(List<ServiceDescriptor> visitedRefs);
        public abstract ServiceCollection compileToDisc();

        protected void unloadLibrary()
        {
            ServiceDescriptor kill = lookUpDescriptorByName(QualifiedTypeName);
            typeNameTable.Remove(kill.QualifiedTypeName);

            library.unload();
        }
       

        protected void displayOutput(System.Collections.Specialized.StringCollection output)
        {
            foreach (String o in output)
            {
                Console.WriteLine(o);
            }
        }

        protected void displayErrors(CompilerErrorCollection errors)
        {
            foreach (CompilerError error in errors)
            {
                Console.WriteLine(error.ToString());
            }

        }
    }

    public abstract class ServiceDescriptor <S> : ServiceDescriptor where S : Service
    {
        protected ServiceDescriptor(ServiceCollection servColl) : base(servColl) { }
       

        private ServiceHandle<S> lastHandle;
       

        public S createServiceInstance()
        {
            if (lastHandle == null)
            {
                compileToDisc();
            }


            S serv = lastHandle.createServiceInstance(library);
            servColl.addServiceInstance(serv, lastHandle);
            return serv;
        }


       

        
        public override ServiceCollection compileToDisc()
        {

            //determine doomset
           

            //get all of the bad descriptors that need to be re-compiled
            //List<ServiceDescriptor> badDescs = servColl.buildDoomedTypeSet(this);


            runCompile();
            
            

            //List<S> converted = new List<S>();
            //foreach (Temp temp in temps)
            //{
            //    S conv = handle.createServiceInstance(library);
            //    converted.Add(temp.populateFromMemory(conv));
            //}
            //return converted;

            typeNameTable.Add(QualifiedTypeName, this);

            return servColl;
           // return handle;
        }


        private void runCompile()
        {
            

            //compile
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters options = new CompilerParameters();
            CodeGeneratorOptions genOptions = new CodeGeneratorOptions();

            options.OutputAssembly = DllPath;
            options.GenerateInMemory = false;
            options.GenerateExecutable = false;

            genOptions.BlankLinesBetweenMembers = true;
            genOptions.BracingStyle = "C";



            foreach (string reference in References)
            {
                options.ReferencedAssemblies.Add(reference);
            }

            List<ServiceDescriptor> servRefs = getReferences(new List<ServiceDescriptor>());
            //List<ServiceDescriptor<S>> servRefs = getServiceReferences(new List<ServiceDescriptor<S>>());

            CodeCompileUnit[] compileUnits = new CodeCompileUnit[servRefs.Count + 1];
            for (int i = 0; i < servRefs.Count; i++)
            {
                options.ReferencedAssemblies.Add(servRefs[i].DllPath);
                compileUnits[i] = servRefs[i].Code;
            }
            compileUnits[servRefs.Count] = Code;


            using (StreamWriter sourceWriter = new StreamWriter(SrcPath))
            {
                provider.GenerateCodeFromCompileUnit(Code, sourceWriter, genOptions);
            }


            if (File.Exists(DllPath))
            {
                unloadLibrary();
            }
            CompilerResults results = provider.CompileAssemblyFromDom(options, Code);
            
            
            if (results.Errors.Count > 0)
            {
                displayOutput(results.Output);
                displayErrors(results.Errors);

                throw new Exception("Compile failed");
            }



            ServiceHandle<S> handle = new ServiceHandle<S>(DllPath, QualifiedTypeName);
            lastHandle = handle;

        }


    }
}
