using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Whiskey2D.TestImpl
{
    class PlayerMoveScript : Script
    {

        public Vector2 gravity = new Vector2(0, .2f);
        public Vector2 velocity = Vector2.Zero;
        public float maxSpeed = 4;
        public float acceleration = .5f;
        public float friction = .8f;
        public float jumpGravity = 25;
        public bool onGround = false;
        public Vector2 jumpNormal = new Vector2(0, -1);

        public override void onStart()
        {
           // throw new NotImplementedException();
        }

        public override void onUpdate()
        {

            Player plr = (Player)Gob; //nasty. 

            ObjectManager objMan = ObjectManager.getInstance();

            List<Floor> walls = objMan.getAllObjectsOfType<Floor>();

            Vector2 leftEdge = plr.Position - plr.Sprite.ImageSize.X*Vector2.UnitX/2;
            Vector2 rightEdge = plr.Position + plr.Sprite.ImageSize.X*Vector2.UnitX/2;
            Vector2 topEdge = plr.Position - plr.Sprite.ImageSize.Y*Vector2.UnitY/2;
            Vector2 bottamEdge = plr.Position + plr.Sprite.ImageSize.Y*Vector2.UnitY/2;

            Boolean yHit = false;
            Boolean xHit = false;

            velocity += gravity;

            if (InputManager.getInstance().isKeyDown(Keys.Right))
            {
                velocity.X += acceleration;
            }
            if (InputManager.getInstance().isKeyDown(Keys.Left))
            {
                velocity.X -= acceleration;
            }
            if (InputManager.getInstance().isNewKeyDown(Keys.Up) && onGround == true)
            {
                velocity += jumpNormal * gravity.Length() * jumpGravity;
                onGround = false;
            }

            if (Math.Abs(velocity.X) > maxSpeed)
            {
                velocity.X = maxSpeed * Math.Sign(velocity.X);
            }


            foreach (Floor wall in walls){


                if (wall.Bounds.vectorWithin(leftEdge + velocity))
                {
                    xHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(2, -.5f);
                }
                if (wall.Bounds.vectorWithin(rightEdge + velocity))
                {
                    xHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(-2, -.5f);
                }

                if (wall.Bounds.vectorWithin(topEdge + velocity))
                {
                    yHit = true;
                }
                if (wall.Bounds.vectorWithin(bottamEdge + velocity))
                {
                    yHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(0, -1);
                }



            }

           
            if (yHit){
                velocity.Y = 0;
            }

            if (xHit)
            {
                velocity.X = 0;
            }

            plr.Position += velocity;
            velocity.X *= friction;
        //    foreach (GameObject otherGob in allGobs)
        //    {

        //        //Y CONSIDER
        //        if (otherGob is Floor)
        //        {
        //            Floor floor = (Floor)otherGob;


        //            if (plr.Position.X + velocity.X >= floor.Position.X &&
        //                plr.Position.X + velocity.X <= floor.Position.X + floor.Size.X &&
        //                plr.Position.Y + velocity.Y >= floor.Position.Y &&
        //                plr.Position.Y + velocity.Y <= floor.Position.Y + floor.Size.Y)
        //            {
        //                velocity.Y = 0;

        //                velocity.X = 0;
        //                gravity = 0;

        //                float creepSize = .1f;
        //                Vector2 creep = Vector2.Normalize(velocity) * creepSize;


        //                while ((plr.Position.X + creep.X <= floor.Position.X ||
        //                        plr.Position.X + creep.X >= floor.Position.X + floor.Size.X ||
        //                        plr.Position.Y + creep.Y <= floor.Position.Y ||
        //                        plr.Position.Y + creep.Y >= floor.Position.Y + floor.Size.Y))
        //                {
        //                    plr.Position += creep;
        //                }
        //            }
        //        }

        //    }

        //    velocity.X *= .6f;
            
        //    {
        //        plr.Position += velocity;
        //    }

        //    if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
        //        velocity.X = moveSpeed;

        //    if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
        //        velocity.X = -moveSpeed;
        //    if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
        //        velocity.Y = -moveSpeed*4;

        //    velocity.Y += gravity;
            
        //    if (velocity.Y > moveSpeed)
        //        velocity.Y = moveSpeed;

        //    gravity = 1;
        }






    }
}
