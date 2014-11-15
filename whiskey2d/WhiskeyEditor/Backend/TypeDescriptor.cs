using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;


namespace WhiskeyEditor.Backend
{
    class TypeDescriptor : FileDescriptor 
    {

        private List<PropertyDescriptor> propDescs;
        private List<String> scriptNames;

        public TypeDescriptor(string filePath, string name)
            : base(filePath, name)
        {
            scriptNames = new List<String>();
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


        public void addScript(String scriptName)
        {           
            scriptNames.Add(scriptName);
        }
        public void removeScript(String scriptName)
        {
            if (scriptNames.Contains(scriptName))
            {
                scriptNames.Remove(scriptName);
            }
            else throw new WhiskeyException("Script Not Found : " + scriptName);
        }



        public List<PropertyDescriptor> getPropertySetClone()
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor propDesc in propDescs)
            {
                props.Add(propDesc.clone());
            }
            return props;
        }

        public List<String> getScriptNamesClone()
        {

            List<String> names = new List<string>();
            foreach (String name in scriptNames)
            {
                names.Add(name);
            }
            return names;
        }

    }
}
