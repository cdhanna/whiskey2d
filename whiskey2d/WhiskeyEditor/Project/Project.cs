using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyEditor.Project
{
    class Project
    {
        public const string PROP_NAME = "Name";
        public const string PROP_LAST_EDITING_SCENE = "LastEditingScene";


        private string path;
        private PropertiesFiles settings;

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
        /// The path of the .project file
        /// </summary>
        public string FileSettingsPath { get { return path + Path.DirectorySeparatorChar + ".whiskeyproj"; } }

        /// <summary>
        /// the settings file
        /// </summary>
        public PropertiesFiles Settings {get { return settings; } }

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


        public Project(string path, string name)
        {
            this.path = path;
            
            createDirs();
            createSettings();
            

            this.Name = name;
            this.EditingScene = "default";
    
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
        }

        public void createSettings()
        {
            settings = new PropertiesFiles(FileSettingsPath);
           
        }


    }
}
