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
    class WhiskeyPropertyValueTypeConverter : ExpandableObjectConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(String))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {

            if (value.GetType() == typeof(string))
            {
                if (context.Instance is WhiskeyPropertyContainer)
                {
                    WhiskeyPropertyContainer model = (WhiskeyPropertyContainer)context.Instance;
                    Type toType = model.Value.GetType();
                    if (ConverterManager.Instance.hasConverter(toType))
                        return ConverterManager.Instance.convertFromString(toType, (string)value);
                    else
                    {
                        try
                        {
                            object converted = Convert.ChangeType(value, toType);
                            return converted;
                        }
                        catch (Exception e)
                        {
                            return model.Value;
                        }
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string))
            {

                TypeConverter converter = WhiskeyTypeConverters.lookUp(value.GetType().Name);
                if (converter != null)
                {
                    return converter.ConvertTo(context, culture, value, destinationType);
                }


                if (ConverterManager.Instance.hasConverter(value.GetType()))
                {
                    return ConverterManager.Instance.convertToString(value);
                }
                else
                {
                    try
                    {
                        return Convert.ChangeType(value, destinationType);
                    }
                    catch (Exception e)
                    {
                        return value.ToString();
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value is WhiskeyEditor.Backend.InstanceDescriptor)
            {
                return new PropertyDescriptorCollection(new PropertyDescriptor[] { }); //don't return anything for sub properties
            }
            return base.GetProperties(context, value, attributes);
        }

    }
}
