using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Project;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Assets;


namespace WhiskeyEditor.UI.Library
{



    class LibraryView : Control
    {

        private TreeView fileTree;

        public LibraryView()
        {

            this.Size = new Size(100, 50);
            this.BackColor = Color.Gray;

            

            initControls();
            configureControls();
            addControls();

            refreshContent();

        }

        public void refreshContent()
        {
            fileTree.Nodes.Clear();

            Project.Project p = ProjectManager.Instance.ActiveProject;

            TreeNode root = new TreeNode(p.Name);

            LibraryTreeNode nodeSrc = new LibraryTreeNode("Source", p.PathSrc);
            nodeSrc.populate();
            root.Nodes.Add(nodeSrc);

            fileTree.Nodes.Add(root);
            root.ExpandAll();
            //fileTree.Update();

        }

       

        private void initControls()
        {
            fileTree = new TreeView();
            fileTree.Size = new Size(50, 50);

            fileTree.ImageList = new ImageList();
            fileTree.ImageList.Images.Add(AssetManager.ICON_FLDR);
            fileTree.ImageList.Images.Add(AssetManager.ICON_FILE);

        }

        private void configureControls()
        {
            fileTree.NodeMouseDoubleClick += (s, a) =>
            {
                LibraryTreeNode node = (LibraryTreeNode)a.Node;
                if (node.IsFile)
                {
                    Console.WriteLine(node.FilePath);
                }

            };
        }

        private void addControls()
        {

            fileTree.Dock = DockStyle.Fill;
            Controls.Add(fileTree);

        }

    }
}
