using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.ex2
{
    class ScriptImpl : Script<GobImpl>
    {

        //public override void update()
        //{
        //    GOB.x = 5;
        //}
        //public override void update()
        //{
        //    GOB.x = 5;
        //}


        public override void updatePlease()
        {
            Gob.x++;
        }

        public override void onStart()
        {
            throw new NotImplementedException();
        }
    }
}
