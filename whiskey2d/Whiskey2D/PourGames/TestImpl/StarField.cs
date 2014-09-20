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

        protected override List<Script> getInitialScripts()
        {
            List<Script> scripts = new List<Script>();
            scripts.Add(new StarFieldMoveScript());
            return scripts;
        }
    }
}
