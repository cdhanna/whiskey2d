using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class PlayGameAction : AbstractAction
    {
        public PlayGameAction()
            : base("Play", AssetManager.ICON_PLAY)
        {

        }
        protected override void run()
        {
            ProjectManager.Instance.ActiveProject.testGame(this);
        }

    }
}
