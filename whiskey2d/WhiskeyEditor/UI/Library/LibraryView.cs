using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Actions.Impl;
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


        private NewLevelAction newLevelAction;
        private NewTypeAction newTypeAction;
        private NewScriptAction newScriptAction;

        private ContextMenuStrip newFileMenu;



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

                    //MEDIA
                    //LibraryTreeNode nodeArt = new LibraryTreeNode("Media", p.PathMedia);
                    //nodeArt.populate();
                    //root.Nodes.Add(nodeArt);


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

            fileTree.ImageList = AssetManager.getImageList();
            //fileTree.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            //fileTree.ImageList.ImageSize = new Size(16, 16);
            //fileTree.ImageList = new ImageList();
            //fileTree.ImageList.Images.Add(AssetManager.ICON_FLDR);
            //fileTree.ImageList.Images.Add(AssetManager.ICON_FILE);


            newLevelAction = new NewLevelAction();
            newTypeAction = new NewTypeAction();
            newScriptAction = new NewScriptAction();

            newFileMenu = new ContextMenuStrip();
            newFileMenu.Width = 200;

            ToolStripLabel label = new ToolStripLabel("Add File...");
            newFileMenu.Text = "Add File";
            newFileMenu.Items.Add(label);
            newFileMenu.Items.Add( newTypeAction.generateControl<ToolStripButton>() );
            newFileMenu.Items.Add( newScriptAction.generateControl<ToolStripButton>());
            newFileMenu.Items.Add( newLevelAction.generateControl<ToolStripButton>() );
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
                if (node.IsFile && a.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ClickedOnNode(this, new LibrarySelectionEventArgs(node));
                }

                if (!node.IsFile && a.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    newFileMenu.Show(this, a.Location, ToolStripDropDownDirection.Right);
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
