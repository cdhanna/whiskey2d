using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;


namespace WhiskeyEditor.Backend
{
    class TypeDescriptor : FileDescriptor 
    {

        private List<PropertyDescriptor> propDescs;
        private List<ScriptDescriptor> scriptDescs;

        public TypeDescriptor(string filePath, string name)
            : base(filePath, name)
        {
            scriptDescs = new List<ScriptDescriptor>();
            propDescs = new List<PropertyDescriptor>();

            addInitialProps();
            

        }

        public String ClassName
        {
            get { return Name; }
        }

        private void addInitialProps()
        {
            addPropertyDescriptor(new PropertyDescriptor(true, "X", new RealType(typeof(float), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "Y", new RealType(typeof(float), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "ID", new RealType(typeof(int), 0)));
            addPropertyDescriptor(new PropertyDescriptor(true, "Sprite", new RealType(typeof(Sprite), new Sprite())));
        }


        public void addPropertyDescriptor(PropertyDescriptor propDesc)
        {
            if (0 == propDescs.Where((p) => p.Name.Equals(propDesc.Name)).ToList().Count)
            {
                propDescs.Add(propDesc);
            }
            else throw new WhiskeyException("Property Already Exists : " + propDesc.Name);
        }
        public void removePropertyDescriptor(PropertyDescriptor propDesc)
        {
            if (propDescs.Contains(propDesc))
            {
                if (!propDesc.Secure)
                {
                    propDescs.Remove(propDesc);
                }
                else throw new WhiskeyException("Property Is Secure : " + propDesc.Name);
            }
            else throw new WhiskeyException("Property Not Found : " + propDesc.Name);
        }


        public void addScriptDescriptor(ScriptDescriptor scriptDesc)
        {           
            scriptDescs.Add(scriptDesc);
        }
        public void removeScriptDescriptor(ScriptDescriptor scriptDesc)
        {
            if (scriptDescs.Contains(scriptDesc))
            {
                scriptDescs.Remove(scriptDesc);
            }
            else throw new WhiskeyException("Script Not Found : " + scriptDesc.Name);
        }


    }
}
