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
    class NewSoundAction : AbstractAction
    {

        public OpenFileDialog Dialog { get; private set; }

        public NewSoundAction()
            : base("Add Sound", AssetManager.ICON_FILE)
        {
            Dialog = new OpenFileDialog();
            Dialog.CheckFileExists = true;
            Dialog.DefaultExt = ".wav";
            Dialog.Filter = "wav files | *.wav";
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
