using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.Game2
{
    class BackgroundScript : Script
    {
        public override void onStart()
        {
            
        }

        public override void onUpdate()
        {

            Gob.Position.X -= .5f;

            if (Gob.Bounds.Right < 0)
            {
                Gob.Position.X = 2080;

            }
        }
    }
}
