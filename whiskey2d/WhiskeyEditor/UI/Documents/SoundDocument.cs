using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Media;

namespace WhiskeyEditor.UI.Documents
{
    class SoundDocument : DocumentTab
    {
        public SoundDescriptor Descriptor { get; private set; }
        public SoundControl Control { get; private set; }
        public SoundPlayer Player { get; private set; }

        public SoundDocument(SoundDescriptor sound, DocumentView dView)
            : base(sound.FilePath, dView)
        {
            Text = sound.Name;
            Descriptor = sound;
            Player = new SoundPlayer(Descriptor.FilePath);

            initControls();
            addControls();
        }


        private void initControls()
        {
            Control = new SoundControl();
            
            Control.PlayEvent += (s, a) =>
            {
                
                Player.Play();
                
            };

            Control.StopEvent += (s, a) =>
            {
                Player.Stop();
            };


        }

        private void addControls()
        {
            Control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(Control);
        }

    }
}
