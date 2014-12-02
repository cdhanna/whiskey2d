using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WhiskeyEditor.UI;

namespace WhiskeyEditor.Backend.Managers
{

    #region Event Setup
    public delegate void FileChangedEventHandler(object sender, FileEventArgs args);
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

        public event FileChangedEventHandler FileAdded = new FileChangedEventHandler( (s, a) => {});
        public event FileChangedEventHandler FileRemoved = new FileChangedEventHandler( (x, y) => {});
        public event FileChangedEventHandler FileChanged = new FileChangedEventHandler( (s, a) => {});

        private void fireFileAdded(FileEventArgs args)
        {
            FileAdded(this, args);
        }

        private void fireFileRemoved(FileEventArgs args)
        {
            FileRemoved(this, args);
        }

        private void fireFileChanged(FileEventArgs args)
        {
            FileChanged(this, args);
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
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += new FileSystemEventHandler(onFileChanged);
            fileWatcher.EnableRaisingEvents = true;
        }



        //private void onFileCreated(object sender, FileSystemEventArgs args)
        //{
        //    //decide the file is already being tracked
        //    //if so, do nothing
        //    //if not, create the appropriate fileDescriptor. TypeDesc or ScriptDesc or nothing?
            
        //    //FileDescriptor fDesc = new FileDescriptor(args.FullPath, args.Name);
        //}

        //private void onFileDeleted(object sender, FileSystemEventArgs args)
        //{
        //    //decide if the file is still be tracked
        //    //if so, delete it
        //    //if not, do nothing
        //}

        private void onFileChanged(object sender, FileSystemEventArgs args)
        {
            //validate that this is a file desc
            fireFileChanged( new FileEventArgs(lookUp(args.FullPath))); //may be broken
        }


        public void addFileDescriptor(FileDescriptor fileDesc)
        {
            fileDescs.Add(fileDesc);
            fileDescMap.Add(UIManager.Instance.normalizePath( fileDesc.FilePath ), fileDesc);
            
            
            fileDesc.ensureFileExists();
            ProjectManager.Instance.ActiveProject.saveGameData();



            fireFileAdded(new FileEventArgs(fileDesc));
          //  ProjectManager.Instance.ActiveProject.loadGameData();
        }

        public void removeFileDescriptor(FileDescriptor fileDesc)
        {
            if (File.Exists(fileDesc.FilePath))
            {
                fileDesc.delete();
            }
            else
            {
                fileDescMap.Remove(UIManager.Instance.normalizePath( fileDesc.FilePath) );
                fileDescs.Remove(fileDesc);
                fireFileRemoved(new FileEventArgs(fileDesc));
            }
        }


        /// <summary>
        /// Gets a file descriptor out of the file manager. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public FileDescriptor lookUp(string filePath)
        {

          //  name = Path.GetFullPath(name);
            //ProjectManager.Instance.ActiveProject.loadGameData();
            filePath = UIManager.Instance.normalizePath(filePath);

            if (fileDescMap.ContainsKey(filePath))
            {
                return fileDescMap[filePath];
            }
            else throw new WhiskeyException("File could not be found : " + filePath);
        }


        public TypeDescriptor createNewTypeDescriptor(string name)
        {
            TypeDescriptor tDesc = new TypeDescriptor(name);
            tDesc.ensureFileExists();
            return tDesc;
        }

        public ScriptDescriptor createNewScriptDescriptor(string name, string typeName)
        {
            ScriptDescriptor sDesc = new ScriptDescriptor(name, typeName);
            sDesc.ensureFileExists();
            return sDesc;
        }

        public LevelDescriptor createNewLevelDescriptor(string name)
        {
            LevelDescriptor lDesc = new LevelDescriptor(name);
            lDesc.ensureFileExists();
            return lDesc;
        }

        public virtual GameData getGameData()
        {
            GameData data = new GameData();
            FileDescriptor[] files = new FileDescriptor[fileDescs.Count];
            fileDescs.CopyTo(files);
            data.Files = files.ToList();
            return data;
        }

        public virtual void setGameData(GameData data)
        {

            if (data == null)
            {
                return;
            }

            //TODO notify someone that everything is gone
            fileDescs.Clear();
            fileDescMap.Clear();

            FileDescriptor[] files = new FileDescriptor[data.Files.Count];
            data.Files.CopyTo(files);
            // = objs.ToList();

            foreach (FileDescriptor f in files)
            {
                addFileDescriptor(f);
            }

        }

    }
}
