﻿using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;
using System.IO;
using WhiskeyEditor.Project;


namespace WhiskeyEditor.Backend
{
    class TypeDescriptor : FileDescriptor 
    {

        public const string CODE_PROP_START = "\t\t#region AUTO-GENERATED BY WHISKEY2D. DO NOT EDIT. (please)";
        public const string CODE_PROP_END = "\t\t#endregion END OF AUTO-GENERATED CODE.";

        private List<PropertyDescriptor> propDescs;
        private List<String> scriptNames;

        public TypeDescriptor(string name)
            : base(name)
        {
            scriptNames = new List<String>();
            propDescs = new List<PropertyDescriptor>();

            addInitialProps();
            

        }

        

        public String ClassName
        {
            get { return Name; }
        }

        private void addInitialProps()
        {
            addPropertyDescriptor(new PropertyDescriptor(true, "X", new RealType(typeof(float), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "Y", new RealType(typeof(float), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "ID", new RealType(typeof(int), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "Sprite", new RealType(typeof(Sprite), new Sprite())));
        }


        public void addPropertyDescriptor(PropertyDescriptor propDesc)
        {
            if (0 == propDescs.Where((p) => p.Name.Equals(propDesc.Name)).ToList().Count)
            {
                propDescs.Add(propDesc);
            }
            else throw new WhiskeyException("Property Already Exists : " + propDesc.Name);
        }
        public void removePropertyDescriptor(PropertyDescriptor propDesc)
        {
            if (propDescs.Contains(propDesc))
            {
                if (!propDesc.Secure)
                {
                    propDescs.Remove(propDesc);
                }
                else throw new WhiskeyException("Property Is Secure : " + propDesc.Name);
            }
            else throw new WhiskeyException("Property Not Found : " + propDesc.Name);
        }


        public void addScript(String scriptName)
        {           
            scriptNames.Add(scriptName);
        }
        public void removeScript(String scriptName)
        {
            if (scriptNames.Contains(scriptName))
            {
                scriptNames.Remove(scriptName);
            }
            else throw new WhiskeyException("Script Not Found : " + scriptName);
        }



        public List<PropertyDescriptor> getPropertySetClone()
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor propDesc in propDescs)
            {
                props.Add(propDesc.clone());
            }
            return props;
        }

        public List<String> getScriptNamesClone()
        {

            List<String> names = new List<string>();
            foreach (String name in scriptNames)
            {
                names.Add(name);
            }
            return names;
        }


        protected override string CodeClassDef
        {
            get
            {
                return base.CodeClassDef + " : GameObject";
            }
        }


        private string getCodeFor(object val)
        {
            if (val == null) return "null";

            string typeName = val.GetType().Name;
            if (typeName.Equals(typeof(int).Name)
                    || typeName.Equals(typeof(string).Name)
                    || typeName.Equals(typeof(bool).Name)
                    || typeName.Equals(typeof(float).Name)
                    || typeName.Equals(typeof(double).Name)
                    || typeName.Equals(typeof(Single).Name)
                    || typeName.Equals(typeof(byte).Name)
                    || typeName.Equals(typeof(Int16).Name)
                    || typeName.Equals(typeof(Int32).Name)
                )
            {
                return stringify(val);
            }
            else if (typeName.Equals(typeof(Vector).Name))
            {
                Vector vec = (Vector) val;
                return " new Vector(" + vec.X + ", " + vec.Y + ")";
            }
            else if (typeName.Equals(typeof(Sprite).Name))
            {
                Sprite spr = (Sprite)val;
                return " new Sprite(" + getCodeFor(spr.ImagePath) + ", " + getCodeFor(spr.Scale) + ", " + getCodeFor(spr.Offset) + ", " + getCodeFor(spr.Depth) + ", " + getCodeFor(spr.Color) + ", " + getCodeFor(spr.Rotation) + ")";

            }
            else if (typeName.Equals(typeof(Color).Name))
            {
                Color col = (Color)val;
                return " new Color(" + col.R + ", " + col.G + ", " + col.B + ", " + col.A + ")";

            } 

            else
            {
                //assume that the val is an instance desc
                return " null";

              //  throw new WhiskeyException("Property is not of supported type: " + val.GetType().Name);
            }

        }
        private string stringify(object val)
        {
            if (val == null)
            {
                return "null";
            }
            else if (val.GetType() == typeof(String))
            {
                return "\"" + val + "\"";
            }
            else if (val.GetType() == typeof(float))
            {

                return "" + val + "f";
            }
            else
            {
                return "" + val;
            }

        }

        private void writeProperties(StreamWriter writer)
        {
            writer.WriteLine(CODE_PROP_START);

            foreach (PropertyDescriptor prop in propDescs)
            {
                if (!prop.Secure)
                {
                    writer.WriteLine("\t\t" + prop.toCodeDefinition());
                }
            }

            writer.WriteLine("");
            writer.WriteLine("\t\t#region INIT_VALUE_ASSIGNMENT");

            writer.WriteLine("\t\tprotected override void initProperties()");
            writer.WriteLine("\t\t{");

            foreach (PropertyDescriptor prop in propDescs)
            {

                writer.WriteLine("\t\t\t" + prop.Name + " =" + getCodeFor(prop.TypeVal.value) + ";");
            }

            writer.WriteLine("\t\t}");

            writer.WriteLine("\t\t#endregion");
            writer.WriteLine("");

            writer.WriteLine(CODE_PROP_END);
        }

        protected override void addSpecializedCode(StreamWriter writer)
        {

            writeProperties(writer);
            writer.WriteLine("");

            writer.WriteLine("\t\tprotected override void addInitialScripts(){ }");

        }

        protected override void processExistingCode(string[] allLines)
        {

            File.Delete(FilePath);
            FileStream stream = File.Create(FilePath);
            StreamWriter writer = new StreamWriter(stream);
            
            for (int i = 0 ; i < allLines.Length ; i ++)
            {

                if (allLines[i].Equals(CODE_PROP_START))
                {

                    string nextLine = allLines[i++];
                    while (!nextLine.Equals(CODE_PROP_END))
                    {
                        nextLine = allLines[i++];
                    }
                    writeProperties(writer);
                    //i++;
                }

                writer.WriteLine(allLines[i]);
            }
            writer.Flush();
            writer.Close();
            stream.Close();

            base.processExistingCode(allLines);
        }

    }
}
