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
        private CompileAction compileAction;
        private PlayGameAction playAction;

 

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

       
           

            compileAction = new CompileAction();
            playAction = new PlayGameAction();


        }
        private void configureControls()
        {
           // btnPlay.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_PLAY)); };

            //btnSave.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_SAVE)); };

//            btnCompile.Click += (s, a) => { ButtonPressed(this, new ToolButtonEventArgs(UIManager.COMMAND_COMPILE)); };

        }
        private void addControls()
        {
           // Items.Add(btnSave);

            Items.Add(compileAction.generateControl<ToolStripButton>());
            Items.Add(playAction.generateControl<ToolStripButton>());
          


           
        }


    }
}
