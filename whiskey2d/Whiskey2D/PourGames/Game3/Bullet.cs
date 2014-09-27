using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Whiskey2D.PourGames.Game3
{
    class Bullet : GameObject
    {

        public Bullet()
        {
            Velocity = Vector2.One;
            Sprite = new Sprite(GameManager.Resources.loadImage("bullet.png"));
            Sprite.Center();
            Sprite.Color = Color.Yellow;
        }

        public Vector2 Velocity { get; set; }

        protected override void addInitialScripts()
        {
            addScript(new BulletScript());
        }
    }
}
