using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;


namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    class Bullet : GameObject
    {

        public Bullet()
        {
            Velocity = Vector.One;
            Sprite = new Sprite("bullet.png");
            Sprite.Center();
            Sprite.Color = Color.Yellow;
        }

        public Vector Velocity { get; set; }

        protected override void addInitialScripts()
        {
            addScript(new BulletScript());
        }
    }
}
