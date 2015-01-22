using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.UI.Documents
{
    public partial class SoundControl : UserControl
    {

        public EventHandler PlayEvent = new EventHandler((s, a) => { });
        public EventHandler StopEvent = new EventHandler((s, a) => { });
        public SoundControl()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayEvent(this, e);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopEvent(this, e);
        }
    }
}
