﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Assets;
namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewLevelAction : NewFileAction
    {
        public NewLevelAction()
            : base("New Level", AssetManager.ICON_FILE)
        {

        }

        protected override void beforeShow(UI.Menu.NewForm form)
        {
            form.setForLevel();
        }

    }
}