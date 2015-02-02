using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Actions
{
    public partial class LightSettingsControl : UserControl
    {


        public CheckBox PreviewLightingBox { get { return prevLightingBox; } }
        public CheckBox PreviewShadowingBox { get { return prevShadowBox; } }
        public CheckBox LightingEnabledBox { get { return enableLightingBox; } }
        public CheckBox ShadowingEnabledBox { get { return enableShadowBox; } }

        public void setFromLevel(LevelDescriptor descriptor)
        {
            Invoke(new NoArgFunction(() =>
            {
                prevLightingBox.Checked = descriptor.Level.PreviewLighting;
                prevShadowBox.Checked = descriptor.Level.PreviewShadowing;
                enableLightingBox.Checked = descriptor.Level.LightingEnabled;
                enableShadowBox.Checked = descriptor.Level.ShadowingEnabled;
            }));
        }

        public LightSettingsControl()
        {
            InitializeComponent();
        }
    }
}
