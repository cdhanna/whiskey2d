using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;

namespace Whiskey2D.TestImpl
{
    class StarFieldMoveScript : Script
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

            List<Player> playerList = ObjectManager.getInstance().getAllObjectsOfType<Player>();
            Player plr = playerList[0];

            stars.Position -= plr.Velocity * stars.Sprite.Depth*.5f;
            stars.Sprite.Rotation += stars.StarRotation;
        }
    }
}
