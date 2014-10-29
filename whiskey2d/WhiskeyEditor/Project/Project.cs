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


namespace WhiskeyEditor.Project
{
    class Project
    {
        public const string PROP_NAME = "Name";
        public const string PROP_LAST_EDITING_SCENE = "LastEditingScene";
        public const string PATH_COMPILE_LIB = "compile-lib";
        public const string PATH_COMPILE_MEDIA = "compile-media";
        public const string PATH_COMPILE_EXE = "compile-exe\\WhiskeyRunner.exe";
        public const string PATH_COMPILE_EXE_CONFIG = PATH_COMPILE_EXE + ".config";
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
        /// the path to the media directory
        /// </summary>
        public string PathMedia { get { return PathBase + Path.DirectorySeparatorChar + "media"; } }

        /// <summary>
        /// the path to the bin directory
        /// </summary>
        public string PathBin { get { return PathBase + Path.DirectorySeparatorChar + "bin"; } }

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



        /// <summary>
        /// The path of the .project file
        /// </summary>
        public string FileSettingsPath { get { return PathBase + Path.DirectorySeparatorChar + ".whiskeyproj"; } }

        /// <summary>
        /// the settings file
        /// </summary>
        public PropertiesFiles Settings {get { return settings; } }


        public PropertiesFiles GameSettings { get { return gameSettings; } }

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
            this.EditingScene = "default";
            this.GameStartScene = EditingScene;
        }

        public Project(string path)
        {
            this.path = path;
            createDirs();
            createSettings();



        }


        public void createDirs()
        {
            Directory.CreateDirectory(PathBase);
            Directory.CreateDirectory(PathSrc);
            Directory.CreateDirectory(PathMedia);
            Directory.CreateDirectory(PathBin);
            Directory.CreateDirectory(PathStates);
        }

        public void createSettings()
        {
            settings = new PropertiesFiles(FileSettingsPath);
           
        }

        public void createGameSettings()
        {
            gameSettings = new PropertiesFiles(FileBuildGamePropPath);
            
            gameSettings.set(GameProperties.START_SCENE, GameStartScene);

            gameSettings.Save();


        }

        public void saveState(State state)
        {
            string stateName = state.Name + ".state";
            string filePath = PathStates + Path.DirectorySeparatorChar + stateName;
            State.serialize(state, filePath);

            GameStartScene = stateName; // hardcoded. Fix.

        }

        public void cleanProject()
        {
            if (Directory.Exists(PathBuild))
            {
                Directory.Delete(PathBuild, true);
            }


            Directory.CreateDirectory(PathBuild);

            Directory.CreateDirectory(PathBuildLib);
            Directory.CreateDirectory(PathBuildMedia);
            Directory.CreateDirectory(PathBuildStates);
        }

        public void buildExecutable()
        {
            cleanProject();
            createGameSettings();

            DirectoryCopy(PathBin, PathBuildLib, true);
            DirectoryCopy(PathMedia, PathBuildMedia, true);
            DirectoryCopy(PathStates, PathBuildStates, true);
            DirectoryCopy(PATH_COMPILE_LIB, PathBuildLib, true);
            DirectoryCopy(PATH_COMPILE_MEDIA, PathBuildMedia, true);
            File.Copy(PATH_COMPILE_EXE, FileBuildGameExePath);
            File.Copy(PATH_COMPILE_EXE_CONFIG, FileBuildGameConfigPath);
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
