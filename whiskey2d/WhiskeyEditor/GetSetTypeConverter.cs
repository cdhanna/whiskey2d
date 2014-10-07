using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;


namespace WhiskeyEditor
{
    class GetSetTypeConverter : ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object obj, Attribute[] attributes)
        {

           
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(obj);
            List<PropertyDescriptor> pdList = new List<PropertyDescriptor>();
            PropertyInfo[] propInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo p in propInfos)
            {
                MethodInfo m = p.GetSetMethod();
                if (m != null)
                {



                    PropertyDescriptor pd = pdc[p.Name];
                    pdList.Add(pd);
                    
                    
                    
                }
            }
           
            return new PropertyDescriptorCollection(pdList.ToArray());
        }

      

    }
}
