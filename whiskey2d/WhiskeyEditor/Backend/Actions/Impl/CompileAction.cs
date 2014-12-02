using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class CompileAction : AbstractAction
    {
        public CompileAction()
            : base("Compile", AssetManager.ICON_COMPILE)
        {
            
        }

        protected override void run()
        {
            CompileManager.Instance.compile();
        }
    }
}
