using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    class CrosshairScript : Script<Crosshair>
    {
        public override void onStart()
        {
        }
        public override void onClose()
        {

        }
        public override void onUpdate()
        {
            Gob.Position = GameManager.Input.getMousePosition();
        }
    }
}
