using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Drawing;

namespace WhiskeyEditor.UI.Properties.Editors
{
    abstract class EditScriptsAction : AbstractAction
    {
        Form form;

        private AddRemoveScriptControl control;
      
        public EditScriptsAction()
            : base("Add/Remove Scripts", AssetManager.ICON_COMPILE)
        {
           
            control = new AddRemoveScriptControl();
           // control.Size = new System.Drawing.Size(400, 400);
            control.Invalidate();
        }

        public ToolStripDropDownButton generateControl()
        {

            
          //  control.AutoSize = true;
            ToolStripDropDownButton dropDown = base.generateControl<ToolStripDropDownButton>();
            ToolStripControlHost panel = new ToolStripControlHost(control);
            //panel.Size = control.Size;
          
           // panel.AutoSize = false;
           // dropDown.DropDown.AutoSize = true;

            panel.BackColor = Color.WhiteSmoke;
            dropDown.DropDown.BackColor = UIManager.Instance.PaleFlairColor;

            


            panel.Padding = Padding.Empty;
            panel.Margin = Padding.Empty;
            panel.Dock = DockStyle.Fill;
            dropDown.DropDown.Items.Add(panel);
            dropDown.DropDownDirection = ToolStripDropDownDirection.BelowLeft;

          
            dropDown.DropDown.Size = control.Size;


            dropDown.DropDown.Padding = Padding.Empty;
            dropDown.DropDown.Margin = Padding.Empty;

            ((ToolStripDropDownMenu)dropDown.DropDown).ShowImageMargin = false;
            ((ToolStripDropDownMenu)dropDown.DropDown).ShowItemToolTips = false;

            //dropDown.DropDown.ShowImag


            control.ClosedControl += (s, accepted) =>
            {
                
                dropDown.DropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
                //updateScripts(accepted);
            };

            //dropDown.DropDown.LostFocus += (s, a) =>
            //{
            //    updateScripts(true);
            //};



            dropDown.DropDownClosed += (s, a) =>
            {
                //accepted = true;
                updateScripts(control.Accepted, control.getSelectedScripts());
            };



            return dropDown;
        }


        protected abstract void updateScripts(bool accepted, List<string> newScriptNames);

        protected abstract List<string> getCurrentScriptNames();
        protected abstract string getTypeDescriptorName();

        protected override void run()
        {

            control.refreshSelectedScripts(getCurrentScriptNames());
            control.refreshAllScripts(getTypeDescriptorName());


        }
    }
}
