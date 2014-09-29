using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;


namespace Whiskey2D.PourGames.TestImpl
{
    class Launch : Starter
    {

        public override void start()
        {
            Console.WriteLine("HELLO WORLD");
            //put game init
            new MouseTester();

            addFloor(new Vector(0, 0), new Vector(20, 600));      //left wall
            addFloor(new Vector(20, 460), new Vector(760, 20));   //floor
            addFloor(new Vector(780, 0), new Vector(20, 480));    //right wall
            addFloor(new Vector(20, 0), new Vector(760, 20));

            addFloor(new Vector(200, 405), new Vector(100, 20));  //platforms
            addFloor(new Vector(350, 370), new Vector(100, 20));  //..
            addFloor(new Vector(500, 320), new Vector(100, 20));  //..
            addFloor(new Vector(350, 280), new Vector(100, 20));  //..
            addFloor(new Vector(200, 240), new Vector(100, 20));  //..

            addStars(new Vector(100, 420), .4f);                   //stars
            addStars(new Vector(300, 520), .3f);
            addStars(new Vector(500, 420), .4f);
            addStars(new Vector(700, 520), .3f);
            addStars(new Vector(300, 320), .2f);
      

            Player player = new Player();
            player.Position = new Vector(200, 250);
            player.Sprite = new Sprite();
            player.Sprite.Scale = new Vector(20, 20);
            player.Sprite.Center();
            player.Sprite.Color = Color.DarkSeaGreen;

            SimpleGameObject pour1 = new SimpleGameObject();
            pour1.Position = new Vector(400, 470);
            pour1.Sprite = new Sprite("pour1.png");
            pour1.Sprite.Offset = new Vector(pour1.Sprite.ImageWidth / 2, pour1.Sprite.ImageHeight);
            pour1.Sprite.Depth = .2f;
            pour1.Sprite.Color = Color.Gray;

        }

        public Floor addFloor(Vector position, Vector size)
        {
            Floor floor = new Floor();
            floor.Position = position;
            floor.Sprite = new Sprite();
            floor.Sprite.Scale = size;
            floor.Sprite.Color = Color.Black;
            return floor;
        }

        public StarField addStars(Vector position, float depth)
        {
            StarField stars = new StarField();
            stars.Position = position;
            stars.Sprite = new Sprite("stars1.png");
            stars.Sprite.Scale *= 2;
            stars.Sprite.Center();
            stars.Sprite.Depth = depth;
            stars.Sprite.Color = Color.LightGreen;
            return stars;
        }

    }
}
