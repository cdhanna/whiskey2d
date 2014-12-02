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
            if (GameManager.Log != null)
            {
                GameManager.Log.error(msg);
            }
            else
            {
                Console.WriteLine("WHISKEYERR: " + msg);
                throw new Exception(msg);
            }
        }

    }
}
