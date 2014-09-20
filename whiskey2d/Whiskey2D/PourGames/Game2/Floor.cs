using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;


namespace Whiskey2D.PourGames.Game2
{
    class Floor : GameObject
    {

        public Floor()
        {
            Sprite = new Sprite(RenderManager.getInstance().getPixel());
            Size = new Vector2(20, 20);

        }

        private Vector2 size;
        public float speed = 1;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                Sprite.Scale = value;
            }
        }

        protected override List<Script> getInitialScripts()
        {
            List<Script> scripts = new List<Script>();
            scripts.Add(new FloorScript());
            return scripts;
        }
    }
}
