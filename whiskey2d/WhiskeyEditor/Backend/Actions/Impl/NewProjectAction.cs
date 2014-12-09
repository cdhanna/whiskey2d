using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewProjectAction : NewFileAction
    {

        public NewProjectAction()
            : base("New Project", AssetManager.ICON_FILE)
        {

        }

    }
}
