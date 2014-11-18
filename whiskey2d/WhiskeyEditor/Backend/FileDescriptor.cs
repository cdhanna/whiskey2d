﻿using System;
using System.Collections.Generic;
using System.Linq;
using WhiskeyEditor.Backend.Managers;
using System.IO;
using WhiskeyEditor.Project;
using SmallMVC;

namespace WhiskeyEditor.Backend
{

    class FileDescriptor : Model
    {

        private string filePath;
        private string name;


        public FileDescriptor(string name)
            : this(ProjectManager.Instance.ActiveProject.PathSrc + Path.DirectorySeparatorChar + name + ".cs", name)
        {

        }

        public FileDescriptor(string filePath, string name)
        {
            this.filePath = filePath;
            this.name = name;
            
            CompileManager.Instance.addFileDescriptor(this);
        }

        public String FilePath
        {
            get
            {
                return this.filePath;
            }
        }
        public String Name
        {
            get
            {
                return this.name;
            }
        }
        public String QualifiedName
        {
            get
            {
                return CodeNameSpace + "." + Name;
            }
        }

        protected virtual String CodeNameSpace
        {
            get { return "Project"; }
        }

        protected virtual String CodeClassDef
        {
            get
            {
                return "[Serializable] " + Environment.NewLine + "\tpublic class " + Name;
            }
        }
        protected virtual String[] CodeUsingStatements
        {
            get
            {
                return new string[] { "System", "Whiskey2D.Core"};
            }
        }

        protected virtual void addSpecializedCode(StreamWriter writer)
        {
            //fill in with subclasses
        }

        protected virtual void processExistingCode(string[] allLines)
        {

        }

        public virtual void ensureFileExists()
        {

            if (File.Exists(filePath))
            {
                //do something?
                string[] allLines = File.ReadAllLines(filePath);

                foreach (String line in allLines)
                {
                    if (line.Contains(" class "))
                    {
                        int indexStart = line.IndexOf(" class ") + " class ".Length;
                        int indexEnd = line.IndexOf(" :");
                        string realName = line.Substring(indexStart, indexEnd - indexStart).Trim();
                        name = realName;
                        break;
                    }
                }

                processExistingCode(allLines);
            }
            else
            {

                //StreamWriter writer = File.CreateText(filePath);
                FileStream fileStream = File.Create(filePath);
                StreamWriter writer = new StreamWriter(fileStream);
                
                //writer.AutoFlush = true;
                foreach (string usingStatement in CodeUsingStatements)
                {
                    writer.WriteLine("using " + usingStatement + ";");
                }
                writer.WriteLine("");
                writer.WriteLine("//auto-generated by Whiskey2D");
                writer.WriteLine("namespace " + CodeNameSpace);
                writer.WriteLine("{");

                writer.WriteLine("\t" + CodeClassDef);
                writer.WriteLine("\t{");

                addSpecializedCode(writer);

                writer.WriteLine("\t}");

                writer.WriteLine("}");

                writer.Flush();
                writer.Close();
                fileStream.Close();
            }

        }

    }
}
