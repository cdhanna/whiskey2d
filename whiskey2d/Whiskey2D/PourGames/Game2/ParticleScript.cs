using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace Whiskey2D.PourGames.Game2
{
    class ParticleScript : Script<Particle>
    {
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            //Particle p = Gob;

            Gob.Position += Gob.Velocity;
            Gob.Sprite.Scale *= .97f;
            Gob.Velocity += Vector2.UnitY * .2f;
            if (Gob.Sprite.Scale.Length() < .1f)
            {
                Gob.close();
            }

        }
    }
}
