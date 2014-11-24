using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend.Managers
{
    class ProjectManager
    {
        private static ProjectManager instance = new ProjectManager();
        public static ProjectManager Instance
        {
            get { return instance; }
        }


        private Project active;

        private ProjectManager()
        {
            active = null;
        }


        public Project ActiveProject
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                Settings.CurrentProject = active.PathBase;
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
