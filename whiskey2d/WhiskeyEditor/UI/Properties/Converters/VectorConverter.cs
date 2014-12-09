using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;

namespace WhiskeyEditor.UI.Properties.Converters
{
    class VectorConverter : Converter<Vector>
    {
        public override string convertToString(Vector obj)
        {

            return obj.X + ", " + obj.Y;

        }

        public override Vector convertFromString_(string str)
        {
            int x = 0 , y = 0;
            string[] parts = str.Split(',');
            for (int i = 0; i < Math.Min(2, parts.Length); i++)
            {
                parts[i] = parts[i].Trim();
                if (i == 0)
                    x = parseInt(parts[i]);
                if (i == 1)
                    y = parseInt(parts[i]);
            }
            if (parts.Length == 1)
            {
                y = x;
            }
            return new Vector(x, y);
        }
    }
}
