using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace Whiskey2D.TestImpl
{
    class PlayerMoveScript : Script
    {

        public float gravity = 1;
        public Vector2 velocity = Vector2.Zero;
        public float moveSpeed = 3;

        public override void onStart()
        {
           // throw new NotImplementedException();
        }

        public override void onUpdate()
        {

            Player plr = (Player)Gob;


            // velocity.Y = gravity;
            //velocity.Y = 0;
            //velocity.X = 0;
          //  velocity = Vector2.Zero;


           

            ObjectManager objMan = ObjectManager.getInstance();

            List<GameObject> allGobs = objMan.getAllObjects();
            bool didYHit = false;
            foreach (GameObject otherGob in allGobs)
            {

                //Y CONSIDER
                if (otherGob is Floor)
                {
                    Floor floor = (Floor)otherGob;


                    if (plr.Position.X + velocity.X >= floor.Position.X &&
                        plr.Position.X + velocity.X <= floor.Position.X + floor.Size.X &&
                        plr.Position.Y + velocity.Y >= floor.Position.Y &&
                        plr.Position.Y + velocity.Y <= floor.Position.Y + floor.Size.Y)
                    {
                        //didYHit = true;
                        //plr.Position.Y = floor.Position.Y;
                        //plr.Position.Y -= velocity.Y;
                        velocity.Y = 0;
                       // velocity.X = 0;

                        float creepSize = .1f;
                        Vector2 creep = Vector2.Normalize(velocity)*creepSize;
                        //Vector2 creep = new Vector2(0, creepSize);


                        while ((plr.Position.X + creep.X <= floor.Position.X ||
                                plr.Position.X + creep.X >= floor.Position.X + floor.Size.X ||
                                plr.Position.Y + creep.Y <= floor.Position.Y ||
                                plr.Position.Y + creep.Y >= floor.Position.Y + floor.Size.Y))
                        {
                            plr.Position += creep;
                            //plr.Position += Vector2.Normalize(velocity) * creepSize;
                        }
                        //plr.Position -= Vector2.Normalize(velocity) * creepSize;


                    }

                }

                //X CONSIDER

         

            }
            //if (!didYHit)
            //{
            //    velocity.Y = gravity;
            //}

            velocity.X *= .6f;
            




            {
                plr.Position += velocity;
            }

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                velocity.X = moveSpeed;

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                velocity.X = -moveSpeed;
           // velocity.Y = gravity;


        }






    }
}
