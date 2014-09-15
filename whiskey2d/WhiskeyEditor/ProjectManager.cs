using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WhiskeyEditor
{
    class ProjectManager
    {
        public const string fileExt = ".w2d";


        public ProjectManager()
        {

        }



        public Project createNewProject(string directory, string name)
        {
            directory = directory.Remove(directory.LastIndexOf(fileExt));

            Project project = new Project(directory, name);

            Directory.CreateDirectory(directory);
            File.Create(directory + "\\" + name);

            Directory.CreateDirectory(directory + "\\Src");
            Directory.CreateDirectory(directory + "\\Content");


            return project;

        }

        public Project loadProject(string path)
        {

            string directory = path;
            string name = path.Substring(path.LastIndexOf('\\') + 1);
            directory = directory.Substring(0, directory.LastIndexOf(name));

            Project project = new Project(directory, name);
            return project;
        }


        public void setTreeFor(Project project, TreeView tree)
        {
            tree.Nodes.Clear();

            TreeNode root = new TreeNode(project.Name);

            string[] subDirs = Directory.GetDirectories(project.Directory);
            foreach (string dir in subDirs){

                

                TreeNode dirNode = new TreeNode(dir);
                root.Nodes.Add(dirNode);

            }

            tree.Nodes.Add(root);
        }

    }
}
