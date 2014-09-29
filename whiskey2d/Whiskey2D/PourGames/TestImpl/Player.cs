using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
namespace Whiskey2D.PourGames.TestImpl
{
    class Player : GameObject
    {

        public Vector Velocity { get; set; }

       




        protected override void addInitialScripts()
        {
            this.addScript(new PlayerMoveScript());
            this.addScript(new PlayerScaleScript());
        }
    }
}
