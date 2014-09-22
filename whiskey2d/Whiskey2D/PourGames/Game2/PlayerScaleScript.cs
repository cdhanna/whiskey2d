using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;


namespace Whiskey2D.PourGames.Game2
{
    class PlayerScaleScript : Script<Player>
    {
        public Vector2 targetScale;

        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            Player plr = Gob;

            targetScale = (Vector2.One * 30) + new Vector2(
                 Math.Max(-6, Math.Abs(plr.Velocity.X) - Math.Abs(plr.Velocity.Y)),
                 Math.Max(-15, Math.Abs(plr.Velocity.Y) - Math.Abs(plr.Velocity.X))) * 3;

            Vector2 scaleDiff = targetScale - plr.Sprite.Scale;
            plr.Sprite.Scale += scaleDiff * .1f;

        }
    }
}
