using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class PlayLevelAction : AbstractAction
    {

        public LevelDescriptor LevelDesc { get; private set; }

        public PlayLevelAction(LevelDescriptor lDesc)
            : base("Run Level", AssetManager.ICON_PLAY)
        {
            LevelDesc = lDesc;
        }

        protected override void run()
        {
            ProjectManager.Instance.ActiveProject.testLevel(LevelDesc, this);
        }
    }
}
