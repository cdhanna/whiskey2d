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

namespace WhiskeyEditor.UI.Properties.TypeConverters
{
    using WhiskeyProperty = WhiskeyEditor.Backend.PropertyDescriptor;
    class WhiskeyPropertyContainerTypeConverter : ExpandableObjectConverter
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
                PropertyAdapter adapter = (PropertyAdapter)context.Instance;
                string name = context.PropertyDescriptor.Name;
                GeneralPropertyDescriptor gpd = (GeneralPropertyDescriptor)context.PropertyDescriptor;
                WhiskeyProperty whiskeyProp = adapter.WhiskeyPropertys.Where((w) => { return w.Name.Equals(name); }).ToList()[0];

                Type toType = whiskeyProp.TypeVal.Value.GetType();

                if (ConverterManager.Instance.hasConverter(toType))
                    whiskeyProp.TypeVal.Value = ConverterManager.Instance.convertFromString(toType, (string)value);
                else
                {
                    try
                    {
                        whiskeyProp.TypeVal.Value = Convert.ChangeType(value, toType);
                    }
                    catch (Exception e)
                    {
                        //???
                    }
                }
                return gpd.PropValue;
            }


            return base.ConvertFrom(context, culture, value);
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string))
            {
                WhiskeyPropertyContainer container = (WhiskeyPropertyContainer)value;

                TypeConverter converter = WhiskeyTypeConverters.lookUp(container.Value.GetType().Name);
                if (converter != null)
                {
                    object newValue = converter.ConvertTo(context, culture, container.Value, destinationType);

                    

                    return newValue;
                }


                if (ConverterManager.Instance.hasConverter(container.Value.GetType()))
                {
                    return ConverterManager.Instance.convertToString(container.Value);
                }
                else
                {
                    try
                    {
                        return Convert.ChangeType(container.Value, destinationType);
                    }
                    catch (Exception e)
                    {
                        return container.Value.ToString();
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}
