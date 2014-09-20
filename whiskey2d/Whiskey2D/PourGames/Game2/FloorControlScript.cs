using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game2
{
    class FloorControlScript : Script
    {

        float ticksUntilNewFloor;
        float ticksUntilNewFloorLimit = 200;
        Random r = new Random(0);


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

                Floor floor = new Floor();
                floor.Position = new Vector2(800, 100);
                floor.Size = new Vector2(100, 20);
                floor.Sprite.Color = Color.Black;


                float[] distances = new float[]{80, 40, -40, -80};

                float deltaY = distances[r.Next(distances.Length)];
                float newY = deltaY + oldY;
                if (newY > 400 || newY < 100)
                {
                    deltaY *= -1;
                    newY = deltaY + oldY;
                }
                
                
                floor.Position.Y = newY;
                oldY = newY;

                float speed = ObjectManager.getInstance().getAllObjectsOfType<GameControl>()[0].gameSpeed;
                floor.speed = speed;
                ObjectManager.getInstance().getAllObjectsOfType<Floor>().ForEach((f) => { f.speed = speed; });


                //speed *= 1.1f;
                //ticksUntilNewFloorLimit *= .9f;
                ticksUntilNewFloor = ticksUntilNewFloorLimit;
            }


            ticksUntilNewFloor--;

        }
    }
}
