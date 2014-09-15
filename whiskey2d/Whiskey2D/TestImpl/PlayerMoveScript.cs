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
        public float maxSpeed = 7;
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

            Vector2 leftEdge = plr.Position - plr.Sprite.ImageSize.X * Vector2.UnitX / 2;
            Vector2 rightEdge = plr.Position + plr.Sprite.ImageSize.X * Vector2.UnitX / 2;
            Vector2 topEdge = plr.Position - plr.Sprite.ImageSize.Y * Vector2.UnitY / 2;
            Vector2 bottamEdge = plr.Position + plr.Sprite.ImageSize.Y * Vector2.UnitY / 2;

            //Vector2 leftEdge = plr.Position - plr.Sprite.Scale.X * Vector2.UnitX / 2;
            //Vector2 rightEdge = plr.Position + plr.Sprite.Scale.X * Vector2.UnitX / 2;
            //Vector2 topEdge = plr.Position - plr.Sprite.Scale.Y * Vector2.UnitY / 2;
            //Vector2 bottamEdge = plr.Position + plr.Sprite.Scale.Y * Vector2.UnitY / 2;

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
                    jumpNormal = new Vector2(2, -1f);
                    plr.Position.X = wall.Bounds.Right + plr.Sprite.ImageSize.X / 2;
                }
                if (wall.Bounds.vectorWithin(rightEdge + velocity))
                {
                    xHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(-2, -1f);
                   
                    plr.Position.X = wall.Bounds.Left - plr.Sprite.ImageSize.X / 2;
                }
                if (wall.Bounds.vectorWithin(bottamEdge + velocity))
                {
                    plr.Position.Y = wall.Bounds.Top - plr.Sprite.ImageSize.Y / 2;

                    yHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(0, -1);
                }
                if (wall.Bounds.vectorWithin(topEdge + velocity))
                {
                    //topEdge = plr.Position - plr.Sprite.ImageSize.Y * Vector2.UnitY / 2;
                    plr.Position.Y = wall.Bounds.Bottam + plr.Sprite.ImageSize.Y/2;

                    yHit = true;
                }




            }

           
            if (yHit)
            {
                //velocity.Y *= -.1f;
                velocity.Y = 0;
            }

            if (xHit)
            {
                velocity.X *= -.2f;
            }

            plr.Position += velocity;
            velocity.X *= friction;
            plr.Velocity = velocity;
            
        }






    }
}
