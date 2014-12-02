using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Documents.Actions;

namespace WhiskeyEditor.UI.Documents
{
    class DocumentTab : TabPage
    {

        public DocumentView ParentController { get; set; }
        public int TabNumber { get; set; }
        public string FileName { get; private set; }

        protected Panel ContentPanel { get; private set; }
        protected ToolStrip ToolStrip { get; private set; }

        private List<WhiskeyAction> Actions { get; set; }


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

            Actions = new List<WhiskeyAction>();

            ContentPanel = new Panel();
            ContentPanel.Dock = DockStyle.Fill;

            ToolStrip = new ToolStrip();
            ToolStrip.Dock = DockStyle.Top;
            ToolStrip.BackColor = UIManager.Instance.PaleFlairColor;
            ToolStrip.GripStyle = ToolStripGripStyle.Hidden;

            ContentPanel.Padding = new Padding(0, ToolStrip.Height, 0, 0);

            Controls.Add(ToolStrip);
            Controls.Add(ContentPanel);

            Dirty = false;
            SaveAction saveAction = new SaveAction(this);
            addAction(saveAction);
            
    


        }

        private bool dirty = false;
        protected bool Dirty
        {
            get
            {
                return dirty;
            }
            set
            {
                dirty = value;
                


                string txt = Text.Replace(" * ", "");
                txt += (Dirty ? " * " : "");
                Text = txt;

               
            }
        }
        

        protected void addAction(WhiskeyAction action)
        {
            Actions.Add(action);
            ToolStripButton btn = action.generateControl<ToolStripButton>();
            ToolStrip.Items.Add(btn);
        }

        public virtual void open()
        {

        }

        public virtual void save()
        {
            Dirty = false;
        }

        //TODO mark with a "Are you sure you want to close" if marked Dirty=True
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
