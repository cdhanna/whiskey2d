using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.UI.Properties.Converters
{
    using WhiskeyColor = Whiskey2D.Core.Color;

    class ColorConverter : Converter<WhiskeyColor>
    {


        public override string convertToString(WhiskeyColor c)
        {
            return c.R + ", " + c.G + ", " + c.B + ", " + c.A;
        }

        public override WhiskeyColor convertFromString_(string str)
        {
            int r = 0;
            int g = 0;
            int b = 0;
            int a = 0;

            string[] parts = str.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();

                if (i == 0) //red
                    r = parseInt(parts[i]);
                if (i == 1) //green
                    g = parseInt(parts[i]);
                if (i == 2) //blue
                    b = parseInt(parts[i]);
                if (i == 3) //alpha
                    a = parseInt(parts[i]);


            }

            //bound
            r = bound(r);
            g = bound(g);
            b = bound(b);
            a = bound(a);


            return new WhiskeyColor(r, g, b, a);
        }


    }
}
