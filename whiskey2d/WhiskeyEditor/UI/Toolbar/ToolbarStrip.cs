using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Actions.Impl;
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
        private PlayAction playAction;
        private CompileAction compileAction;

        private ToolStripButton btnPlay;
        private ToolStripButton btnCompile;
        private ToolStripButton btnSave;

        public event ToolButtonEventHandler ButtonPressed = new ToolButtonEventHandler((s, a) => { });
       
        public ToolBarStrip()
        {
            
            BackColor = UIManager.Instance.PaleFlairColor;

            this.GripStyle = ToolStripGripStyle.Hidden;
            this.GripMargin = new Padding(0);
            this.Padding = new Padding(0);


            this.ImageList = new ImageList();
            this.ImageList.Images.Add(AssetManager.ICON_SAVE);
            this.ImageList.Images.Add(AssetManager.ICON_PLAY);

            initControls();
            configureControls();
            addControls();

        }

        private void initControls()
        {
           // btnPlay = new ToolStripButton("Play");
            //btnPlay.ImageIndex = 1;
           // btnPlay.Image = AssetManager.ICON_PLAY;

            playAction = new PlayAction();
            btnPlay = playAction.generateControl<ToolStripButton>();

            btnSave = new ToolStripButton("Save");
            btnSave.ImageIndex = 0;

            compileAction = new CompileAction();
            btnCompile = compileAction.generateControl<ToolStripButton>();

        }
        private void configureControls()
        {
           // btnPlay.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_PLAY)); };

            btnSave.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_SAVE)); };

//            btnCompile.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_COMPILE)); };

        }
        private void addControls()
        {
           // Items.Add(btnSave);

            Items.Add(btnCompile);

            btnPlay.Dock = DockStyle.Right;
            Items.Add(btnPlay);


           
        }


    }
}
