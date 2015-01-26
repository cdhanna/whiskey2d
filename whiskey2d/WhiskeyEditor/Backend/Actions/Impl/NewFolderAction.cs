using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewFolderAction: NewFileAction
    {
        public NewFolderAction()
            : base("Add Folder", WhiskeyEditor.UI.Assets.AssetManager.ICON_FLDR)
        {

        }

        protected override void beforeShow(UI.Menu.NewFileForm form)
        {
            form.setForFolder(Path);
        }
    }
}
