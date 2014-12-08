using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    public interface TypeVal
    {

        string TypeName { get; }
        object Value { get; set; }

        TypeVal clone();


        event EventHandler ValueChanged;

    }
}
