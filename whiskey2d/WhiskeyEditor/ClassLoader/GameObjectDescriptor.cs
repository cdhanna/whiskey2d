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

namespace WhiskeyEditor.ClassLoader
{


    /// <summary>
    /// The GameObject Descriptor is responsible for describing the type name and set of properties for a GameObject sub-class. 
    /// </summary>
    public class GameObjectDescriptor
    {
        public static Dictionary<GameObjectDescriptor, Assembly> descToAsmMap = new Dictionary<GameObjectDescriptor,Assembly>();
        static Dictionary<GameObjectDescriptor, CodeCompileUnit> descToUnitMap = new Dictionary<GameObjectDescriptor, CodeCompileUnit>();
        static int asmCounter = 0;


        public CodeCompileUnit targetUnit;
        CodeTypeDeclaration targetClass;


        private List<PropertyDescriptor> pds;
        private List<PropertyDescriptor> parentProperties;
        
        public String Name { get; set; }
        public String NameSpace { get; set; }
        public List<PropertyDescriptor> Properties { get { return pds; } set { pds = value; } }


        public String QualifiedName { get { return NameSpace + "." + Name; } }

        /// <summary>
        /// Create a GameObjectDescriptor from another descriptor. This is the copy constructor
        /// </summary>
        /// <param name="other">A non-null GobDescr </param>
        public GameObjectDescriptor(GameObjectDescriptor other)
        {
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

        /// <summary>
        /// Create a GameObjectDescriptor
        /// </summary>
        /// <param name="nameSpace">The namespace that the gameobject sub class resides in </param>
        /// <param name="name">the name of the gameobject sub class</param>
        public GameObjectDescriptor(String nameSpace, String name)
        {
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

            CodeAttributeDeclaration classAttr = new CodeAttributeDeclaration("Serializable");
            targetClass.CustomAttributes.Add(classAttr);
            targetClass.BaseTypes.Add("Whiskey2D.Core.GameObject");

            samples.Types.Add(targetClass);
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
                    field.Type = new CodeTypeReference(p.Type);

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
                    prop.Type = new CodeTypeReference(p.Type);
                    prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower())));
                    prop.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower()), new CodePropertySetValueReferenceExpression()));
                    targetClass.Members.Add(prop);
  
                }
            }
        }

        private void addConstructor()
        {
            CodeConstructor cons = new CodeConstructor();
            cons.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            targetClass.Members.Add(cons);

            foreach (PropertyDescriptor p in pds)
            {
                GameObjectConfigurator.getInstance().setInitialValueFor(QualifiedName, p.Name, p.Value);
                
                if (!parentProperties.Contains(p))
                {
                    cons.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), p.Name),
                        new CodeCastExpression(p.Type, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(GameObjectConfigurator)), "getInitialValueFor",
                            new CodePrimitiveExpression(QualifiedName), new CodePrimitiveExpression(p.Name)))));
                }
                else
                {
                    cons.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), p.Name),
                       new CodeCastExpression(p.Type, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(GameObjectConfigurator)), "getInitialValueFor",
                           new CodePrimitiveExpression(QualifiedName), new CodePrimitiveExpression(p.Name)))));
                }
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
            
            options.OutputAssembly =ProjectManager.Instance.ActiveProject.PathLib + Path.DirectorySeparatorChar +  QualifiedName +asmCounter.ToString()+ ".dll";
           // options.LinkedResources.Add("Whiskey2D.Core");
            asmCounter++;
            
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

            Assembly compiledAssembly = results.CompiledAssembly;
            if (compiledAssembly != null)
            {
                Console.WriteLine("built an asm: " + compiledAssembly.Location);

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



    }
}
