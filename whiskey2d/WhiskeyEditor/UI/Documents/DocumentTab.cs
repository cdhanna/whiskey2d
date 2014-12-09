using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Documents.Actions;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents
{

    public delegate void PropertyChangeRequestEventHandler(object sender, PropertyChangeRequestEventArgs args);
    public class PropertyChangeRequestEventArgs : EventArgs
    {
        public Object PropertyObject { get; private set; }
        public PropertyChangeRequestEventArgs(Object obj)
        {
            PropertyObject = obj;
        }
    }

    class DocumentTab : TabPage
    {
        /// <summary>
        /// The Document collection that this tab belongs to
        /// </summary>
        public DocumentView ParentController { get; set; }

        /// <summary>
        /// The id of the tab, and the index of the tab in the parent controller
        /// </summary>
        public int TabNumber { get; set; }
        
        /// <summary>
        /// The filename of the file currently being used in the document
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The panel that holds all of user controls of the tab
        /// </summary>
        protected Panel ContentPanel { get; private set; }
        
        /// <summary>
        /// A toolstrip displays actions of the tab
        /// </summary>
        protected ToolStrip ToolStrip { get; private set; }

        /// <summary>
        /// A set of actions for the tab
        /// </summary>
        private List<WhiskeyAction> Actions { get; set; }

        /// <summary>
        /// This event being fired means that the tab has requested a certain object to have its properties displayed in the propertyView
        /// </summary>
        public event PropertyChangeRequestEventHandler PropertyChangeRequested = new PropertyChangeRequestEventHandler((s, a) => { });
        
        
        /// <summary>
        /// Fire a PropertyChangeRequest event.
        /// Caling this means to ask for the given object to have its properties displayed
        /// </summary>
        /// <param name="propertyObject"></param>
        protected void requestPropertyChange(Object propertyObject)
        {
            PropertyChangeRequested(this, new PropertyChangeRequestEventArgs(propertyObject));
        }

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
            ToolStrip.Margin = new Padding(0);
            ContentPanel.Padding = new Padding(0, ToolStrip.Height, 0, 0);
            ContentPanel.Margin = new Padding(0);
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


                Invoke(new NoArgFunction(() =>
                {
                    string txt = Text.Replace(" * ", "");
                    txt += (Dirty ? " * " : "");
                    Text = txt;
                }));
               
            }
        }

        

        public override void Refresh()
        {
           // ContentPanel.Refresh();
            base.Refresh();
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

        public virtual void save(ProgressNotifier pn)
        {
            Dirty = false;

        }
        //public void save()
        //{
        //   // Dirty = false;
        //    this.save(new DefaultProgressNotifier());
        //}

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
