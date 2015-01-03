using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.UI.Menu;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewTypeAction : NewFileAction
    {
        public NewTypeAction()
            : base("New Type", AssetManager.ICON_FILE_TYPE)
        {

        }
        protected override void beforeShow(NewForm form)
        {
            form.setForType();
        }

       

    }
}
