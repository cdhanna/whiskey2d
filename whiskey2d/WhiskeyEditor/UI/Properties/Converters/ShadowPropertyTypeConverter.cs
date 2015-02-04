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
    class ShadowPropertyTypeConverter : TypeConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

       
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ShadowProperties || value is bool)
            {
                ShadowProperties b = (ShadowProperties)value;


                if (destinationType == typeof(string))
                {

                    if (b.CastsShadows)
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
