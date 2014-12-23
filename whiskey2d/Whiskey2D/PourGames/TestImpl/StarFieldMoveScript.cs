using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Whiskey2D.Core;

namespace Whiskey2D.PourGames.TestImpl
{
    class StarFieldMoveScript : Script<StarField>
    {
        Random rand = new Random();

        public override void onStart()
        {
            StarField stars = (StarField)Gob;
            stars.StarRotation = .004f*(float)(rand.NextDouble() - .5);
        }

        public override void onUpdate()
        {
            StarField stars = (StarField)Gob;

            List<Player> playerList = GameManager.Objects.getAllObjectsOfType<Player>();
            Player plr = playerList[0];

            stars.Position -= plr.Velocity * stars.Sprite.Depth*.5f;
            stars.Sprite.Rotation += stars.StarRotation;
        }

        public override void onClose()
        {
            
        }
    }
}
