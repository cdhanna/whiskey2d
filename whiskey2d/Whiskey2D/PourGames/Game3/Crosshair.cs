using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    class Crosshair : GameObject
    {
        public Crosshair()
        {
            Sprite = new Sprite("crosshair.png");
            Sprite.Center();
            Sprite.Color = Color.Red;
        }

        protected override void addInitialScripts()
        {
            addScript(new CrosshairScript());
        }
    }
}
