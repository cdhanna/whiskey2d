using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WhiskeyEditor.Project;

namespace WhiskeyEditor.Backend.Managers
{

    #region Event Setup
    public delegate void FileChangedEventHandler(object sender, FileEventArgs args);
    public delegate void FileAddedEventHandler(object sender, FileEventArgs args);
    public delegate void FileRemovedEventHandler(object sender, FileEventArgs args);
    public class FileEventArgs : EventArgs
    {
        private FileDescriptor fDesc;
        public FileDescriptor FileDescriptor { get { return fDesc; } }
        public FileEventArgs(FileDescriptor fDesc)
        {
            this.fDesc = fDesc;
        }
    }
    #endregion

    class FileManager
    {

        private static FileManager instance = new FileManager();
        public static FileManager Instance { get { return instance; } }
        private FileManager()
        {
            fileDescs = new List<FileDescriptor>();
            fileDescMap = new Dictionary<string, FileDescriptor>();
            createFileWatcher();
        }

        #region events

        public event FileAddedEventHandler FileAdded;
        public event FileRemovedEventHandler FileRemoved;
        public event FileChangedEventHandler FileChanged;

        private void fireFileAdded(FileEventArgs args)
        {
            if (FileAdded != null)
            {
                FileAdded(this, args);
            }
        }

        private void fireFileRemoved(FileEventArgs args)
        {
            if (FileRemoved != null)
            {
                FileRemoved(this, args);
            }
        }

        private void fireFileChanged(FileEventArgs args)
        {
            if (FileChanged != null)
            {
                FileChanged(this, args);
            }
        }

        #endregion

        private List<FileDescriptor> fileDescs;
        private Dictionary<String, FileDescriptor> fileDescMap;
        private FileSystemWatcher fileWatcher;

        public List<FileDescriptor> FileDescriptors { get { return fileDescs; } }

        private void createFileWatcher()
        {
            fileWatcher = new FileSystemWatcher(ProjectManager.Instance.ActiveProject.PathSrc);
            fileWatcher.Filter = "*.cs";
            //fileWatcher.NotifyFilter =  NotifyFilters.LastWrite
            //                            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += new FileSystemEventHandler(onFileChanged);
            fileWatcher.Created += new FileSystemEventHandler(onFileCreated);
            fileWatcher.Deleted += new FileSystemEventHandler(onFileDeleted);
            fileWatcher.EnableRaisingEvents = true;
        }



        private void onFileCreated(object sender, FileSystemEventArgs args)
        {
            //decide the file is already being tracked
            //if so, do nothing
            //if not, create the appropriate fileDescriptor. TypeDesc or ScriptDesc or nothing?

            //FileDescriptor fDesc = new FileDescriptor(args.FullPath, args.Name);
        }

        private void onFileDeleted(object sender, FileSystemEventArgs args)
        {
            //decide if the file is still be tracked
            //if so, delete it
            //if not, do nothing
        }

        private void onFileChanged(object sender, FileSystemEventArgs args)
        {
            //validate that this is a file desc
            
            fireFileChanged( new FileEventArgs(lookUp(args.Name))); //may be broken
        }


        public void addFileDescriptor(FileDescriptor fileDesc)
        {
            fileDescs.Add(fileDesc);
            fileDescMap.Add(fileDesc.Name, fileDesc);
            fireFileAdded(new FileEventArgs(fileDesc));
        }
        public void removeFileDescriptor(FileDescriptor fileDesc)
        {
            if (File.Exists(fileDesc.FilePath))
            {
                fileDesc.delete();
            }
            else
            {
                fileDescMap.Remove(fileDesc.Name);
                fileDescs.Remove(fileDesc);
                fireFileRemoved(new FileEventArgs(fileDesc));
            }
        }


        public FileDescriptor lookUp(string name)
        {
            if (fileDescMap.ContainsKey(name))
            {
                return fileDescMap[name];
            }
            else return null;
        }


    }
}
