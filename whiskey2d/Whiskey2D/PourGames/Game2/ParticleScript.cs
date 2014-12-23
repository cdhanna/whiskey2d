using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.PourGames.Game2
{
    class ParticleScript : Script<Particle>
    {
        public override void onStart()
        {
        }
        public override void onClose()
        {

        }
        public override void onUpdate()
        {
            //Particle p = Gob;

            Gob.Position += Gob.Velocity;
            Gob.Sprite.Scale *= .97f;
            Gob.Velocity += Vector.UnitY * .2f;
            if (Gob.Sprite.Scale.Length() < .1f)
            {
                Gob.close();
            }

        }
    }
}
