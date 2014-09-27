using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Inputs;

namespace Whiskey2D.PourGames.Game3
{
    class PlayerScript : Script<Player>
    {

        private int ticksLeftToFire;
        private int ticksLeftToCloseMouth;
        private bool isMouthOpen = false;

        public override void onStart()
        {
            ticksLeftToFire = Gob.FireRate;
        }

        public override void onUpdate()
        {

            if (GameManager.Input.isKeyDown(Keys.Right))
            {
                Gob.Position += Vector2.UnitX;
            }
            if (GameManager.Input.isKeyDown(Keys.Left))
            {
                Gob.Position -= Vector2.UnitX;
            }
            if (GameManager.Input.isKeyDown(Keys.Up))
            {
                Gob.Position -= Vector2.UnitY;
            }
            if (GameManager.Input.isKeyDown(Keys.Down))
            {
                Gob.Position += Vector2.UnitY;
            }

           
            if (GameManager.Input.isNewMouseDown(MouseButtons.Left) && ticksLeftToFire <= 0)
            {
                ticksLeftToFire = Gob.FireRate;

                Bullet b = new Bullet();
                b.Position = Gob.Position;
                b.Velocity = (Gob.CrossHair.Position - Gob.Position);
                GameManager.Log.debug(Gob.CrossHair.Position.X + " " + Gob.CrossHair.Position.Y);
                b.Velocity /= b.Velocity.Length();
                b.Velocity *= Gob.BulletSpeed;

                isMouthOpen = true;
                ticksLeftToCloseMouth = 20;
                Gob.Sprite.Image = GameManager.Resources.loadImage(Gob.ShootImagePath);
            }

            if (ticksLeftToCloseMouth <= 0 && isMouthOpen)
            {
                isMouthOpen = false;
                Gob.Sprite.Image = GameManager.Resources.loadImage(Gob.IdleImagePath);
            }

            ticksLeftToFire--;
            ticksLeftToCloseMouth--;
        }
    }
}
