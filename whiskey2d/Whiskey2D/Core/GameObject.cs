using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    public class GameObject
    {
        private static int idCounter = 0;


        /// <summary>
        /// Create a new Game Object
        /// </summary>
        public GameObject()
        {
            Position = Vector2.Zero;
            Sprite = null;
            ID = idCounter++;

            ObjectManager.getInstance().addObject(this);

        }

        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }
        public int ID { get; set; }

        /// <summary>
        /// Initializes the GameObject
        /// </summary>
        public void init()
        {
            
        }

        /// <summary>
        /// Closes out the GameObject, and removes it from the ObjectManager
        /// </summary>
        public void close()
        {
            ObjectManager.getInstance().removeObject(this);
        }

     

        public void update()
        {

            Position = new Vector2(Position.X + 1f, Position.Y);

        }



    }
}
