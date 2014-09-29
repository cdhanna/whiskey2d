using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    class BadGuyScript : Script<BadGuy>
    {
        public override void onStart()
        {
        }

        public override void onUpdate()
        {


            Vector vel = Gob.Direction * Gob.Speed;
            Gob.Position += vel;

            if (Gob.Position.X - Gob.Sprite.ImageSize.X /2 < 0 )
            {
                Gob.Direction = new Vector(-Gob.Direction.X, Gob.Direction.Y); 
            }
            if (Gob.Position.X + Gob.Sprite.ImageSize.X / 2 > GameManager.getInstance().ScreenWidth)
            {
                Gob.Direction = new Vector(-Gob.Direction.X, Gob.Direction.Y); 
            }
            if (Gob.Position.Y - Gob.Sprite.ImageSize.Y / 2 < 0)
            {
                Gob.Direction = new Vector(Gob.Direction.X, -Gob.Direction.Y); 
            }
            if (Gob.Position.Y + Gob.Sprite.ImageSize.Y / 2 > GameManager.getInstance().ScreenHeight)
            {
                Gob.Direction = new Vector(Gob.Direction.X, -Gob.Direction.Y); 
            }


            List<Bullet> bullets = GameManager.Objects.getAllObjectsOfType<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                float dist = (bullet.Position - Gob.Position).Length();
                if (dist < (Gob.Sprite.ImageSize.X / 2) + (bullet.Sprite.ImageSize.X / 2))
                {
                    bullet.close();
                    Gob.close();
                }
            }

        }
    }
}
