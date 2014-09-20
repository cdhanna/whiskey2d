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
            addFloor(new Vector2(100, 300), new Vector2(800, 20));
   
            new GameControl();


            Player player = new Player();
            player.Position = new Vector2(200, 250);
            player.Sprite = new Sprite(RenderManager.getInstance().getPixel());
            player.Sprite.Scale = new Vector2(20, 20);
            player.Sprite.Center();
            player.Sprite.Color = Color.DarkSeaGreen;

        }

        public Floor addFloor(Vector2 position, Vector2 size)
        {
            Floor floor = new Floor();
            floor.Position = position;
            floor.Sprite.Scale = size;
            floor.Sprite.Color = Color.Black;
            return floor;
        }

    }
}
