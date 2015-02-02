using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.MonoHelp;
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Documents.Actions;
using WhiskeyEditor.compile_types;
using WhiskeyEditor.compile_types.Types;

namespace WhiskeyEditor.UI.Documents
{

    /// <summary>
    /// provides a document for levels
    /// </summary>
    class LevelDocument : DocumentTab
    {

        /// <summary>
        /// A control that exposes graphics and the whiskey game engine
        /// </summary>
        private WhiskeyControl whiskey;

        /// <summary>
        /// A handler for drag drop
        /// </summary>
        private DragEventHandler dragEnterHandler;
        private DragEventHandler dragDropHandler;

        /// <summary>
        /// The level descriptor
        /// </summary>
        private LevelDescriptor Descriptor { get; set; }

        public LevelDocument(LevelDescriptor levelDescriptor, DocumentView parent)
            : base(levelDescriptor.FilePath, parent)
        {
            Text = levelDescriptor.Name;
            Descriptor = levelDescriptor;
            
            initControls();
            addControls();
            
            whiskey.AllowDrop = true;

            //whiskey.BecameDirty += (s, a) =>
            //{
            //    Dirty = true;
            //};







            whiskey.MouseUp += (s, a) =>
            {
                if (SelectionManager.Instance.SelectedInstance != null)
                {
                    requestPropertyChange(SelectionManager.Instance.SelectedInstance);
                }
            };

            SelectionManager.Instance.SelectedInstanceUpdated += (s, a) =>
            {
                if (SelectionManager.Instance.SelectedInstance != null)
                {
                   // requestPropertyChange(SelectionManager.Instance.SelectedInstance); //TODO fix how slow this is
                }
                else
                {
                    if (ParentController.Tabs.SelectedTab == this)
                    {
                        requestPropertyChange(Descriptor);
                    }
                }
            }; 

            dragEnterHandler = new DragEventHandler(dragEnter);
            dragDropHandler = new DragEventHandler(dragDrop);

            base.addAction(new PlayLevelAction(Descriptor));
            base.addAction<ToolStripDropDownButton>(new ViewInstanceDetailsAction(Descriptor.Level));
            base.addAction<ToolStripDropDownButton>(new ViewLevelLayersAction(Descriptor));
            base.addAction(new CopyInstanceAction());
            base.addAction(new PasteInstanceAction(Descriptor));
           // base.addAction<ToolStripDropDownButton>(new ToggleLightingAction(Descriptor));
            base.addAction<ToolStripDropDownButton>(new ToggleLightingAction(Descriptor));
            base.addAction(new IncreaseGridSnapAction());
            base.addAction(new DecreaseGridSnapAction());
        }




        private void initControls()
        {
            whiskey = new WhiskeyControl();
            whiskey.Load += (s, a) =>
            {
                whiskey.Level = Descriptor.Level;
            };
        }

        private void addControls()
        {
            whiskey.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(whiskey);
        }


        private void dragEnter(object sender, DragEventArgs args){
            if (args.Data.GetData(typeof(LibraryTreeNode)) is LibraryTreeNode)
            {
                LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));
                FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
                if (fDesc != null && (fDesc is TypeDescriptor || fDesc.GetType().IsSubclassOf(typeof(TypeDescriptor))))
                {
                    args.Effect = DragDropEffects.All;
                }
                if (fDesc != null && (fDesc is ArtDescriptor))
                {
                    args.Effect = DragDropEffects.All;
                }
            }
        }
        private void dragDrop(object sender, DragEventArgs args)
        {
            LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));


            FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
            TypeDescriptor tDesc = null;
            if (fDesc is TypeDescriptor)
            {
                tDesc = (TypeDescriptor)fDesc;
            }
            else if (fDesc is ArtDescriptor)
            {
                tDesc = CoreTypes.getType<SimpleObject>();
            }



            if (tDesc != null)
            {
                InstanceDescriptor inst = new InstanceDescriptor( Descriptor.Level);
                inst.Sprite = new Sprite(WhiskeyControl.Renderer, WhiskeyControl.Resources, inst.Sprite);
                inst.Light.Visible = false;
                inst.initialize(tDesc);

                //inst.Sprite.Scale *= 50;
                Point p = PointToClient(new Point(args.X, args.Y - ToolStrip.Height));

                inst.Position = new Vector(p.X, p.Y);// -inst.Bounds.Size / 2;
                inst.Position = WhiskeyControl.ActiveCamera.getGameCoordinate(inst.Position);

                inst.X = inst.Position.X;
                inst.Y = inst.Position.Y;

                SelectionManager.Instance.SelectedInstance = inst;

                Dirty = true;

                if (fDesc is ArtDescriptor)
                {
                    ArtDescriptor aDesc = (ArtDescriptor) fDesc;
                    inst.Sprite.ImagePath = aDesc.Name;
                    inst.Sprite.Scale = Vector.One;
                }

                Descriptor.Level.updateAll();

            }

            

        }

        public override void Refresh()
        {
            
            whiskey.setAsActive();
            
            base.Refresh();
        }

        public override void save(ProgressNotifier pn)
        {
            pn.Progress = .3f;
            //State state = Descriptor.Level.getInstanceLevelState();
            Descriptor.save();
            pn.Progress = .9f;
            
            //State.serialize(state, Descriptor.FilePath);
            pn.Progress = 1;
            base.save(pn);
        }

        public override void open()
        {
            whiskey.DragDrop += dragDropHandler;
            whiskey.DragEnter += dragEnterHandler;
            base.open();
        }
        public override void close()
        {
            whiskey.DragDrop -= dragDropHandler;
            whiskey.DragEnter -= dragEnterHandler;
            whiskey.Dispose();
            base.close();
        }

    }
}
