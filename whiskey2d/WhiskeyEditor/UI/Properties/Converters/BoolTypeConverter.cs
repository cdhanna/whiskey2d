using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;
using System.Globalization;
using Whiskey2D.Core;
namespace WhiskeyEditor.UI.Properties.Converters
{
    class BoolTypeConverter : TypeConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string str = (string)value;
                bool result = false;
                Boolean.TryParse(str, out result);
                return result;

            }
            
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Boolean || value is bool)
            {
                Boolean b = (Boolean)value;


                if (destinationType == typeof(string))
                {

                    if (b)
                    {
                        return "true";
                    }
                    else return "false";

                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
