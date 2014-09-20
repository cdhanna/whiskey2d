using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Whiskey2D.Core;
namespace Whiskey2D.PourGames.TestImpl
{
    class Player : GameObject
    {

        public Vector2 Velocity { get; set; }

        protected override List<Script> getInitialScripts()
        {
            List<Script> scripts = new List<Script>();
            scripts.Add(new PlayerMoveScript());
            scripts.Add(new PlayerScaleScript());

            return scripts;
        }

    }
}
