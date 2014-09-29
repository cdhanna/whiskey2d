using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Whiskey2D.Core.Inputs;


namespace Whiskey2D.PourGames.TestImpl
{
    class MouseScript : Script<MouseTester>
    {
        public override void onStart()
        {
            Gob.Position = GameManager.Input.getMousePosition();

        }

        public override void onUpdate()
        {


            Gob.Position = GameManager.Input.getMousePosition();

            if (GameManager.Input.isNewMouseDown(MouseButtons.Left))
            {
                Gob.Sprite.Color = Rand.getInstance().nextColorVariation(Color.Red, 1, 1, 1, 0);
            }

        }
    }
}
