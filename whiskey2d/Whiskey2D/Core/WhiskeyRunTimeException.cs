using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    class WhiskeyRunTimeException : Exception
    {
        public WhiskeyRunTimeException(string msg)
            : base(msg)
        {
            GameManager.Log.error(msg);
        }

    }
}
