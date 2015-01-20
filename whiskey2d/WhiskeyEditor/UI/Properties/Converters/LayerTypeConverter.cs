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

using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Converters
{
    class LayerTypeConverter : TypeConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Layer)
            {
                Layer layer = (Layer)value;


                if (destinationType == typeof(string))
                {

                    return layer.Name;

                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
