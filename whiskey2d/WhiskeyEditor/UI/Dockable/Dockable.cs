using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WhiskeyEditor.UI.Dockable
{
    public delegate void DockControlDockChangedEventHandler(object sender, DockControlDockChangedEventArgs args);
    public class DockControlDockChangedEventArgs : EventArgs
    {
        public DockControl DockControl { get; private set; }
        public DockControlDockChangedEventArgs(DockControl dockControl)
        {
            DockControl = dockControl;
        }
    }

    public class DockControl : Control
    {
        private Control parent;
        private DockStyle defaultDockStyle;
        private bool viewable;
        private Control primaryView;
        private TableLayoutPanel tablePanel;
        private Panel menuPanel;
        private Button closeBtn;
        private Label label;

        public DockControl(Control parent, DockStyle defaultDockStyle)
        {
            this.parent = parent;
            this.defaultDockStyle = defaultDockStyle;
            Size = new Size(100, 100);
            BackColor = Color.Red;

            initControls();
            addControls();
            configureControls();
        }

        #region events
        public event DockControlDockChangedEventHandler Undocked = new DockControlDockChangedEventHandler( (s, a) => {});
        public event DockControlDockChangedEventHandler Docked = new DockControlDockChangedEventHandler( (s, a) => {});
        public event DockControlDockChangedEventHandler DockedChanged = new DockControlDockChangedEventHandler( (s, a) => {});

        private void fireDockChangedEvt(DockControlDockChangedEventArgs args)
        {
            if (DockedChanged != null)
                DockedChanged(this, args);
        }
        private void fireDockedEvt(DockControlDockChangedEventArgs args)
        {
            if (Docked != null)
            {
                Docked(this, args);
                fireDockChangedEvt(args);
            }
        }
        private void fireUndockedEvt(DockControlDockChangedEventArgs args)
        {
            if (Undocked != null)
            {
                Undocked(this, args);
                fireDockChangedEvt(args);
            }
        }
        #endregion

        public bool Viewable
        {
            get { return this.viewable; }
            set { this.viewable = value; }
        }

        public Control PrimaryView
        {
            get
            {
                return primaryView;
            }
            set
            {
                if (primaryView != null)
                {
                    tablePanel.Controls.Remove(primaryView);
                }

                primaryView = value;
                primaryView.Dock = DockStyle.Fill;
                primaryView.Padding = new Padding(0);
                primaryView.Margin = new Padding(0);
                label.Text = primaryView.Name;
                tablePanel.Controls.Add(primaryView, 0, 1);
            }
        }

        public void dock()
        {
            dock(defaultDockStyle);
        }
        public void dock(DockStyle dockStyle)
        {
            if (!viewable)
            {

                Dock = dockStyle;
                parent.Controls.Add(this);
                viewable = true;
                fireDockedEvt(new DockControlDockChangedEventArgs(this));
            }
        }

        public void undock()
        {
            if (viewable)
            {
                parent.Controls.Remove(this);
                viewable = false;
                fireUndockedEvt(new DockControlDockChangedEventArgs(this));
            }
        }

        private void configureControls()
        {
            closeBtn.Click += (s, args) => {
                undock();
            };
        }

        private void initControls()
        {
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            

            menuPanel = new Panel();
            menuPanel.BackColor = UIManager.Instance.FlairColor ;
            menuPanel.Dock = DockStyle.Fill;
            menuPanel.Size = new Size(40, 20);
            menuPanel.Padding = new Padding(0);
            menuPanel.Margin = new Padding(1);
            menuPanel.BorderStyle = BorderStyle.None;

            closeBtn = new Button();
            closeBtn.Text = "X";
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Size = new Size(16, 16);
            closeBtn.Dock = DockStyle.Right;
            menuPanel.Controls.Add(closeBtn);

            label = new Label();
            label.Text = "SomeText";
            label.Location = new Point(2, 2);
            menuPanel.Controls.Add(label);

            tablePanel = new TableLayoutPanel();
            tablePanel.Size = new Size(50, 50);
            tablePanel.BackColor = UIManager.Instance.DullFlairColor;
            tablePanel.RowCount = 2;
            tablePanel.ColumnCount = 1;
            tablePanel.Padding = new Padding(0);
            tablePanel.Margin = new Padding(0);
            tablePanel.Controls.Add(menuPanel, 0, 0);
        }

        private void addControls()
        {
            tablePanel.Dock = DockStyle.Fill;
            
            Controls.Add(tablePanel);
        }

    }
}
