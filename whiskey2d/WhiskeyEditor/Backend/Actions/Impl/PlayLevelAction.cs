using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class PlayLevelAction : AbstractAction
    {
        public PlayLevelAction()
            : base("Play", AssetManager.ICON_PLAY)
        {

        }

        protected override void run()
        {
            string dll = CompileManager.Instance.compile();
            //// UIManager.Instance.GobInstances.convertToGobs(dll, "default");
            ProjectManager.Instance.ActiveProject.buildExecutable();
            ProjectManager.Instance.ActiveProject.runGame();
        }
    }
}
