using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;


namespace Whiskey2D.PourGames.Game2
{
    class Particle : GameObject
    {

        Rand r = Rand.getInstance();

        public Vector Velocity;


        public Particle(Vector position, Vector velocity)
        {
            Sprite = new Sprite();
            Sprite.Color = Color.Blue;
            Sprite.Scale *= r.nextFloat() * 10;
            Sprite.Depth = .6f;
            Sprite.Center();
            Position = position;
            Velocity = velocity;
        }

        

        protected override void addInitialScripts()
        {
            
            this.addScript(new ParticleScript());
        }
    }
}
