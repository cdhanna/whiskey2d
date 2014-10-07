using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.Game3
{
    [Serializable]
    public class BadGuy : GameObject
    {

        public BadGuy()
        {
            Speed = 2;
            Direction = Vector.One;

            Sprite = new Sprite("badguy.png");
            Sprite.Center();
            Sprite.Color = Color.Orange;
        }


        public float Speed { get; set; }
        public Vector Direction { get; set; }

        protected override void addInitialScripts()
        {
            addScript(new BadGuyScript());
        }
    }
}
