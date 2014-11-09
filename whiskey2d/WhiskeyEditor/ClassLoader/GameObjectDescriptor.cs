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
using WhiskeyEditor.Project;
using WhiskeyEditor.Parsers;
using Whiskey2D.Services;

namespace WhiskeyEditor.ClassLoader
{


    /// <summary>
    /// The GameObject Descriptor is responsible for describing the type name and set of properties for a GameObject sub-class. 
    /// </summary>
    public class GameObjectDescriptor : ServiceDescriptor<GameObjectService>
    {
        public static Dictionary<GameObjectDescriptor, Assembly> descToAsmMap = new Dictionary<GameObjectDescriptor,Assembly>();
        static Dictionary<GameObjectDescriptor, CodeCompileUnit> descToUnitMap = new Dictionary<GameObjectDescriptor, CodeCompileUnit>();
        static int asmCounter = 0;


        public CodeCompileUnit targetUnit;
        private CodeTypeDeclaration targetClass;


        private List<PropertyDescriptor> pds;
        private List<PropertyDescriptor> parentProperties;
        
        public String Name { get; set; }
        public String NameSpace { get; set; }
        public List<PropertyDescriptor> Properties { get { return pds; } set { pds = value; } }


        private List<string> dllPaths;
        private string latestDllPath;
        public String LatestDllPath { get { return latestDllPath; } }


        public String QualifiedName { get { return NameSpace + "." + Name; } }


        //private static Dictionary<String, ServiceDescriptor<GameObjectService>> serviceNameTable = new Dictionary<string, ServiceDescriptor<GameObjectService>>();

        /// <summary>
        /// Create a GameObjectDescriptor from another descriptor. This is the copy constructor
        /// </summary>
        /// <param name="other">A non-null GobDescr </param>
        public GameObjectDescriptor(GameObjectDescriptor other) : base (new ServiceCollection())
        {

            

            dllPaths = new List<string>();
            NameSpace = other.NameSpace;
            Name = other.Name;
            Properties = new List<PropertyDescriptor>();
            parentProperties = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor pd in other.parentProperties)
            {
                parentProperties.Add(new PropertyDescriptor(pd));
            }
            foreach (PropertyDescriptor pd in other.Properties)
            {
                Properties.Add(new PropertyDescriptor(pd));
            }

        }


        public GameObjectDescriptor(String nameSpace, String name)
            : this(new ServiceCollection(), nameSpace, name)
        {

        }

        /// <summary>
        /// Create a GameObjectDescriptor
        /// </summary>
        /// <param name="nameSpace">The namespace that the gameobject sub class resides in </param>
        /// <param name="name">the name of the gameobject sub class</param>
        public GameObjectDescriptor(ServiceCollection servColl, String nameSpace, String name) : base(servColl)
        {
            dllPaths = new List<string>();
            NameSpace = nameSpace;
            Name = name;
            pds = new List<PropertyDescriptor>();
            parentProperties = new List<PropertyDescriptor>();
            Type gobType = typeof(GameObject);
            PropertyInfo[] gobProps = gobType.GetProperties();
            foreach (PropertyInfo p in gobProps)
            {
                if (p.SetMethod != null)
                {
                    object value = GameObjectConfigurator.getInitialValueFor("GameObject", p.Name);
                    PropertyDescriptor pd = new PropertyDescriptor(p.Name, p.PropertyType, value);
                    parentProperties.Add(pd);
                    pds.Add(pd);
                }
            }
           // serviceNameTable.Add(QualifiedTypeName, this );

        }


       
        private void setUpClass()
        {
            targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace(NameSpace);
            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("Whiskey2D.Core"));
            targetClass = new CodeTypeDeclaration(Name);            
            targetClass.IsClass = true;
            targetClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Serializable;

            CodeTypeDeclaration dumbClass = new CodeTypeDeclaration("DumbWhiskeyDumb");
            dumbClass.IsClass = true;
            dumbClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Serializable;
            dumbClass.BaseTypes.Add("System.MarshalByRefObject");

            CodeAttributeDeclaration classAttr = new CodeAttributeDeclaration("Serializable");
            targetClass.CustomAttributes.Add(classAttr);
            targetClass.BaseTypes.Add("Whiskey2D.Core.GameObject");
           

            samples.Types.Add(targetClass);
            samples.Types.Add(dumbClass);
            targetUnit.Namespaces.Add(samples);
        }

        private void addFields()
        {

            foreach (PropertyDescriptor p in pds)
            {
                if (!parentProperties.Contains(p))
                {
                    CodeMemberField field = new CodeMemberField();
                    field.Attributes = MemberAttributes.Private;
                    field.Name = p.Name.ToLower();
                   

                    if (!p.Type.Equals(typeof(GameObject)))
                    {
                        field.Type = new CodeTypeReference(p.Type);
                    }
                    else
                    {

                        field.Type = new CodeTypeReference(((GameObject)p.Value).getServiceTypeName());
                    }

                    targetClass.Members.Add(field);
                }
            }
        }

        private void addProperties()
        {
            foreach (PropertyDescriptor p in pds)
            {
                if (!parentProperties.Contains(p))
                {
                    CodeMemberProperty prop = new CodeMemberProperty();
                    prop.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    prop.Name = p.Name.ToUpper().Substring(0, 1) + p.Name.Substring(1);
                    prop.HasGet = true;
                    prop.HasSet = true;
                    if (!p.Type.Equals(typeof(GameObject)) )
                    {
                        prop.Type = new CodeTypeReference(p.Type);
                    }
                    else
                    {

                        prop.Type = new CodeTypeReference(((GameObject)p.Value).getServiceTypeName());
                    }
                    
                    prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower())));
                    prop.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower()), new CodePropertySetValueReferenceExpression()));
                    targetClass.Members.Add(prop);
  
                }
            }
        }


        private string parsePrim(object val)
        {
            Type t = val.GetType();
            string str = "";
            if (t == typeof(int) || t == typeof(Single))
            {
                return "" + val;
            }

            return str;
        }

        
        private Dictionary<string, int> nameTable = new Dictionary<string,int>();
        private string generateName(string name)
        {
            name = "_" + name;
            if (nameTable.ContainsKey(name))
            {
                nameTable[name]++;
                return name + nameTable[name];
            }
            else
            {
                nameTable.Add(name, 0);
                return name;
            }
        }
        private string newLine = Environment.NewLine + "\t\t\t";


        private string codeFor(object val, string name)
        {

            if (Parse.isTerminable(val))
            {
                return Parse.stringify(val) + ";";
            }
            else
            {

                string code;

                if (!val.GetType().Equals(typeof(GameObject)))
                {
                    code = "new " + val.GetType().Name + "(); " + newLine;

                    PropertyInfo[] props = val.GetType().GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.SetMethod != null && prop.SetMethod.IsPublic)
                        {
                            string pName = name + "." + prop.Name;
                            object pVal = prop.GetGetMethod().Invoke(val, new object[] { });
                            string pValName = generateName(name + prop.Name);

                            code = code + prop.PropertyType.Name + " " + pValName + " = " + codeFor(pVal, pValName) + newLine;
                            code = code + pName + " = " + pValName + ";" + newLine;
                            code = code + newLine;
                        }
                    }
                }
                else
                {
                    code = "new " + ((GameObject)val).getServiceTypeName() + "(); " + newLine;
                }
                return code;
            }

        }


        private void addConstructor()
        {
            CodeConstructor cons = new CodeConstructor();
            cons.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            targetClass.Members.Add(cons);
            nameTable.Clear();
            foreach (PropertyDescriptor p in pds)
            {

                cons.Statements.Add(new CodeSnippetStatement( newLine + p.Name + " = " + codeFor(p.Value, p.Name)));
               


                //GameObjectConfigurator.getInstance().setInitialValueFor(QualifiedName, p.Name, p.Value);
                //cons.Statements.Add(new CodeSnippetStatement(";"));
                //if (!parentProperties.Contains(p))
                //{
                //    cons.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), p.Name),
                //        new CodeCastExpression(p.Type, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(GameObjectConfigurator)), "getInitialValueFor",
                //            new CodePrimitiveExpression(QualifiedName), new CodePrimitiveExpression(p.Name)))));
                //}
                //else
                //{
                //    cons.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), p.Name),
                //       new CodeCastExpression(p.Type, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(GameObjectConfigurator)), "getInitialValueFor",
                //           new CodePrimitiveExpression(QualifiedName), new CodePrimitiveExpression(p.Name)))));
                //}
            }

            CodeMemberMethod addInitScripts = new CodeMemberMethod();
            addInitScripts.Name = "addInitialScripts";
            addInitScripts.Attributes = MemberAttributes.Override | MemberAttributes.Family;

            targetClass.Members.Add(addInitScripts);


        }

        /// <summary>
        /// Add property to the gameobject. 
        /// </summary>
        /// <param name="pd"></param>
        public void addProperty(PropertyDescriptor pd)
        {
            pds.Add(pd);
        }

        /// <summary>
        /// Turn the gameobject descriptor into readable source code. 
        /// </summary>
        /// <param name="fileName">The path to create the source .cs file </param>
        public void generateSource(string fileName)
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            

            Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf('\\')));


            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, options);
            }

        }

        public void compile()
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
        
            options.OutputAssembly = ProjectManager.Instance.ActiveProject.PathBin + Path.DirectorySeparatorChar + QualifiedName  + ".dll";

            options.ReferencedAssemblies.Add("Whiskey2D_core.dll");
            options.ReferencedAssemblies.Add("System.dll");

            List<CodeCompileUnit> allUnits = new List<CodeCompileUnit>();
            allUnits.Add(targetUnit);
            foreach (GameObjectDescriptor ds in descToAsmMap.Keys)
            {
                if (ds != this)
                {
                    Assembly asm = descToAsmMap[ds];
                    Console.WriteLine("ASSEMBLY: " + asm.Location);
                    options.ReferencedAssemblies.Add(asm.Location);

                    allUnits.Add(descToUnitMap[ds]);

                }
            }
            CompilerResults results = provider.CompileAssemblyFromDom(options, targetUnit);


            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }
            latestDllPath = options.OutputAssembly;

      
          
        }

        /// <summary>
        /// Compile the gameobject descriptor into an assembly
        /// </summary>
        /// <returns>An assembly containing the compiled code for the gameobject descritpr</returns>
        public Assembly generateSourceInMem()
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
            //options.GenerateInMemory = true;
            //options.GenerateExecutable = false;


            //string dllPath = ProjectManager.Instance.ActiveProject.PathBin + Path.DirectorySeparatorChar + QualifiedName + ".dll";
            //if (File.Exists(dllPath))
            //{
            //    Console.WriteLine("it exists!!!!");
            //    File.Delete(dllPath);
            //}
            //options.OutputAssembly = dllPath;
            options.OutputAssembly =ProjectManager.Instance.ActiveProject.PathBin + Path.DirectorySeparatorChar +  QualifiedName + (asmCounter++) + ".dll";
           // options.LinkedResources.Add("Whiskey2D.Core");
            
            
            options.ReferencedAssemblies.Add("Whiskey2D_core.dll");
            options.ReferencedAssemblies.Add("System.dll");

            List<CodeCompileUnit> allUnits = new List<CodeCompileUnit>();
            allUnits.Add(targetUnit);
            foreach (GameObjectDescriptor ds in descToAsmMap.Keys)
            {
                if (ds != this)
                {
                    Assembly asm = descToAsmMap[ds];
                    Console.WriteLine("ASSEMBLY: " + asm.Location);
                    options.ReferencedAssemblies.Add(asm.Location);

                    allUnits.Add(descToUnitMap[ds]);

                }
            }

            
 
            

            CompilerResults results = provider.CompileAssemblyFromDom(options, targetUnit);

            //in the no error case, we should remember this dll

          

            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }
            latestDllPath = options.OutputAssembly;
     
            Assembly compiledAssembly = results.CompiledAssembly;


            //ProxyDomain proxy = new ProxyDomain(options.OutputAssembly, "Project.DumbWhiskeyDumb");
            //Assembly compiledAssembly = proxy.Assembly;


            if (compiledAssembly != null)
            {
                Console.WriteLine("built an asm: " + options.OutputAssembly);

                if (descToAsmMap.ContainsKey(this))
                {
                    descToAsmMap[this] = compiledAssembly;
                    descToUnitMap[this] = targetUnit;
                }
                else
                {
                    descToAsmMap.Add(this, compiledAssembly);
                    descToUnitMap.Add(this, targetUnit);
                }


            }
            return results.CompiledAssembly;

        }


        public override string QualifiedTypeName
        {
            get {return QualifiedName; }
        }


        private CodeCompileUnit getUnit()
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();
            return this.targetUnit;
        }

        public override CodeCompileUnit Code
        {
            get { return getUnit(); }
        }

        public override String[] References
        {
            get { return new string[] { ResourceFiles.DllSystem, ResourceFiles.DllWhiskeyCore }; }
        }

        public override string DllPath
        {
            get { return ProjectManager.Instance.ActiveProject.PathBin + "\\" + QualifiedTypeName + ".dll" ; }
        }
        public override string SrcPath
        {
            get { return ProjectManager.Instance.ActiveProject.PathSrc + "\\" + QualifiedTypeName + ".cs"; }
        }

        
        public override List<ServiceDescriptor> getReferences(List<ServiceDescriptor> visitedRefs)
        {
            List<ServiceDescriptor> servRefs = new List<ServiceDescriptor>();
            
            foreach (PropertyDescriptor prop in Properties)
            {
                if (prop.Type.Equals (typeof (GameObject) ) )
                {
                    ServiceDescriptor serv = lookUpDescriptorByName(((GameObject)prop.Value).getServiceTypeName());//typeNameTable[((GameObject)prop.Value).getServiceTypeName()];
                    if (!visitedRefs.Contains(serv))
                    {
                        servRefs.Add(serv);
                        servRefs.AddRange(serv.getReferences(servRefs));
                    }
                }
            }


            return servRefs ;
        }
    }
}
