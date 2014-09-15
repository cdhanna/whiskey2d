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


            addFloor(new Vector2(0, 0), new Vector2(20, 600));      //left wall
            addFloor(new Vector2(20, 460), new Vector2(760, 20));   //floor
            addFloor(new Vector2(780, 0), new Vector2(20, 480));    //right wall

            addFloor(new Vector2(200, 405), new Vector2(100, 20));  //platforms
            addFloor(new Vector2(350, 370), new Vector2(100, 20));  //..
            addFloor(new Vector2(500, 320), new Vector2(100, 20));  //..
            addFloor(new Vector2(350, 280), new Vector2(100, 20));  //..
            addFloor(new Vector2(200, 240), new Vector2(100, 20));  //..

            Player player = new Player();
            player.Position = new Vector2(200, 250);
            player.Sprite = new Sprite(ResourceManager.getInstance().loadImage("ai.png"));
            player.Sprite.Center();
            player.Sprite.Rotation = .2f;
        }

        public Floor addFloor(Vector2 position, Vector2 size)
        {
            Floor floor = new Floor();
            floor.Position = position;
            floor.Sprite = new Sprite(RenderManager.getInstance().getPixel());
            floor.Sprite.Scale = size;
            floor.Sprite.Color = Color.DarkSlateGray;
            return floor;
        }

    }
}
