using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game2
{
    class GameControl : GameObject
    {
        public float gameSpeed = 1;



        protected override void addInitialScripts()
        {
            this.addScript(new FloorControlScript());
        }
    }
}
