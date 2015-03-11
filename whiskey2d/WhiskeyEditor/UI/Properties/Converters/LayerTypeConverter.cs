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

using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Converters
{

    using CoreLayer = Whiskey2D.Core.Layer;

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
            if (value is CoreLayer)
            {
                CoreLayer layer = (CoreLayer)value;


                if (destinationType == typeof(string))
                {

                    return layer.Name;

                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
