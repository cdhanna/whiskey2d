using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.ClassLoader
{
    public class PropertyDescriptor
    {

        public string Name { get; set; }
        public Type Type { get; set; }
        public Object Value { get; set; }

        public PropertyDescriptor(string name, Type type, Object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

    }
}
