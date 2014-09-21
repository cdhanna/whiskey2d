using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.Game2
{
    class Background : GameObject
    {
        public Background()
        {
            this.Sprite = new Sprite(ResourceManager.getInstance().loadImage("background.png"));
            this.Sprite.Depth = .3f;
        }


        protected override List<Script> getInitialScripts()
        {
            List<Script> scripts = new List<Script>();
            scripts.Add(new BackgroundScript());
            return scripts;
        }
    }
}
