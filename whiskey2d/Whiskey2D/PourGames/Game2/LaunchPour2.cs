using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Whiskey2D.PourGames.TestImpl;

namespace Whiskey2D.PourGames.Game2
{
    class LaunchPour2 : Starter
    {
        public override void start()
        {
            addFloor(new Vector2(100, 300), 22);
   
            new GameControl();
            Background b1 = new Background();
            Background b2 = new Background();
            b2.Position.X = b1.Sprite.ImageSize.X;


            Player player = new Player();
            player.Position = new Vector2(200, 250);
            player.Sprite = new Sprite(RenderManager.getInstance().getPixel());
            player.Sprite.Scale = new Vector2(20, 20);
            player.Sprite.Center();
            player.Sprite.Color = Color.DarkGreen;
            player.Sprite.Depth = 1;

        }

        public void addFloor(Vector2 position, int n)
        {


            Floor floor = null;
            for (int i = 0; i < n; i++)
            {
                floor = new Floor();
                floor.Position = position;
                floor.Position.X += floor.Sprite.ImageSize.X * i;
            }
            Floor end = new Floor();
            end.Sprite = new Sprite(ResourceManager.getInstance().loadImage("grass_right.png"));
            end.Sprite.Scale *= .5f;
            end.Position = position;
            end.Position.X += floor.Sprite.ImageSize.X * n;

            Floor start = new Floor();
            start.Sprite = new Sprite(ResourceManager.getInstance().loadImage("grass_left.png"));
            start.Sprite.Scale *= .5f;
            start.Position = position;
            start.Position.X -= start.Sprite.ImageSize.X;


           // floor.Sprite.Scale = size;
           // floor.Sprite.Color = Color.Black;
            
        }

    }
}
