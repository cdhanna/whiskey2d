using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WhiskeyEditor.UI.Properties.Converters
{
    class WhiskeyTypeConverters
    {
        private static Dictionary<string, TypeConverter> map;

        static WhiskeyTypeConverters()
        {
            map = new Dictionary<string, TypeConverter>();

            map.Add("Sprite", new SpriteTypeConverter());
        }

        public static TypeConverter lookUp(string typeName)
        {
            if (map.ContainsKey(typeName))
            {
                return map[typeName];
            } else return null;
        }

    }
}
