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
using Microsoft.Xna.Framework.Input;
using WhiskeyEditor.Backend;


namespace WhiskeyEditor.UI.Properties.Converters
{
    using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

    class KeyConverter : TypeConverter
    {
       
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) || destinationType == typeof(String))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

      


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is XnaKeys && value is XnaKeys)
            {
                XnaKeys k = (XnaKeys)value;


                if (destinationType == typeof(string))
                {

                    return k.ToString();

                }
            }

            return XnaKeys.None;
        }


    }
}
