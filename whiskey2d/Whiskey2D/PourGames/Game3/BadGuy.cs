using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game3
{
    public class BadGuy : GameObject
    {

        public BadGuy()
        {
            Speed = 2;
            Direction = Vector2.One;

            Sprite = new Sprite(GameManager.Resources.loadImage("badguy.png"));
            Sprite.Center();
            Sprite.Color = Color.Orange;
        }

        public float Speed { get; set; }
        public Vector2 Direction { get; set; }

        protected override void addInitialScripts()
        {
            addScript(new BadGuyScript());
        }
    }
}
