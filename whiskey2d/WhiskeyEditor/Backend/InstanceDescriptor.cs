using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;

namespace WhiskeyEditor.Backend
{
    class InstanceDescriptor : GameObject
    {
        public const string PROP_X = "X";
        public const string PROP_Y = "Y";
        public const string PROP_SPRITE = "Sprite";


        private bool initialized;
        private TypeDescriptor typeDesc;
        private List<PropertyDescriptor> propDescs;
        private List<String> scriptNames;

        public InstanceDescriptor(TypeDescriptor typeDesc) : base()
        {
            propDescs = new List<PropertyDescriptor>();
            scriptNames = new List<string>();

            initialize(typeDesc);
        }

        public InstanceDescriptor() : base()
        {
            propDescs = new List<PropertyDescriptor>();
            scriptNames = new List<string>();

            initialized = false;
        }


        public TypeDescriptor TypeDescriptor { get { return typeDesc; } }

        public void initialize(TypeDescriptor typeDesc)
        {
            initialized = true;
            this.typeDesc = typeDesc;

            propDescs = typeDesc.getPropertySetClone();
            scriptNames = typeDesc.getScriptNamesClone();

        }
        private void initCheck()
        {
            if (!initialized)
            {
                throw new WhiskeyException("Instance not initialized ");
            }
        }


        public TypeVal getTypeValOfName(string name)
        {
            List<PropertyDescriptor> props = propDescs.Where(p => p.Name.Equals(name)).ToList();
            if (props.Count == 1)
            {
                return props[0].TypeVal;
            }
            else throw new WhiskeyException("Property Name : " + name + " had " + props.Count + " instances.");
        }

        public void addScript(String scriptName)
        {
            initCheck();
            scriptNames.Add(scriptName);
        }
        public void removeScript(String scriptName)
        {
            initCheck();
            if (scriptNames.Contains(scriptName))
            {
                scriptNames.Remove(scriptName);
            }
            else throw new WhiskeyException("Script Not Found : " + scriptName);
        }


        public override float X
        {
            get
            {
                if (!initialized)
                {
                    return base.X;
                }
                else return (float)getTypeValOfName(PROP_X).value;
            }
            set
            {
                base.X = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_X).value = value;
                }
            }
        }

        public override float Y
        {
            get
            {
                if (!initialized)
                {
                    return base.Y;
                }
                else return (float)getTypeValOfName(PROP_Y).value;
            }
            set
            {
                base.Y = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_Y).value = value;
                }
            }
        }

        public override Sprite Sprite
        {
            get
            {
                if (!initialized)
                {
                    return base.Sprite;
                }
                else return (Sprite)getTypeValOfName(PROP_SPRITE).value;
            }
            set
            {
                base.Sprite = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_SPRITE).value = value;
                }
            }
        }

        protected override void addInitialScripts()
        {
            //none at the moment
        }
    }
}
