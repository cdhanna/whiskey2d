using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Assets;


namespace WhiskeyEditor.UI.Library
{

    public delegate void LibrarySelectionEventHandler (object sender, LibrarySelectionEventArgs args);
    public class LibrarySelectionEventArgs : EventArgs
    {
        public LibraryTreeNode Selected { get; private set; }
        public LibrarySelectionEventArgs(LibraryTreeNode node)
        {
            Selected = node;
        }
    }

    public class LibraryView : Control
    {

        private TreeView fileTree;

        public event LibrarySelectionEventHandler SelectionChanged = new LibrarySelectionEventHandler((s, a) => { });
        public event LibrarySelectionEventHandler ClickedOnNode = new LibrarySelectionEventHandler((s, a) => { });
        

        public LibraryView()
        {

            this.Size = new Size(100, 50);
            this.BackColor = Color.Gray;

            ProjectManager.Instance.ProjectChanged += (s, a) =>
            {
                refreshContent();
            };

            FileManager.Instance.FileAdded += (s, a) =>
            {
                refreshContent();
            };
            

            initControls();
            configureControls();
            addControls();

            refreshContent();

           
            fileTree.ItemDrag += (s, a) =>
            {
                fileTree.DoDragDrop(a.Item, DragDropEffects.All);
                
            };

            

        }

        public void refreshContent()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new NoArgFunction(() =>
                {
                    fileTree.Nodes.Clear();

                    Project p = ProjectManager.Instance.ActiveProject;

                    LibraryTreeNode root = new LibraryTreeNode(p.Name, p.PathBase);

                    //SOURCE
                    LibraryTreeNode nodeSrc = new LibraryTreeNode("Source", p.PathSrc);
                    nodeSrc.populate();
                    root.Nodes.Add(nodeSrc);

                    //LEVELS
                    LibraryTreeNode nodeLvl = new LibraryTreeNode("Levels", p.PathStates);
                    nodeLvl.populate();
                    root.Nodes.Add(nodeLvl);

                    fileTree.Nodes.Add(root);
                    root.ExpandAll();
                    //fileTree.Update();
                }));
            }
            else
            {
                this.HandleCreated += (s, a) => { refreshContent(); };
            }
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
                    SelectionChanged(this, new LibrarySelectionEventArgs(node));
                }

            };

            fileTree.NodeMouseClick += (s, a) =>
            {
                LibraryTreeNode node = (LibraryTreeNode)a.Node;
                if (node.IsFile)
                {
                    ClickedOnNode(this, new LibrarySelectionEventArgs(node));
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
