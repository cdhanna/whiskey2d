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
using System.Diagnostics;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    public class Project
    {
        public const string PROP_NAME = "Name";
        public const string PROP_LAST_EDITING_SCENE = "LastEditingScene";

        
        public const string PATH_COMPILE_EXE_CONFIG = ResourceFiles.LibExe + ".config";
        private string path;
        private PropertiesFiles settings;
        private PropertiesFiles gameSettings;

        /// <summary>
        /// The base path of the project
        /// </summary>
        public string PathBase { get { return path; } }

        /// <summary>
        /// the path to the src directory
        /// </summary>
        public string PathSrc { get { return PathBase + Path.DirectorySeparatorChar + "src"; } }

        /// <summary>
        /// the path the scripts directory
        /// </summary>
        public string PathSrcScripts { get { return PathSrc + Path.DirectorySeparatorChar + "scripts"; } }

        /// <summary>
        /// the path to the media directory
        /// </summary>
        public string PathMedia { get { return PathBase + Path.DirectorySeparatorChar + "media"; } }

        /// <summary>
        /// the path to the bin directory
        /// </summary>
        public string PathLib { get { return PathBase + Path.DirectorySeparatorChar + "bin"; } }

        /// <summary>
        /// the path to the states directory
        /// </summary>
        public string PathStates { get { return PathBase + Path.DirectorySeparatorChar + "states"; } }


        /// <summary>
        /// the path to the build directory. It should only be created after a build is called
        /// </summary>
        public string PathBuild { get { return PathBase + Path.DirectorySeparatorChar + "build"; } }

        public string PathBuildLib { get { return PathBuild + Path.DirectorySeparatorChar + "lib"; } }
        public string PathBuildMedia { get { return PathBuild + Path.DirectorySeparatorChar + "media"; } }
        public string PathBuildStates { get { return PathBuild + Path.DirectorySeparatorChar + "states"; } }
        public string FileBuildGamePropPath { get { return PathBuild + Path.DirectorySeparatorChar + ".gameprops"; } }
        public string FileBuildGameExePath { get { return PathBuild + Path.DirectorySeparatorChar + Name + ".exe"; } }
        public string FileBuildGameConfigPath { get { return FileBuildGameExePath + ".config"; } }
        
        public string FileGameDataLibrary { get { return PathLib + Path.DirectorySeparatorChar + "GameData.dll"; } }


        public string FileGameDataPath { get { return PathBase + Path.DirectorySeparatorChar + ".gamedata";  } }

        /// <summary>
        /// The path of the .project file
        /// </summary>
        public string FileSettingsPath { get { return PathBase + Path.DirectorySeparatorChar + ".whiskeyproj"; } }

        /// <summary>
        /// the settings file
        /// </summary>
        public PropertiesFiles Settings {get { return settings; } }

        /// <summary>
        /// the settings file that will be genererated when a game is built
        /// </summary>
        private PropertiesFiles GameSettings { get { return gameSettings; } }


        

        /// <summary>
        /// Get/Set the name of the project. 
        /// </summary>
        public string Name
        {
            get
            {
                return Settings.get(PROP_NAME);
            }
            set 
            {
                Settings.set(PROP_NAME, value);
                Settings.Save();
            }
        }

        /// <summary>
        /// Get/Set the scene that the project is editing
        /// </summary>
        public string EditingScene
        {
            get
            {
                return Settings.get(PROP_LAST_EDITING_SCENE);
            }
            set
            {
                Settings.set(PROP_LAST_EDITING_SCENE, value);
                Settings.Save();
            }
        }

        /// <summary>
        /// Get/Set the scene that will be launched when the game is started
        /// </summary>
        public string GameStartScene
        {
            get
            {
                return Settings.get(GameProperties.START_SCENE);
            }
            set
            {
                Settings.set(GameProperties.START_SCENE, value);
                Settings.Save();
            }
        }



        public Project(string path, string name)
        {
            this.path = path;
            
            createDirs();
            createSettings();
            

            this.Name = name;
            ensureDefaultProps();
        }

        public Project(string path)
        {
            this.path = path;
            createDirs();
            createSettings();
            ensureDefaultProps();


        }

        private void ensureDefaultProps()
        {
            if (this.Name == null)
            {
                this.Name = "Unnamed Project";
            }
            if (this.EditingScene == null)
            {
                this.EditingScene = "default";
            }
            if (this.GameStartScene == null)
            {
                this.GameStartScene = EditingScene;
            }
        }

        private void createDirs()
        {
            Directory.CreateDirectory(PathBase);
            Directory.CreateDirectory(PathSrc);
            Directory.CreateDirectory(PathMedia);
            Directory.CreateDirectory(PathLib);
            Directory.CreateDirectory(PathStates);
            Directory.CreateDirectory(PathSrcScripts);
        }

        private void createSettings()
        {
            settings = new PropertiesFiles(FileSettingsPath);
           
        }

        private void createGameSettings()
        {
            gameSettings = new PropertiesFiles(FileBuildGamePropPath);
            
            gameSettings.set(GameProperties.START_SCENE, GameStartScene + ".state");

            gameSettings.Save();

        }

        ///// <summary>
        ///// Save a state to the /states folder
        ///// </summary>
        ///// <param name="state">A valid and named State</param>
        //public void saveState(State state)
        //{
        //    string stateName = state.Name + ".state";
        //    string filePath = PathStates + Path.DirectorySeparatorChar + stateName;
        //    State.serialize(state, filePath);

        //    GameStartScene = stateName; // hardcoded. Fix.

        //}



        public void saveGameData()
        {
            GameData data = FileManager.Instance.getGameData();
            GameData.serialize(data, FileGameDataPath);
        }

        public void loadGameData()
        {
            try
            {
                GameData data = GameData.deserialize(FileGameDataPath);
                FileManager.Instance.setGameData(data);
            }
            catch (WhiskeyException e)
            {
                GameData data = new GameData();
                FileManager.Instance.setGameData(data);
                saveGameData();
            }
        }

        /// <summary>
        /// Delete and recreate the build directory structure
        /// </summary>
        public void cleanProject()
        {
            if (Directory.Exists(PathBuild))
            {
                Directory.Delete(PathBuild, true);
            }


            DirectoryInfo buildInfo = Directory.CreateDirectory(PathBuild);

            Directory.CreateDirectory(PathBuildLib);
            Directory.CreateDirectory(PathBuildMedia);
            Directory.CreateDirectory(PathBuildStates);
        }

        public void testLevel(LevelDescriptor level)
        {
            testLevel(level, new DefaultProgressNotifier());
        }
        public void testLevel(LevelDescriptor level, ProgressNotifier pn)
        {
            string origStartScene = GameStartScene;
            pn.Progress = .2f;

            cleanProject();
            pn.Progress = .3f;

            CompileManager.Instance.compile();
            DirectoryCopy(PathLib, PathBuildLib, true);
            DirectoryCopy(PathMedia, PathBuildMedia, true);
            
            pn.Progress = .6f;

            string statePath = InstanceManager.Instance.convertToGobs(FileGameDataLibrary, level.Level);
            GameStartScene = level.Name;

            pn.Progress = .7f;

            DirectoryCopy(ResourceFiles.CompileLib, PathBuildLib, true);
            DirectoryCopy(ResourceFiles.CompileMedia, PathBuildMedia, true);
            File.Copy(ResourceFiles.LibExe, FileBuildGameExePath);
            File.Copy(PATH_COMPILE_EXE_CONFIG, FileBuildGameConfigPath);
            createGameSettings();

            pn.Progress = .8f;
            runGame();
            pn.Progress = 1;

            GameStartScene = origStartScene;
        }

        /// <summary>
        /// Clean the project
        /// Then build the project into the build directory
        /// </summary>
        public void buildExecutable()
        {
            cleanProject();
            
            DirectoryCopy(PathLib, PathBuildLib, true);
            DirectoryCopy(PathMedia, PathBuildMedia, true);
            
            //build states 
            
            
            //InstanceManager.Instance.convertToGobs(dll, "default");

            foreach (Level level in InstanceManager.Instance.Levels)
            {
                string statePath = InstanceManager.Instance.convertToGobs(FileGameDataLibrary, level);
                GameStartScene = level.LevelName;
            }

            string[] states = Directory.GetFiles(PathStates);
            foreach (string stateName in states)
            {
                State s = State.deserialize(stateName);
              //  InstanceManager.Instance.setState(s);
              //  InstanceManager.Instance.convertToGobs(dll, "default");
            }
           // DirectoryCopy(PathStates, PathBuildStates, true);
            
            
            
            DirectoryCopy(ResourceFiles.CompileLib, PathBuildLib, true);
            DirectoryCopy(ResourceFiles.CompileMedia, PathBuildMedia, true);
            File.Copy(ResourceFiles.LibExe, FileBuildGameExePath);
            File.Copy(PATH_COMPILE_EXE_CONFIG, FileBuildGameConfigPath);
            createGameSettings();

        }

        public void runGame()
        {
            var startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = PathBuild;
            // set additional properties 
            startInfo.FileName = Name + ".exe";
            Process proc = Process.Start(startInfo);
        }


        /// <summary>
        /// Taken from http://msdn.microsoft.com/en-us/library/bb762914(v=vs.110).aspx
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
