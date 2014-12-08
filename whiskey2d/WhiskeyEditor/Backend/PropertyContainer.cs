using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace WhiskeyEditor.Backend
{
    class PropertyContainer : DynamicObject
    {
        private readonly IDictionary<string, object> dynamicProperties = 
        new Dictionary<string, object>();

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        var memberName = binder.Name;
        return dynamicProperties.TryGetValue(memberName, out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        var memberName = binder.Name;
        dynamicProperties[memberName] = value;
        return true;
    }
        //public dynamic convertToDynamic(TypeDescriptor data)
        //{
        //    dynamic obj = new ExpandoObject();

        //    List<PropertyDescriptor> props = data.getPropertySetClone();

        //    foreach (PropertyDescriptor prop in props)
        //    {
        //        ((IDictionary<string, object>)obj)[prop.Name] = prop.TypeVal.Value;
                
        //    }

        //    return obj;
        //}

        //public TypeDescriptor convertFromDynamic(dynamic obj, TypeDescriptor tDesc)
        //{
        //    IDictionary<string, object> map = ((IDictionary<string, object>)obj);
        //    foreach (string propName in map.Keys)
        //    {
        //        tDesc.setPropertyValue(propName, map[propName]);
        //    }

        //    return tDesc;
        //}


    }
}
