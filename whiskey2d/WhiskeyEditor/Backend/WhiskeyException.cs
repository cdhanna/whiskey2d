using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class WhiskeyException : Exception
    {
        public WhiskeyException(string msg)
            : base(msg)
        {
        }
    }
}
