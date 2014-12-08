using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend.Managers
{
    public delegate void ProjectChangedEventHandler (object sender, ProjectChangedEventArgs args);
    public class ProjectChangedEventArgs : EventArgs
    {
        public Project OldProject { get; private set; }
        public Project NewProject { get; private set; }
        public ProjectChangedEventArgs(Project oldProject, Project newProject)
        {
            OldProject = oldProject;
            NewProject = newProject;
        }
    }


    public class NoProject : Project
    {
        public NoProject()
            : base("noproject")
        {

        }
    }

    public class ProjectManager
    {
        private static ProjectManager instance = new ProjectManager();
        public static ProjectManager Instance
        {
            get { return instance; }
        }


        public event ProjectChangedEventHandler ProjectChanged = new ProjectChangedEventHandler((s, a) => { });

        private Project active;

        

        private ProjectManager()
        {
            
            active = new NoProject();
        }


        public Project ActiveProject
        {
            get
            {
                return active;
            }
            set
            {
               // InstanceManager.Instance.clear();
                Project old = active;
                active = value;
                FileManager.Instance.refreshFileWatch();
                Settings.CurrentProject = active.PathBase;
                active.loadGameData();
                ProjectChanged(this, new ProjectChangedEventArgs(old, active));
            }
        }

        public Project createNewProject(string projectPath, string projectName)
        {
            Project p = new Project(projectPath, projectName);
            return p;
        }

        public Project openProject(string projectPath)
        {
            Project p = new Project(projectPath);
            return p;
        }


    }
}
