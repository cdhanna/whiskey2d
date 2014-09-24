using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Expr
{
    interface GobConverter<G> where G : Gob
    {
        G getGob(Gob gob);
    }
}
