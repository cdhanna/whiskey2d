using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewScriptAction : NewFileAction
    {
        public NewScriptAction()
            : base("New Script", AssetManager.ICON_FILE)
        {

        }

        protected override void beforeShow(UI.Menu.NewForm form)
        {
            form.setForScript();
        }
    }
}
