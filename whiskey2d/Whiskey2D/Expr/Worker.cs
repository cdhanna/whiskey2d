using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Expr
{


    public abstract class Worker<G> where G : Gob
    {

        Gob Thing { get; set; }
    }


}
