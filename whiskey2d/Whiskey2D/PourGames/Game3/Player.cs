using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.PourGames.Game3
{
    class Player : GameObject
    {

        public Player()
        {
            Name = "Ricky J";
            FireRate = 10;
            IdleImagePath = "ricky_simple.png";
            ShootImagePath = "ricky_shoot.png";
            BulletSpeed = 4;
            Sprite = new Sprite(GameManager.Resources.loadImage(IdleImagePath));
            Sprite.Center();
            Sprite.Depth -= .1f;
            CrossHair = new Crosshair();
        }

        public Crosshair CrossHair { get; set; }
        public string IdleImagePath { get; set; }
        public string ShootImagePath { get; set; }
        public string Name { get; set; }
        public int FireRate { get; set; }
        public float BulletSpeed { get; set; }

        protected override void addInitialScripts()
        {
            addScript(new PlayerScript());
        }
    }
}
