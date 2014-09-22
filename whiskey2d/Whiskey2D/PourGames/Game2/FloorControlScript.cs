using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game2
{
    class FloorControlScript : Script<GameControl>
    {

        float ticksUntilNewFloor;
        float ticksUntilNewFloorLimit = 200;


        Rand r = Rand.getInstance();


        float oldY;

        public override void onStart()
        {
            ticksUntilNewFloor = ticksUntilNewFloorLimit;
            //oldY = r.Next(150, 460); 
            oldY = 300;
        }

        public override void onUpdate()
        {

            if (ticksUntilNewFloor < 0)
            {

                float[] distances = new float[] { 80, 40, -40, -80 };

                float deltaY = distances[r.Next(distances.Length)];
                float newY = deltaY + oldY;
                if (newY > GameManager.getInstance().ScreenHeight-50 || newY < 100)
                {
                    deltaY *= -1;
                    newY = deltaY + oldY;
                }
                oldY = newY;

                Floor floor = null;
                int floorSegments = r.Next(2, 5);
                for (int i = 0; i < floorSegments; i++)
                {
                    floor = new Floor();
                    floor.Position = new Vector2(800, 100);
                    floor.Position.X += floor.Sprite.ImageSize.X * i;

                    floor.Position.Y = newY;
                   
                }
                Floor end = new Floor();
                end.Sprite = new Sprite(ResourceManager.getInstance().loadImage("grass_right.png"));
                end.Sprite.Scale *= .5f;
                end.Position = new Vector2(800, 100);
                end.Position.X += floor.Sprite.ImageSize.X * floorSegments;
                end.Position.Y = newY;

                Floor start = new Floor();
                start.Sprite = new Sprite(ResourceManager.getInstance().loadImage("grass_left.png"));
                start.Sprite.Scale *= .5f;
                start.Position = new Vector2(800, 100); 
                start.Position.X -= start.Sprite.ImageSize.X;
                start.Position.Y = newY;

                LogManager.getInstance().debug("created a floor segment at " + newY);

                //float speed = ObjectManager.getInstance().getAllObjectsOfType<GameControl>()[0].gameSpeed;
                //ObjectManager.getInstance().getAllObjectsOfType<Floor>().ForEach((f) => { f.speed = speed; });


                //speed *= 1.1f;
                //ticksUntilNewFloorLimit *= .9f;
                ticksUntilNewFloor = ticksUntilNewFloorLimit - (3 - floorSegments)*10;
            }


            ticksUntilNewFloor--;

        }
    }
}
