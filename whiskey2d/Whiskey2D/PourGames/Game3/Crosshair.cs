using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace Whiskey2D.PourGames.Game3
{
    class Crosshair : GameObject
    {
        public Crosshair()
        {
            Sprite = new Sprite(GameManager.Resources.loadImage("crosshair.png"));
            Sprite.Center();
            Sprite.Color = Color.Red;
        }

        protected override void addInitialScripts()
        {
            addScript(new CrosshairScript());
        }
    }
}
