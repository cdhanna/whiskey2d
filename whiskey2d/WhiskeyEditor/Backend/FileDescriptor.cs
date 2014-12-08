using System;
using System.Collections.Generic;
using System.Linq;
using WhiskeyEditor.Backend.Managers;
using System.IO;
using SmallMVC;
using WhiskeyEditor.UI;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class FileDescriptor
    {

        private string filePath;
        private string name;

        public FileDescriptor(string filePath, string name)
        {
            this.filePath = UIManager.Instance.normalizePath(filePath);

            this.name = name;
            
           
        }

        public String FilePath
        {
            get
            {
                return this.filePath;
            }
            protected set
            {
                this.filePath = UIManager.Instance.normalizePath(value);
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
        
        public void delete()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                FileManager.Instance.removeFileDescriptor(this);
            }
        }

        public string[] readAllLines()
        {
            if (File.Exists(filePath))
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

        public virtual void ensureFileExists()
        {
            if (File.Exists(filePath))
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
