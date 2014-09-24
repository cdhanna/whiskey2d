using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.ex2
{
    partial class GobImpl : Gob
    {



        public int x;

        protected override void addScripts()
        {
            this.addScript(new ScriptImpl());
        }
    }


}
