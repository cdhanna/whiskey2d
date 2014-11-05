using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WhiskeyEditor.Parsers
{

    public class Parse
    {
        private static Parse instance = new Parse();
        public static Parse Instance { get { return instance; }}

        private Dictionary<Type, object> typeTable;

        static Parse()
        {
            terminableTypes = new List<Type>();
            terminableTypes.Add(typeof(int));
            terminableTypes.Add(typeof(String));
            terminableTypes.Add(typeof(float));
            terminableTypes.Add(typeof(Single));
            terminableTypes.Add(typeof(double));
            terminableTypes.Add(typeof(bool));
            terminableTypes.Add(typeof(Boolean));
            terminableTypes.Add(typeof(Int16));
        }
        private static List<Type> terminableTypes = new List<Type>();
        public static bool isTerminable(object val)
        {
            if (val == null)
            {
                return true;
            }
            else
            {
                return terminableTypes.Contains(val.GetType());
            }

        }

        public static string stringify(object val)
        {
            if (val == null)
            {
                return "null";
            }
            else if (val.GetType() == typeof(String))
            {
                return "\"" + val + "\"";
            }
            else if (val.GetType() == typeof(float))
            {

                return "" + val + "f";
            }
            else
            {
                return "" + val;
            }

        }


        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        private Parse()
        {
            typeTable = new Dictionary<Type, object>();

            addParser(typeof(Single), new IntParser());

            foreach (Type parseType in GetType().Assembly.GetTypes().Where(t => IsSubclassOfRawGeneric(typeof(Parser<>), t) && t!=(typeof(Parser<>))))
            {
                addParser(parseType);
            }

        }


        private void addParser(Type parseType)
        {
            if (!typeTable.ContainsKey(parseType))
            {
                typeTable.Add(parseType, parseType.GetConstructor(new Type[] { }).Invoke(new Object[] { }));
            }
        }
        private void addParser<T>(Parser<T> parser)
        {
            if (!typeTable.ContainsKey(parser.DefaultValue.GetType()))
            {
                typeTable.Add(parser.DefaultValue.GetType(), parser);
            }
        }
        private void addParser(Type parserType, object parser)
        {
            if (!typeTable.ContainsKey(parserType))
            {
                typeTable.Add(parserType, parser);
            }
        
        }

        private Parser<T> getParser<T>()
        {
            object pObj = typeTable[typeof(T)];
            return (Parser<T>)pObj;
        }



        private Parser<dynamic> getParser(Type type)
        {
            object pObj = typeTable[type];
            return (Parser<dynamic>)  pObj;
        }




        public static object parse(Type type, string str)
        {
            Parser<dynamic> parser = Instance.getParser(type);
            if (parser != null)
            {
                return parser.parse(str);
            }
            else
            {
                Console.WriteLine("parser not found for " + type);
                if (type.IsValueType)
                {
                    return Activator.CreateInstance(type);
                }
                else return null;
            }

        }

        public static T parse<T>(string str)
        {
            Parser<T> parser = Instance.getParser<T>();
            if (parser != null)
            {
                return parser.parse(str);
            }
            else
            {
                Console.WriteLine("parser not found for " + typeof(T));
                return default(T);
            }
        }

        

        public static string toCode<T>(T value)
        {
            Parser<T> parser = Instance.getParser<T>();
            if (parser != null)
            {
                return parser.toCode(value);
            }
            else
            {
                Console.WriteLine("parser not found for " + typeof(T));
                return "";
            }
        }

        public static string toCode(object value)
        {
            Parser<dynamic> parser = Instance.getParser(value.GetType());
            if (parser != null)
            {
                return parser.toCode(value);
            }
            else
            {
                Console.WriteLine("parser not found for " + value.GetType());
                return "";
            }
        }


    }

    abstract class Parser <T>
    {
        protected BinaryFormatter binFormer;
        protected BufferedStream binStream;

        private T defaultValue;
        public T DefaultValue { get { return defaultValue; } }


        protected Parser()
        {
            binFormer = new BinaryFormatter();
            binStream = new BufferedStream(new MemoryStream());
            
            defaultValue = getDefaultValue();
        }



        protected abstract T toValue(string str);
        protected abstract string toString(T value);
        protected abstract string code(T value);
        protected abstract T getDefaultValue();

        protected void resetStream()
        {
            binStream = new BufferedStream(new MemoryStream());
        }

        protected string readStream()
        {
            int b = 0;
            string streamText = "";
            binStream.Seek(0, SeekOrigin.Begin);
            while (-1 != (b = binStream.ReadByte()))
            {
                streamText += (char)b;
            }
            return streamText;
            
        }

        protected void writeStream(string text)
        {
            binStream.Seek(0, SeekOrigin.Begin);
            foreach (char c in text.ToCharArray()){
                binStream.WriteByte((byte)c);
            }
            binStream.Flush();
        }

        public T parse(string str)
        {
            try
            {
                return toValue(str);
            }
            catch (Exception e)
            {
                return defaultValue;
            }
        }

        public string deparse(T value)
        {
            try
            {
                return toString(value);
            }
            catch (Exception e)
            {
                return value.ToString();
            }
        }

        public string toCode(T value)
        {
            try
            {
                return code(value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
