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
        /// Closes out the GameObject
        /// </summary>
        public void close()
        {

        }

     

        public void update()
        {

        }



    }
}
