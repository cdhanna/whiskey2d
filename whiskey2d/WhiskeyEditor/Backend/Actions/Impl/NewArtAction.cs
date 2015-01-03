using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewArtAction : AbstractAction
    {

        public OpenFileDialog Dialog { get; private set; }

        public NewArtAction()
            : base("Add Art", AssetManager.ICON_FILE_PICTURE)
        {
            Dialog = new OpenFileDialog();
            Dialog.CheckFileExists = true;
            Dialog.DefaultExt = ".png";
            Dialog.Filter = "png files | *.png";
        }

        protected override void run()
        {

            DialogResult result = Dialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                ProjectManager.Instance.ActiveProject.addMedia(Dialog.FileName);
            }
            

        }
    }
}
