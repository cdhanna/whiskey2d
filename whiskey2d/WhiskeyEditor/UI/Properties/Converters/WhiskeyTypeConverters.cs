using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WhiskeyEditor.UI.Properties.TypeConverters;



namespace WhiskeyEditor.UI.Properties.Converters
{
    class WhiskeyTypeConverters
    {
        private static Dictionary<string, TypeConverter> map;

        static WhiskeyTypeConverters()
        {
            map = new Dictionary<string, TypeConverter>();

            map.Add("Sprite", new SpriteTypeConverter());
            map.Add("Boolean", new BoolTypeConverter());
            map.Add("ShadowProperties", new ShadowPropertyTypeConverter());
            map.Add("Light", new LightTypeConverter());
            map.Add("InstanceDescriptor", new WhiskeyInstanceTypeConverter());
            map.Add("Layer", new LayerTypeConverter());
            map.Add("Keys", new KeyConverter());
        }

        public static TypeConverter lookUp(string typeName)
        {
            Console.WriteLine("looking for : " + typeName);
            if (map.ContainsKey(typeName))
            {
                return map[typeName];
            } else return null;
        }

    }
}
