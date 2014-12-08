using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;


namespace WhiskeyEditor.UI.Properties.TypeConverters
{
    public class StructTypeConverter<T> : ExpandableObjectConverter where T : struct
    {
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(String))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (ConverterManager.Instance.hasConverter(typeof(T)))
                {
                    return ConverterManager.Instance.convertFromString(typeof(T), (string)value);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (ConverterManager.Instance.hasConverter(typeof(T)))
                {
                    return ConverterManager.Instance.convertToString(value);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            T ret = default(T);
            object boxed = ret;
            foreach (DictionaryEntry entry in propertyValues)
            {
                PropertyInfo pi = ret.GetType().GetProperty(entry.Key.ToString());
                if ((pi != null) && (pi.CanWrite))
                {
                    pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
                }
            }
            return (T)boxed;
        }
    }
}
