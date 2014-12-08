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

            whiskey.SelectionChanged += (s, a) =>
            {
                requestPropertyChange(a.SelectedObject);
            };

            whiskey.MouseUp += (s, a) =>
            {
                if (whiskey.SelectedGob != null)
                {
                    requestPropertyChange(whiskey.SelectedGob);
                }
            };


            dragEnterHandler = new DragEventHandler(dragEnter);
            dragDropHandler = new DragEventHandler(dragDrop);

            base.addAction(new PlayLevelAction(levelDescriptor));
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
            args.Effect = DragDropEffects.All;
        }
        private void dragDrop(object sender, DragEventArgs args)
        {
            LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));


            FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
            if (fDesc is TypeDescriptor)
            {
                TypeDescriptor tDesc = (TypeDescriptor)fDesc;

                InstanceDescriptor inst = new InstanceDescriptor(tDesc, Descriptor.Level);
                inst.Sprite = new Sprite(WhiskeyControl.Renderer, inst.Sprite);


                //inst.Sprite.Scale *= 50;
                Point p = PointToClient(new Point(args.X, args.Y - ToolStrip.Height));
                
                inst.Position = new Vector(p.X, p.Y) - inst.Bounds.Size/2;

                inst.X = inst.Position.X;
                inst.Y = inst.Position.Y;
                
                //Descriptor.Level.Descriptors.Add(inst);
                Dirty = true;
            }

           // save(new DefaultProgressNotifier());

        }

        public override void Refresh()
        {
            whiskey.setAsActive();
            base.Refresh();
        }

        public override void save(ProgressNotifier pn)
        {
            pn.Progress = .3f;
            State state = Descriptor.Level.getInstanceLevelState();
            pn.Progress = .9f;
            
            State.serialize(state, Descriptor.FilePath);
            pn.Progress = 1;
            base.save();
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
