using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.UI.Properties.Converters
{

    using WhiskeyException = WhiskeyEditor.Backend.WhiskeyException;

    class ConverterManager
    {

        private static ConverterManager instance = new ConverterManager();
        public static ConverterManager Instance { get { return instance; } }

        private Dictionary<Type, Converter> typeToConvert;

        private ConverterManager()
        {
            typeToConvert = new Dictionary<Type, Converter>();

            registerConvert(new ColorConverter());
            registerConvert(new VectorConverter());
        }

        public void registerConvert(Converter c)
        {
            typeToConvert.Add(c.getConvertType(), c);
        }

        public bool hasConverter(Type type)
        {
            return typeToConvert.ContainsKey(type);
        }

        public Converter getConverter(Type type)
        {
            if (typeToConvert.ContainsKey(type))
                return typeToConvert[type];
            else throw new WhiskeyEditor.Backend.WhiskeyException("No converter for " + type);
        }

        public object convertFromString(Type destType, string str)
        {
            
            Converter c = getConverter(destType);
            return c.convertFromString(str);

        }

        public string convertToString(object obj)
        {
            Converter c = getConverter(obj.GetType());

            return c.convertToString(obj);
        }

    }
}
