using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Whiskey2D.PourGames.Game2
{
    class PlayerMoveScript : Script<Player>
    {

        public Vector2 gravity = new Vector2(0, .2f);
        public Vector2 velocity = Vector2.Zero;
        public float maxSpeed = 7;
        public float acceleration = .5f;
        public float friction = .8f;
        public float jumpGravity = 25;
        public bool onGround = false;
        public Vector2 jumpNormal = new Vector2(0, -1);
        public bool onWall = false;
        public override void onStart()
        {

        }

        public override void onUpdate()
        {

            Player plr = (Player)Gob; //nasty. 



            List<Floor> walls = GameManager.Objects.getAllObjectsOfType<Floor>();

            Vector2 leftEdge = plr.Position - plr.Sprite.ImageSize.X * Vector2.UnitX / 2;
            Vector2 rightEdge = plr.Position + plr.Sprite.ImageSize.X * Vector2.UnitX / 2;
            Vector2 topEdge = plr.Position - plr.Sprite.ImageSize.Y * Vector2.UnitY / 2;
            Vector2 bottamEdge = plr.Position + plr.Sprite.ImageSize.Y * Vector2.UnitY / 2;

            bottamEdge.Y -= 10;
       
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
                    jumpNormal = new Vector2(2, -1f);
                    if (!onWall)
                    {
                        makeParticleCluster(plr.Position + new Vector2(-plr.Sprite.ImageSize.Y / 2, 0), jumpNormal);
                    }
                    onWall = true;
                    plr.Position.X = wall.Bounds.Right + plr.Sprite.ImageSize.X / 2;
                }
                else if (wall.Bounds.vectorWithin(rightEdge + velocity))
                {
                    xHit = true;
                    onGround = true;
                    jumpNormal = new Vector2(-2, -1f);
                    if (!onWall)
                    {
                        makeParticleCluster(plr.Position + new Vector2(plr.Sprite.ImageSize.X / 2, 0), jumpNormal);
                    }
                    onWall = true;
                    plr.Position.X = wall.Bounds.Left - plr.Sprite.ImageSize.X / 2;
                }
                else
                {
                    onWall = false;
                }
                if (wall.Bounds.vectorWithin(bottamEdge + velocity))
                {

             
                    
                    plr.Position.Y = wall.Bounds.Top - plr.Sprite.ImageSize.Y / 2;
                    plr.Position.Y += 10;
                    jumpNormal = new Vector2(0, -1);
                    if (!onGround)
                    {
                        makeParticleCluster(plr.Position + new Vector2(0, plr.Sprite.ImageSize.Y/2), jumpNormal);
                    }

                    yHit = true;
                    onGround = true;
                    
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
                float speed = GameManager.Objects.getAllObjectsOfType<GameControl>()[0].gameSpeed;
                plr.Position.X -= speed;
            }

            if (xHit)
            {
                velocity.X *= -.2f;
            }


            if (plr.Position.Y > 800)
            {
                plr.Position.Y = 0;
                plr.Velocity = gravity;
            }

            plr.Position += velocity;
            velocity.X *= friction;
            plr.Velocity = velocity;
            
        }



        public void makeParticleCluster(Vector2 pos, Vector2 dir)
        {
            Rand r = Rand.getInstance();
            dir = Vector2.Normalize(dir);
            Vector2 orth = new Vector2(dir.Y, -dir.X);
           

            for (int i = 0; i < 20; i++)
            {

                Vector2 start = pos +20 * (orth * (r.nextFloat() - .5f));
                Vector2 vel = (start - (pos - (20 * dir)));
                vel.X -= 1;
                vel.Normalize();
               // 
                vel *= (.5f+r.nextFloat())*4;
                Particle p = new Particle(start, vel);
                p.Sprite.Color = r.nextColorVariation(Color.Green, .3f, .1f, .4f, 0);
                //new Particle(pos + r.nextUnit2() * 20, 3*r.nextUnit2()* (r.nextFloat()+.5f));
            }

        }


    }
}
