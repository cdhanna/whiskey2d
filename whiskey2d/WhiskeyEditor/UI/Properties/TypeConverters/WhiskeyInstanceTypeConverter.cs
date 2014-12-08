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
    class WhiskeyInstanceTypeConverter : TypeConverter
    {

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            //return base.GetProperties(context, value, attributes);
        }

    }
}
