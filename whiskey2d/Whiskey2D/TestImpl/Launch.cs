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

            addStars(new Vector2(100, 420), .4f);                   //stars
            addStars(new Vector2(300, 520), .3f);
            addStars(new Vector2(500, 420), .4f);
            addStars(new Vector2(700, 520), .3f);
            addStars(new Vector2(300, 320), .2f);
      

            Player player = new Player();
            player.Position = new Vector2(200, 250);
            player.Sprite = new Sprite(RenderManager.getInstance().getPixel());
            player.Sprite.Scale = new Vector2(20, 20);
            player.Sprite.Center();
            player.Sprite.Color = Color.DarkSeaGreen;

            SimpleGameObject pour1 = new SimpleGameObject();
            pour1.Position = new Vector2(400, 470);
            pour1.Sprite = new Sprite(ResourceManager.getInstance().loadImage("pour1.png"));
            pour1.Sprite.Offset = new Vector2(pour1.Sprite.Image.Width / 2, pour1.Sprite.Image.Height);
            pour1.Sprite.Depth = .2f;
            pour1.Sprite.Color = Color.Gray;

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

        public StarField addStars(Vector2 position, float depth)
        {
            StarField stars = new StarField();
            stars.Position = position;
            stars.Sprite = new Sprite(ResourceManager.getInstance().loadImage("stars1.png"));
            stars.Sprite.Scale *= 2;
            stars.Sprite.Center();
            stars.Sprite.Depth = depth;
            stars.Sprite.Color = Color.LightGreen;
            return stars;
        }

    }
}
