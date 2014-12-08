using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using WhiskeyEditor.UI.Properties.TypeConverters;

namespace WhiskeyEditor.UI.Properties
{
    using WhiskeyProperty = WhiskeyEditor.Backend.PropertyDescriptor;

    delegate void WhiskeyPropertyEventHandler(object sender, WhiskeyPropertyEventArgs args);
    class WhiskeyPropertyEventArgs : EventArgs
    {
        public WhiskeyProperty WhiskeyProperty { get; private set; }
        public WhiskeyPropertyEventArgs(WhiskeyProperty prop)
        {
            WhiskeyProperty = prop;
        }
    }

    [TypeConverter(typeof(WhiskeyPropertyContainerTypeConverter))]
    class WhiskeyPropertyContainerSecured : WhiskeyPropertyContainer
    {

        public WhiskeyPropertyContainerSecured(WhiskeyProperty prop)
            : base(prop)
        {
        }


        [ReadOnly(true)]
        public override string TypeName
        {
            get
            {
                return base.TypeName;
            }
            set
            {
                base.TypeName = value;
            }
        }

        [ReadOnly(true)]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

    }



    [TypeConverter(typeof(WhiskeyPropertyContainerTypeConverter))]
    class WhiskeyPropertyContainer : IComparable<WhiskeyPropertyContainer>
    {
        /// <summary>
        /// An event that fires anytime any of the properties are changed
        /// </summary>
        public event WhiskeyPropertyEventHandler Changed = new WhiskeyPropertyEventHandler((s, a) => { });

        /// <summary>
        /// A whiskeyProperty to mirror
        /// </summary>
        protected WhiskeyProperty WhiskeyProperty { get; private set; }
        
        
        public WhiskeyPropertyContainer(WhiskeyProperty prop)
        {
            WhiskeyProperty = prop;

            Name = prop.Name;
            //testTypeName = "poop";
            //TypeName = prop.TypeVal.TypeName;
            Value = prop.TypeVal.Value;

            //prop.TypeVal.ValueChanged += (s, a) =>
            //{
            //    Changed(this, new WhiskeyPropertyEventArgs(WhiskeyProperty));
            //};
        }

        /// <summary>
        /// The name of the property
        /// </summary>
        public virtual string Name
        {
            get
            {
                return WhiskeyProperty.Name;
            }
            set
            {
                WhiskeyProperty.Name = value;
                Changed(this, new WhiskeyPropertyEventArgs(WhiskeyProperty));
            }
        }

        /// <summary>
        /// The type name of the property
        /// </summary>
        //[TypeConverter(typeof(StringSelectorConverter))]
        [Editor(typeof(TypePicker), typeof(System.Drawing.Design.UITypeEditor))]
        public virtual string TypeName
        {
            get
            {
                return WhiskeyProperty.TypeVal.TypeName;
                //return WhiskeyProperty.TypeName;
            }
            set
            {
                
                WhiskeyEditor.Backend.TypeVal typeVal = WhiskeyEditor.Backend.TypeNameBank.Instance.createTypeVal(value);

                object val = null;
                try
                {
                    if (typeVal is WhiskeyEditor.Backend.RealType )
                    {
                        val = Convert.ChangeType(Value, ((WhiskeyEditor.Backend.RealType)typeVal).Type);
                    }
                }
                catch (Exception e)
                {
                    
                }

                WhiskeyProperty.TypeVal = typeVal;
                if (val != null)
                {
                    Value = val;
                }


                Changed(this, new WhiskeyPropertyEventArgs(WhiskeyProperty));
            }
        }

       
        /// <summary>
        /// The value of the property
        /// </summary>
        [TypeConverter(typeof(WhiskeyPropertyValueTypeConverter))]
        public object Value
        {
            get
            {
                return WhiskeyProperty.TypeVal.Value;
            }
            set
            {
                WhiskeyProperty.TypeVal.Value = value;
                Changed(this, new WhiskeyPropertyEventArgs(WhiskeyProperty));
            }
        }





        public int CompareTo(WhiskeyPropertyContainer other)
        {
            return Name.CompareTo(other.Name);
        }
    }

}
