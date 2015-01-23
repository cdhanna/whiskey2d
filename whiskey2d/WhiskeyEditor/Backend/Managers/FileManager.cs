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
            fileWatcherHandler = new FileSystemEventHandler(onFileChanged);
            
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

        private FileSystemEventHandler fileWatcherHandler;

        public void refreshFileWatch()
        {

            if (fileWatcher != null)
            {
                fileWatcher.Changed -= fileWatcherHandler;
                fileWatcher = null;
            }

            fileWatcher = new FileSystemWatcher(ProjectManager.Instance.ActiveProject.PathSrc);
            fileWatcher.IncludeSubdirectories = true;
            fileWatcher.Filter = "*.cs";
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += fileWatcherHandler;
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

            string filePathKey = UIManager.Instance.normalizePath(fileDesc.FilePath);

            if (!fileDescMap.ContainsKey(filePathKey))
            {
                //add
                fileDescMap.Add(filePathKey, fileDesc);
                fileDescs.Add(fileDesc);
                fireFileAdded(new FileEventArgs(fileDesc));
            }
            else
            {
                //replace
                fileDescs.Remove(fileDescMap[filePathKey]);
                fileDescs.Add(fileDesc);
                fileDescMap[filePathKey] = fileDesc;
            }
            

           

            fileDesc.ensureFileExists();
            ProjectManager.Instance.ActiveProject.saveGameData();



            


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

            ProjectManager.Instance.ActiveProject.saveGameData();
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
            else return null;
            //else throw new WhiskeyException("File could not be found : " + filePath);
        }

        public T lookUpFileByPath<T>(string path) where T : FileDescriptor
        {
            List<T> conv = new List<T>();
            fileDescs.Where(f => (f is T && f.FilePath.Equals(path))).ToList().ForEach(f => conv.Add((T)f));
            if (conv.Count == 1)
                return conv[0];
            else throw new WhiskeyException("No file exists with the given path : " + path);
        }
        public T lookUpFileByName<T>(string name) where T : FileDescriptor
        {
            List<T> conv = new List<T>();

            fileDescs.Where(f => (f is T && f.Name.Equals(name))).ToList().ForEach((f) => { conv.Add((T) f); });
            if (conv.Count == 1)
            {
                return conv[0];
            }
            else throw new WhiskeyException("Too many");
           
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

            foreach (FileDescriptor file in files)
            {
                if (data.Files.Find(f => f.FilePath.Equals(file.FilePath)) == null)
                    data.Files.Add(file);
            }
           // data.Files = files.ToList();



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
            ScriptManager.Instance.clear();
            data.Files.CopyTo(files);
            // = objs.ToList();

            ////add core types
            //foreach (CoreTypeDescriptor c in WhiskeyEditor.compile_types.CoreTypes.typeFileNameMap.Values)
            //{
            //    addFileDescriptor(c);
            //}
            ////add core scripts
            //foreach (CoreScriptDescriptor c in WhiskeyEditor.compile_types.CoreTypes.scriptFileNameMap.Values)
            //{
            //    addFileDescriptor(c);
            //}

            List<LevelDescriptor> levels = new List<LevelDescriptor>();
            foreach (FileDescriptor f in files)
            {
                //if (f is CoreScriptDescriptor || f is CoreTypeDescriptor)
                //    continue; //we don't care about core descriptors
                
                addFileDescriptor(f);

                if (f is LevelDescriptor)
                {
                    LevelDescriptor l = (LevelDescriptor)f;
                    l.Level.updateAll();
                    levels.Add(l);
                    InstanceManager.Instance.addLevel(l.Level);
                }
                else if (f is ScriptDescriptor)
                {
                    ScriptDescriptor s = (ScriptDescriptor)f;
                    ScriptManager.Instance.addScript(s);

                }


            }
            levels.ForEach((l) =>
            {
                l.Level.updateAll();
                l.Level.syncAllTypesToInstances();
            });
            

        }

    }
}
