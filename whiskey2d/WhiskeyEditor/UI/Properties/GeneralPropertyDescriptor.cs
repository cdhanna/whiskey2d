using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WhiskeyEditor.UI.Properties
{

    /// <summary>
    /// Simple implementation of Property Descriptor. Provides ways to change values of normaly read-only properties
    /// </summary>
    class GeneralPropertyDescriptor : PropertyDescriptor , IComparable<PropertyDescriptor>
    {

        public event EventHandler ValueChanged = new EventHandler((s, a) => { });

        public object PropValue { get; set; }
        public string PropDisplayName { get; set; }
        public string PropDescription { get; set; }
        public string PropCategory { get; set; }
        public bool PropIsReadOnly { get; set; }
        //public GeneralPropertyDescriptor(string name, string category)
        //    : base(name, new Attribute[] { new CategoryAttribute(category) })
        //{
        //    PropDisplayName = name;
        //}

        public GeneralPropertyDescriptor(string name) : base(name,null)
        {
            
            PropDisplayName = name;
        }
        
        
        public override string Description
        {
            get
            {
                return PropDisplayName;
            }
        }

        public override string DisplayName
        {
            get
            {
                return PropDisplayName;
            }
        }

        public override string Category
        {
            get
            {
                return PropCategory;
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            
            //if (PropertyType == typeof(WhiskeyPropertyContainer))
            //{
            //    WhiskeyPropertyContainer container = (WhiskeyPropertyContainer)PropValue;
            //    if (container.Value != null && container.Value.GetType() == typeof(Whiskey2D.Core.Color))
            //    {
            //        return new ColorPicker();
            //    }

                
            //}

            return base.GetEditor(editorBaseType);
        }

        public override Type PropertyType
        {
            get { return PropValue.GetType(); }
        }

        public override void SetValue(object component, object value)
        {
            PropValue = value;
            ValueChanged(this, new EventArgs());
        }

        public override object GetValue(object component)
        {
            return PropValue;
        }

        public override bool IsReadOnly
        {
            get { return PropIsReadOnly; }
            
        }

        public override Type ComponentType
        {
            get { return null; }
            
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public int CompareTo(PropertyDescriptor other)
        {
            return PropDisplayName.CompareTo(other.DisplayName);
        }
    }
}
