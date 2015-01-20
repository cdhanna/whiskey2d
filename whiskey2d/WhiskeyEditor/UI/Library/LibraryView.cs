using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

using WhiskeyEditor.UI.Documents;
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

        public TreeView FileTree { get; private set; }

        public event LibrarySelectionEventHandler SelectionChanged = new LibrarySelectionEventHandler((s, a) => { });
        public event LibrarySelectionEventHandler ClickedOnNode = new LibrarySelectionEventHandler((s, a) => { });
        public event PropertyChangeRequestEventHandler FileDeleted = new PropertyChangeRequestEventHandler((s, a) => { });


        private NewLevelAction newLevelAction;
        private NewTypeAction newTypeAction;
        private NewScriptAction newScriptAction;
        private NewArtAction newArtAction;
        private NewSoundAction newSoundAction;

        private DeleteFileAction deleteAction;

        private ContextMenuStrip folderNodeMenu;
        private ContextMenuStrip fileNodeMenu;


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

           
            FileTree.ItemDrag += (s, a) =>
            {
                FileTree.DoDragDrop(a.Item, DragDropEffects.All);
                
            };

            FileTree.KeyDown += (s, a) =>
            {
               
                if (!a.Handled && a.KeyData == Keys.Delete) //delete key
                {
                    deleteSelectedNode();
                }
            };

        }

        public void deleteSelectedNode()
        {
            Invoke(new NoArgFunction(() =>
            {
                TreeNode node = FileTree.SelectedNode;
                if (node != null && node is LibraryTreeNode)
                {
                    LibraryTreeNode libNode = (LibraryTreeNode)node;

                    DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this?",
                        "Delete Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);


                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            FileDescriptor fDesc = FileManager.Instance.lookUpFileByPath<FileDescriptor>(libNode.FilePath);
                            fireDeleteFileEvent(new PropertyChangeRequestEventArgs(fDesc));
                            FileManager.Instance.removeFileDescriptor(fDesc);
                        }
                        catch (WhiskeyException e)
                        {

                        }
                        FileTree.Nodes.Remove(libNode);
                    }


                }
            }));
        }

        public void fireDeleteFileEvent(PropertyChangeRequestEventArgs args)
        {
            FileDeleted(this, args);

        }

        public void refreshContent()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new NoArgFunction(() =>
                {
                    FileTree.Nodes.Clear();

                    Project p = ProjectManager.Instance.ActiveProject;

                    LibraryTreeNode root = new LibraryTreeNode(p.Name, p.PathBase);

                    //PROPERTIES
                    LibraryTreeNode nodeProp = new LibraryTreeNode("Properties", p.FileSettingsPath, true);
                    nodeProp.ImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FILE);
                    nodeProp.SelectedImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FILE); 
                    root.Nodes.Add(nodeProp);

                    //SOURCE-Objects
                    LibraryTreeNode nodeObjects = new LibraryTreeNode("Objects", p.PathSrcObjects);
                    nodeObjects.populate();
                    root.Nodes.Add(nodeObjects);

                    //SOURCE-Scripts
                    LibraryTreeNode nodeScripts = new LibraryTreeNode("Scripts", p.PathSrcScripts);
                    nodeScripts.populate();
                    root.Nodes.Add(nodeScripts);

                    //LEVELS
                    LibraryTreeNode nodeLvl = new LibraryTreeNode("Levels", p.PathStates);
                    nodeLvl.populate();
                    root.Nodes.Add(nodeLvl);

                    //MEDIA
                    LibraryTreeNode nodeArt = new LibraryTreeNode("Media", p.PathMedia);
                    nodeArt.populate();
                    root.Nodes.Add(nodeArt);


                    FileTree.Nodes.Add(root);
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
            FileTree = new TreeView();
            FileTree.Size = new Size(50, 50);

            FileTree.ImageList = AssetManager.getImageList();
            //fileTree.ImageList.ImageSize = new Size(32, 32);
            //fileTree.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            //fileTree.ImageList.ImageSize = new Size(16, 16);
            //fileTree.ImageList = new ImageList();
            //fileTree.ImageList.Images.Add(AssetManager.ICON_FLDR);
            //fileTree.ImageList.Images.Add(AssetManager.ICON_FILE);


            newLevelAction = new NewLevelAction();
            newTypeAction = new NewTypeAction();
            newScriptAction = new NewScriptAction();
            newArtAction = new NewArtAction();
            newSoundAction = new NewSoundAction();

            folderNodeMenu = new ContextMenuStrip();
            folderNodeMenu.Width = 200;

            ToolStripLabel label = new ToolStripLabel("Add File...");
            folderNodeMenu.Text = "Add File";
           // folderNodeMenu.Items.Add(label);
            folderNodeMenu.Items.Add( newTypeAction.generateControl<ToolStripButton>() );
            folderNodeMenu.Items.Add( newScriptAction.generateControl<ToolStripButton>());
            folderNodeMenu.Items.Add( newLevelAction.generateControl<ToolStripButton>() );
            folderNodeMenu.Items.Add( newArtAction.generateControl<ToolStripButton>());
            folderNodeMenu.Items.Add( newSoundAction.generateControl<ToolStripButton>());


            deleteAction = new DeleteFileAction(this);

            fileNodeMenu = new ContextMenuStrip();
            
            ToolStripLabel fileNodeLabel = new ToolStripLabel("Options...");
            fileNodeMenu.Text = "Options...";
            //fileNodeMenu.Items.Add(fileNodeLabel);
            fileNodeMenu.Items.Add(deleteAction.generateControl<ToolStripButton>());

        }

        private void configureControls()
        {
            FileTree.NodeMouseDoubleClick += (s, a) =>
            {
                LibraryTreeNode node = (LibraryTreeNode)a.Node;
                if (node.IsFile)
                {
                    SelectionChanged(this, new LibrarySelectionEventArgs(node));
                }

            };

            FileTree.NodeMouseClick += (s, a) =>
            {
                LibraryTreeNode node = (LibraryTreeNode)a.Node;
                if (node.IsFile && a.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ClickedOnNode(this, new LibrarySelectionEventArgs(node));
                }

                if (a.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    FileTree.SelectedNode = a.Node;
                    if (node.IsFile)
                    {
                        fileNodeMenu.Width = 300;
                        fileNodeMenu.Show(this, a.Location, ToolStripDropDownDirection.Right);
                    }
                    else
                    {
                        folderNodeMenu.Show(this, a.Location, ToolStripDropDownDirection.Right);
                    }
                }

            };
            
          

        }

        private void addControls()
        {

            FileTree.Dock = DockStyle.Fill;
            Controls.Add(FileTree);

        }

    }
}
