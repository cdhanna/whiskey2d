using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Expr
{
    class WorkerImpl : Worker<GobImpl>
    {

        public void someMethod()
        {

            GobImpl impl = this.Thing;
            impl.Name = "toast";

        }


        public GobImpl Thing
        {
            get;
            set;
        }
    }
}
