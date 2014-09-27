using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.PourGames.Game3
{
    class Game3Launch : Starter
    {
        public override void start()
        {

            Player ricky = new Player();
            Mouse.SetPosition(100, 100);
            ricky.Position = new Vector2(100, 100);


            BadGuy bg = new BadGuy();
            bg.Position = new Vector2(400, 300);

        }
    }
}
