using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents
{
    class ArtDocument : DocumentTab
    {
        public ArtDescriptor Descriptor { get; private set; }
        public PictureBox PictureBox { get; private set; }

        public ArtDocument(ArtDescriptor art, DocumentView dView)
            : base(art.FilePath, dView)
        {
            Text = art.Name;
            Descriptor = art;

            initControls();
            addControls();
        }

        private void initControls()
        {
            PictureBox = new PictureBox();

            PictureBox.ImageLocation = Descriptor.FilePath;

        }
        private void addControls()
        {
            PictureBox.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(PictureBox);
        }

    }
}
