using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    interface TypeVal
    {

        string TypeName { get; }
        object value { get; set; }

        TypeVal clone();

    }
}
