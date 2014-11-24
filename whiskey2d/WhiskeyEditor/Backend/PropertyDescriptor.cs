using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    public delegate void PropertyAddedEventHandler(object sender, PropertyChangeEventArgs args);
    public delegate void PropertyRemovedEventHandler(object sender, PropertyChangeEventArgs args);
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangeEventArgs args);
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

        public PropertyDescriptor(string name, TypeVal typeVal)
            : this(false, name, typeVal)
        {
        }

        public PropertyDescriptor(bool secure, string name, TypeVal typeVal){
            this.name = name;
            this.typeVal = typeVal;
            this.secure = secure;
        }

        public bool Secure
        {
            get { return secure; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public TypeVal TypeVal
        {
            get { return typeVal; }
        }

        public PropertyDescriptor clone()
        {
            return new PropertyDescriptor(secure, name, typeVal.clone());
        }

        public string toCodeDefinition()
        {
            return "public " + typeVal.TypeName + " " + name + " { get; set; } ";
        }

    }
}
