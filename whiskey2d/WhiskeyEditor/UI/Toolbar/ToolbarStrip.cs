using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Toolbar
{
    public delegate void ToolButtonEventHandler (object sender, ToolButtonEventArgs args);
    public class ToolButtonEventArgs : EventArgs
    {
        public String ButtonName { get; private set; }
        public ToolButtonEventArgs(String buttonName)
        {
            ButtonName = buttonName;
        }
    }

    public class ToolBarStrip : ToolStrip
    {

        private ToolStripButton btnCompile;
        private ToolStripButton btnSave;

        public event ToolButtonEventHandler ButtonPressed = new ToolButtonEventHandler((s, a) => { });
       
        public ToolBarStrip()
        {
            
            BackColor = UIManager.Instance.FlairColor;

            this.GripStyle = ToolStripGripStyle.Hidden;
            this.GripMargin = new Padding(0);
            this.Padding = new Padding(0);
            

            initControls();
            configureControls();
            addControls();

        }

        private void initControls()
        {
            btnCompile = new ToolStripButton("Play");
            btnSave = new ToolStripButton("Save");
        }
        private void configureControls()
        {
            btnCompile.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_PLAY)); };

            btnSave.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_SAVE)); };

            

        }
        private void addControls()
        {
            Items.Add(btnSave);

            btnCompile.Dock = DockStyle.Right;
            Items.Add(btnCompile);
           
        }


    }
}
