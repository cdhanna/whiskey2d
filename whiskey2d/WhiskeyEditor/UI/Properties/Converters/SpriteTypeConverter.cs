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
    class SpriteTypeConverter : TypeConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Sprite)
            {
                Sprite sprite = (Sprite)value;
                

                if (destinationType == typeof(string))
                {
                    
                    return sprite.ImagePath;

                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
