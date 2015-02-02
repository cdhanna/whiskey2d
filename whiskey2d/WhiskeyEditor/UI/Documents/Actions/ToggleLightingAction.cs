using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using Whiskey2D.Core.Managers;
using System.Windows.Forms;
using System.Drawing;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class ToggleLightingAction : AbstractAction
    {
        public LevelDescriptor Descriptor { get; private set; }


        private Panel mainPanel;
        private ToolStripControlHost host;
        private CheckBox chkBoxLightMap;

        public ToggleLightingAction(LevelDescriptor levelDescriptor)
            : base("Lighting", AssetManager.ICON_FILE_PICTURE)
        {
            Descriptor = levelDescriptor;
        }

        protected override void setupDropDown(ToolStripDropDownButton btn)
        {
            mainPanel = new Panel();
            chkBoxLightMap = new CheckBox();
            chkBoxLightMap.Text = "PreviewLighting";
            chkBoxLightMap.CheckAlign = ContentAlignment.MiddleRight;
            chkBoxLightMap.Checked = Descriptor.Level.PreviewLighting; 
            
            chkBoxLightMap.CheckedChanged += (s, a) =>
            {
                Descriptor.Level.PreviewLighting = chkBoxLightMap.Checked;
            };
            mainPanel.AutoSize = true;
            mainPanel.Controls.Add(chkBoxLightMap);
            


            ToolStripDropDown dropDown = btn.DropDown;

            //dropDown.AutoSize = false;
            dropDown.Margin = Padding.Empty;
            dropDown.Padding = Padding.Empty;
            host = new ToolStripControlHost(mainPanel);

            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = mainPanel.Size;
            dropDown.Size = mainPanel.Size;
            //((ToolStripDropDownMenu)btn.DropDown).ShowImageMargin = false;
            //((ToolStripDropDownMenu)btn.DropDown).ShowItemToolTips = false;
            dropDown.Items.Add(host);

           

            //host.Margin = Padding.Empty;
            //host.Padding = Padding.Empty;
            //btn.DropDown.Margin = Padding.Empty;
            //btn.DropDown.Padding = Padding.Empty;
            //host.Width = grid.Width*2;
            //host.Height = grid.Height * 2;
            //btn.DropDown.Width = host.Width;
            //btn.DropDown.Items.Add(host);

           

            base.setupDropDown(btn);
        }

        protected override void run()
        {
            
        }
    }
}
