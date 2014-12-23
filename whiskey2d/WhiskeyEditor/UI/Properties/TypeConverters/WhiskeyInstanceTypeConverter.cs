using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using WhiskeyEditor.UI.Properties.Converters;


namespace WhiskeyEditor.UI.Properties.TypeConverters
{

    using WhiskeyInstance = WhiskeyEditor.Backend.InstanceDescriptor;

    class WhiskeyInstanceTypeConverter : TypeConverter
    {

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            //return base.GetProperties(context, value, attributes);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is WhiskeyInstance)
                {
                    WhiskeyInstance inst = (WhiskeyInstance)value;
                    return inst.Name;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }


    }
}
