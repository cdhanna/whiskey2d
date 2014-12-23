using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    class BulletScript : Script<Bullet>
    {
        public override void onStart()
        {
        }
        public override void onClose()
        {

        }
        public override void onUpdate()
        {
            Gob.Position += Gob.Velocity;

            if (Gob.Position.X < 0 || Gob.Position.X > GameManager.getInstance().ScreenWidth || Gob.Position.Y < 0 || Gob.Position.Y > GameManager.getInstance().ScreenHeight)
            {
                Gob.close();
            }

        }
    }
}
