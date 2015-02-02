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


        private ToolStripControlHost host;
        private LightSettingsControl control;


        public ToggleLightingAction(LevelDescriptor levelDescriptor)
            : base("Lighting", AssetManager.ICON_FILE_PICTURE)
        {
            Descriptor = levelDescriptor;
        }

        protected override void setupDropDown(ToolStripDropDownButton btn)
        {
            control = new LightSettingsControl();

            control.ShadowingEnabledBox.CheckedChanged += (s, a) =>
            {
                Descriptor.Level.ShadowingEnabled = control.ShadowingEnabledBox.Checked;
            };
            control.PreviewShadowingBox.CheckedChanged += (s, a) =>
            {
                Descriptor.Level.PreviewShadowing = control.PreviewShadowingBox.Checked;
            };
            control.LightingEnabledBox.CheckedChanged += (s, a) =>
            {
                Descriptor.Level.LightingEnabled = control.LightingEnabledBox.Checked;
            };
            control.PreviewLightingBox.CheckedChanged += (s, a) =>
            {
                Descriptor.Level.PreviewLighting = control.PreviewLightingBox.Checked;
            };

            ToolStripDropDown dropDown = btn.DropDown;

            //dropDown.AutoSize = false;
            dropDown.Margin = Padding.Empty;
            dropDown.Padding = Padding.Empty;
            host = new ToolStripControlHost(control);

            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = control.Size;
            dropDown.Size = control.Size;
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
            control.setFromLevel(Descriptor);
        }
    }
}
