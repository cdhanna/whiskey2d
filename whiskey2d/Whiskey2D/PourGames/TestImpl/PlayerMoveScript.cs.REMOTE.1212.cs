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
        public float moveSpeed = 4;

        public override void onStart()
        {
           // throw new NotImplementedException();
        }

        public override void onUpdate()
        {

            Player plr = (Player)Gob;

            ObjectManager objMan = ObjectManager.getInstance();

            List<GameObject> allGobs = objMan.getAllObjects();
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
                        velocity.Y = 0;
                        velocity.X = 0;
                        gravity = 0;

                        float creepSize = .1f;
                        Vector2 creep = Vector2.Normalize(velocity) * creepSize;


                        while ((plr.Position.X + creep.X <= floor.Position.X ||
                                plr.Position.X + creep.X >= floor.Position.X + floor.Size.X ||
                                plr.Position.Y + creep.Y <= floor.Position.Y ||
                                plr.Position.Y + creep.Y >= floor.Position.Y + floor.Size.Y))
                        {
                            plr.Position += creep;
                        }
                    }
                }
            }

            velocity.X *= .6f;
            
            {
                plr.Position += velocity;
            }

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                velocity.X = moveSpeed;

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                velocity.X = -moveSpeed;

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                velocity.Y = -moveSpeed*4;

            velocity.Y += gravity;
            
            if (velocity.Y > moveSpeed)
                velocity.Y = moveSpeed;

            gravity = 1;
        }






    }
}
