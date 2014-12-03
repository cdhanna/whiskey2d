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

namespace WhiskeyEditor.UI.Documents
{
    class LevelDocument : DocumentTab
    {

        private WhiskeyControl whiskey;

        private DragEventHandler dragEnterHandler;
        private DragEventHandler dragDropHandler;

        private LevelDescriptor desc;

        public LevelDocument(LevelDescriptor lDesc, DocumentView parent)
            : base(lDesc.FilePath, parent)
        {
            Text = lDesc.Name;
            desc = lDesc;
            initControls();
            addControls();
            
            whiskey.AllowDrop = true;
            dragEnterHandler = new DragEventHandler(dragEnter);
            dragDropHandler = new DragEventHandler(dragDrop);
            

        }




        private void initControls()
        {
            whiskey = new WhiskeyControl();
            whiskey.Level = desc.Level;
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

                InstanceDescriptor inst = new InstanceDescriptor(tDesc);
                inst.Sprite.Scale *= 50;
                Point p = PointToClient(new Point(args.X, args.Y));
                inst.Position = new Vector(p.X, p.Y);
                inst.X = p.X;
                inst.Y = p.Y;
                desc.Level.Descriptors.Add(inst);
                Dirty = true;
            }
        }

        public override void Refresh()
        {
            whiskey.setAsActive();
            base.Refresh();
        }

        public override void save()
        {
            State state = desc.Level.getInstanceLevelState();
            State.serialize(state, desc.FilePath);
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
