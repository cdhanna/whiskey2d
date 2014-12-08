using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WhiskeyEditor.UI.Properties.Converters
{
    abstract class Converter
    {

        public Converter()
        {

        }

        public abstract Type getConvertType();
        public abstract string convertToString(object obj);
        public abstract object convertFromString(string str);

        protected int bound(int x)
        {
            return Math.Max(0, Math.Min(x, 255));
        }

        protected int parseInt(string s)
        {
            try
            {
                return Int32.Parse(s);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //public abstract object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType);
        
    }

    abstract class Converter<C> : Converter
    {

        public override Type getConvertType()
        {
            return typeof(C);
        }

        public override string convertToString(object obj)
        {
            return convertToString((C)obj);
        }
        public override object convertFromString(string str)
        {
            return convertFromString_(str);
        }

        public abstract new string convertToString(C obj);
        public abstract new C convertFromString_(string str);
    }

}
