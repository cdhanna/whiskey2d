using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace WhiskeyEditor.UI.Documents
{
    class DocumentTab : TabPage
    {

        public DocumentView ParentController { get; set; }
        public int TabNumber { get; set; }
        public string FileName { get; private set; }


        public DocumentTab(string fileName, DocumentView parent)
            : base(fileName)
        {
            this.ImageIndex = 0;
            FileName = fileName;
            ParentController = parent;
            TabNumber = parent.Tabs.TabCount;
            
            ParentController.Tabs.TabPages.Add(this);

            ParentController.Tabs.MouseMove += mouseMoveHandle;
            ParentController.Tabs.MouseLeave += mouseMoveHandle;
            //ParentController.Tabs.MouseClick += mouseClickHandle;


        }

        public virtual void open()
        {

        }

        public virtual void save()
        {

        }

        public virtual void close()
        {
            ParentController.Tabs.MouseMove -= mouseMoveHandle;
            ParentController.Tabs.MouseLeave -= mouseMoveHandle;
            //ParentController.Tabs.MouseClick -= mouseClickHandle;
            ParentController.closeDocument(FileName);
        }

        private void ensureImageIndex(int i)
        {
            if (ImageIndex != i)
                ImageIndex = i;

        }

        public bool mouseInIcon()
        {

            if (TabNumber >= ParentController.Tabs.TabCount)
            {
                return false;
            }

            Point pos = ParentController.PointToClient(Cursor.Position);
            Rectangle rect = ParentController.Tabs.GetTabRect(TabNumber);
            if (pos.X > rect.Left + 5
                && pos.X < rect.Right - rect.Width + 25
                && pos.Y < rect.Bottom
                && pos.Y > rect.Top)
            {
                return true;
            }
            else return false;

        }

        public void mouseMoveHandle(object sender, EventArgs args)
        {
            if (mouseInIcon())
                ensureImageIndex(1);
            else ensureImageIndex(0);
        }

        public void mouseClickHandle(object sender, MouseEventArgs args)
        {
            if (mouseInIcon())
            {
                close();
            }
        }

    }
}
