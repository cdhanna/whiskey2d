using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace Whiskey2D.PourGames.TestImpl
{
    class StarField : GameObject
    {

        public float StarRotation { get; set; }

        
        protected override void addInitialScripts()
        {
            this.addScript(new StarFieldMoveScript());
        }
    }
}
