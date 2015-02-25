using System;
using System.Collections.Generic;
using System.Linq;
using WhiskeyEditor.Backend.Managers;
using System.IO;
using WhiskeyEditor.UI;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class FileDescriptor : PathDescriptor
    {

        private string filePath;
        private string name;

        public FileDescriptor(string filePath, string name)
        {

            FilePath = filePath;
           // this.filePath = UIManager.Instance.normalizePath(filePath);

           // this.filePath = this.filePath.Replace(ProjectManager.Instance.ActiveProject.PathBase, "");

            this.name = name;
            
           
        }

        public String FilePath
        {
            get
            {
                String result =  UIManager.Instance.normalizePath(ProjectManager.Instance.ActiveProject.PathBase + Path.DirectorySeparatorChar + this.filePath);
                return result;
            }
            protected set
            {
                
                this.filePath = value.Replace(ProjectManager.Instance.ActiveProject.PathBase, "");
                this.filePath = UIManager.Instance.normalizePath(filePath);
                if (filePath.StartsWith("\\"))
                    filePath = filePath.Substring(1);
                int x;
            }
        }


        public String Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                this.name = value;
            }
        }
        
        public virtual void delete()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                FileManager.Instance.removeFileDescriptor(this);
            }
        }

        public string[] readAllLines()
        {
            if (File.Exists(FilePath))
            {
                return File.ReadAllLines(FilePath);
            }
            else return new string[] { };
        }

        public virtual void createFile()
        {
            //do nothing
        }
        public virtual void inspectFile()
        {
            //do nothing
        }
        public virtual void save()
        {
            //do nothing
        }

        public virtual void ensureFileExists()
        {
            if (File.Exists(FilePath))
            {
                inspectFile();
            }
            else
            {
                createFile();
            }
            ProjectManager.Instance.ActiveProject.saveGameData();
        }

    }
}
