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
    using WhiskeyColor = Whiskey2D.Core.Color;
    using Light = Whiskey2D.Core.Light;

    class LightTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Light)
            {
                Light light = (Light)value;


                if (destinationType == typeof(string))
                {

                    return light.Color.R + ", " + light.Color.G + ", " + light.Color.B + ", " + light.Color.A;

                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
