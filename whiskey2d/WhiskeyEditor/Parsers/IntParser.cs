using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Parsers
{
    class IntParser : Parser<int>
    {


        protected override int toValue(string str)
        {
            return int.Parse(str);
        }

        protected override string toString(int value)
        {
            return "" + value;
        }

        protected override int getDefaultValue()
        {
            return 0;
        }

        protected override string code(int value)
        {
            return toString(value);
        }
    }
}
