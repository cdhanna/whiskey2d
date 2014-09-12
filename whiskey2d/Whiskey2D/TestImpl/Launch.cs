using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace Whiskey2D.TestImpl
{
    class Launch : Starter
    {

        public override void start()
        {
            Console.WriteLine("HELLO WORLD");
            //put game init
            Floor baseFloor = new Floor();
            baseFloor.Position = new Vector2(100, 300);
            baseFloor.Size = new Vector2(200, 20);

            Floor leftWall = new Floor();
            leftWall.Position = new Vector2(100, 100);
            leftWall.Size = new Vector2(80, 300);

            Player player = new Player();
            player.Position = new Vector2(200, 250);
            player.Sprite = new Sprite(ResourceManager.getInstance().loadImage("ai.png"));
        }
    }
}
