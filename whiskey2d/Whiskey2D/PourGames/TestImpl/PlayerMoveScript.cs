using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework.Input;


namespace Whiskey2D.PourGames.TestImpl
{
    class PlayerMoveScript : Script<Player>
    {

        public Vector gravity = new Vector(0, .2f);
        public Vector velocity = Vector.Zero;
        public float maxSpeed = 7;
        public float acceleration = .5f;
        public float friction = .8f;
        public float jumpGravity = 25;
        public bool onGround = false;
        public Vector jumpNormal = new Vector(0, -1);

        public override void onStart()
        {

        }
        public override void onClose()
        {

        }
        public override void onUpdate()
        {

            Player plr = (Player)Gob; //nasty. 

            //ObjectManager objMan = ObjectManager.getInstance();

            List<Floor> walls = GameManager.Objects.getAllObjectsOfType<Floor>();

            Vector leftEdge = plr.Position - plr.Sprite.ImageSize.X * Vector.UnitX / 2;
            Vector rightEdge = plr.Position + plr.Sprite.ImageSize.X * Vector.UnitX / 2;
            Vector topEdge = plr.Position - plr.Sprite.ImageSize.Y * Vector.UnitY / 2;
            Vector bottamEdge = plr.Position + plr.Sprite.ImageSize.Y * Vector.UnitY / 2;

       
            Boolean yHit = false;
            Boolean xHit = false;

            velocity += gravity;

            if (GameManager.Input.isKeyDown(Keys.Right))
            {
                velocity.X += acceleration;
            }
            if (GameManager.Input.isKeyDown(Keys.Left))
            {
                velocity.X -= acceleration;
            }
            if (GameManager.Input.isNewKeyDown(Keys.Up) && onGround == true)
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
                    jumpNormal = new Vector(2, -1f);
                    plr.Position.X = wall.Bounds.Right + plr.Sprite.ImageSize.X / 2;
                    GameManager.Log.debug("left edge hit a wall");
                }
                if (wall.Bounds.vectorWithin(rightEdge + velocity))
                {
                    xHit = true;
                    onGround = true;
                    jumpNormal = new Vector(-2, -1f);
                   
                    plr.Position.X = wall.Bounds.Left - plr.Sprite.ImageSize.X / 2;
                }
                if (wall.Bounds.vectorWithin(bottamEdge + velocity))
                {
                    plr.Position.Y = wall.Bounds.Top - plr.Sprite.ImageSize.Y / 2;

                    yHit = true;
                    onGround = true;
                    jumpNormal = new Vector(0, -1);
                }
                if (wall.Bounds.vectorWithin(topEdge + velocity))
                {
                    plr.Position.Y = wall.Bounds.Bottam + plr.Sprite.ImageSize.Y/2;

                    yHit = true;
                }




            }

           
            if (yHit)
            {
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
