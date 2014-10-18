using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.ClassLoader
{
    /// <summary>
    /// The property descriptor is responsible for representing all of the data about a property
    /// </summary>
    public class PropertyDescriptor
    {

        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the property
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The value of the property
        /// </summary>
        public Object Value { get; set; }


        /// <summary>
        /// Create a propDescr from an exisitng propDescr. This is the copy-constructor
        /// </summary>
        /// <param name="other">a non-null PropertyDescr</param>
        public PropertyDescriptor(PropertyDescriptor other)
        {
            Name = other.Name;
            Type = other.Type;
            Value = other.Value; //jenky TODO use clone
        }

        /// <summary>
        /// Create a new property descr. 
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="value">The value of the property</param>
        public PropertyDescriptor(string name, Type type, Object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

    }
}
