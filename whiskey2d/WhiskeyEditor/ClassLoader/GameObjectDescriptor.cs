﻿using System;
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


namespace WhiskeyEditor.ClassLoader
{
    class GameObjectDescriptor
    {

        CodeCompileUnit targetUnit;
        CodeTypeDeclaration targetClass;


        private List<PropertyDescriptor> pds;
        public String Name { get; set; }
        public String NameSpace { get; set; }

        public String QualifiedName { get { return NameSpace + "." + Name; } }

        public GameObjectDescriptor(String nameSpace, String name)
        {
            NameSpace = nameSpace;
            Name = name;
            pds = new List<PropertyDescriptor>();

            


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

                CodeMemberField field = new CodeMemberField();
                field.Attributes = MemberAttributes.Private;
                field.Name = p.Name.ToLower();
                field.Type = new CodeTypeReference(p.Type);
                //field.Comments.Add(new CodeCommentStatement("autogenerated"));
                //field.InitExpression = new CodePrimitiveExpression(p.Value);
                //field.InitExpression = new CodeObjectCreateExpression(p.Type, p.Value);
               // field.InitExpression = new 

                GameObjectConfigurator.getInstance().setInitialValueFor(QualifiedName, field.Name, p.Value);
                field.InitExpression =new CodeCastExpression(p.Type, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(GameObjectConfigurator)), "getInitialValueFor",
                    new CodePrimitiveExpression(QualifiedName), new CodePrimitiveExpression(field.Name)));

                targetClass.Members.Add(field);

            }
        }

        private void addProperties()
        {
            foreach (PropertyDescriptor p in pds)
            {

                CodeMemberProperty prop = new CodeMemberProperty();
                prop.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                prop.Name = p.Name.ToUpper().Substring(0, 1) + p.Name.ToLower().Substring(1);
                prop.HasGet = true;
                prop.HasSet = true;
                prop.Type = new CodeTypeReference(p.Type);
               //prop.Comments.Add(new CodeCommentStatement("autogenerated"));
                prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower())));
                prop.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.Name.ToLower()), new CodePropertySetValueReferenceExpression()));
                targetClass.Members.Add(prop);

            }
        }

        private void addConstructor()
        {
            CodeConstructor cons = new CodeConstructor();
            cons.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            targetClass.Members.Add(cons);
           // cons.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression( new CodeThisReferenceExpression(), "Name"), new Code

            CodeMemberMethod addInitScripts = new CodeMemberMethod();
            addInitScripts.Name = "addInitialScripts";
            addInitScripts.Attributes = MemberAttributes.Override | MemberAttributes.Family;

            targetClass.Members.Add(addInitScripts);


        }


        public void addProperty(PropertyDescriptor pd)
        {
            pds.Add(pd);
        }


        public void generateSource(string fileName)
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            
            // our\name\space\class.cs
            // our\name\space\

            Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf('\\')));


            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, options);
            }

        }

        public Assembly generateSourceInMem()
        {
            this.setUpClass();
            this.addFields();
            this.addProperties();
            this.addConstructor();

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = true;
            options.GenerateExecutable = false;
            options.OutputAssembly = "test.dll";
           // options.LinkedResources.Add("Whiskey2D.Core");
            options.ReferencedAssemblies.Add("Whiskey2D.dll");
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Linq.dll");

            
            CompilerResults results = provider.CompileAssemblyFromDom(options, targetUnit);
            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }
            return results.CompiledAssembly;

        }



    }
}
