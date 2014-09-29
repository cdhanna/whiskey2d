using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.TestImpl
{
    class MouseTester : GameObject
    {
        public MouseTester()
        {
            Sprite = new Sprite();
            Sprite.Scale *= 40;
        }

        protected override void addInitialScripts()
        {
            addScript(new MouseScript());
        }
    }
}
