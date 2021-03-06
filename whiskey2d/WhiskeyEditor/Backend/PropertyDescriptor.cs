﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    public delegate void PropertyAddedEventHandler(object sender, PropertyChangeEventArgs args);
    public delegate void PropertyRemovedEventHandler(object sender, PropertyChangeEventArgs args);
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangeEventArgs args);
    public delegate void PropertyEventHandler(object sender, PropertyChangeEventArgs args);

    public class PropertyChangeEventArgs : EventArgs
    {
        private PropertyDescriptor prop;
        private String propName;

        public PropertyDescriptor Property { get { return prop; }}
        public String PropertyName { get { return propName; } }
        public PropertyChangeEventArgs(String propName, PropertyDescriptor prop)
        {
            this.propName = propName;
            this.prop = prop;
        }
    }

    [Serializable]
    public class PropertyDescriptor 
    {

        private string name;
        private TypeVal typeVal;
        private bool secure;
        private bool visible;
        private string category;

        public event PropertyChangedEventHandler TypeValChanged = new PropertyChangedEventHandler((s, a) => { });
        

        public PropertyDescriptor(string name, TypeVal typeVal)
            : this(false, name, typeVal)
        {
        }

        

        public PropertyDescriptor(bool secure, string name, TypeVal typeVal)
            : this(secure, true, name, typeVal)
        {
        }

        public PropertyDescriptor(bool secure, bool visible, string name, TypeVal typeVal)
        {
            this.name = name;
            this.TypeVal = typeVal;
            this.secure = secure;
            this.category = "Base Properties";
            this.visible = visible;
        }

        public bool Visible { get { return visible; } set { visible = value; } }

        public bool Secure
        {
            get { return secure; }
        }

        //public Guid Id
        //{
        //    get { return id; }
            
        //}

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (TypeValChanged != null)
                    TypeValChanged(this, new PropertyChangeEventArgs(Name, this));
            }

        }

      
        public TypeVal TypeVal
        {
            get { return typeVal; }
            set
            {
              

                typeVal = value;
                
                if (TypeValChanged != null)
                    TypeValChanged(this, new PropertyChangeEventArgs(Name, this));
            }
        }

        public string Category
        {
            get { return category != null ? category : "Base Properties"; }
            set { category = value; }
        }

        public PropertyDescriptor clone()
        {
            // new PropertyDescriptor(true, true, name, typeVal.clone());
            return clone(true);
        }
        public PropertyDescriptor clone(bool secure)
        {
            PropertyDescriptor pd = new PropertyDescriptor(secure, true, name, typeVal.clone());
            pd.Category = Category;
            return pd;
        }

        public string toCodeDefinition()
        {
            return "public " + typeVal.TypeName + " " + name + " { get; set; } ";
        }

    }
}
