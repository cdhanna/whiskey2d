using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    [Serializable]
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
            InstanceManager.Instance.addInstance(this);
            propDescs = typeDesc.getPropertySetClone();
            scriptNames = typeDesc.getScriptNamesClone();

            typeDesc.PropertyAdded += new PropertyAddedEventHandler(propertyAddedToType);
            typeDesc.PropertyChanged += new PropertyChangedEventHandler(propertyChangedInType);
            typeDesc.PropertyRemoved += new PropertyRemovedEventHandler(propertyRemovedFromType);

            typeDesc.ScriptAdded += new ScriptAddedEventHandler(scriptAddedInType);
            typeDesc.ScriptRemoved += new ScriptRemovedEventHandler(scriptRemovedFromType);

        }

        #region Event Handler Code

        private void scriptAddedInType(object sender, ScriptChangedEventArgs args)
        {
            scriptNames.Add(args.Script.Name);
        }
        private void scriptRemovedFromType(object sender, ScriptChangedEventArgs args)
        {
            scriptNames.Remove(args.Script.Name);
        }

        private void propertyChangedInType(object sender, PropertyChangeEventArgs args)
        {
            PropertyDescriptor prop = lookUpPropertyDescriptor(args.PropertyName);
            prop.Name = args.Property.Name;
            propDescs.Remove(prop);
            propDescs.Add(args.Property.clone());
            
        }

        private void propertyAddedToType(object sender, PropertyChangeEventArgs args)
        {
            propDescs.Add(args.Property.clone());
        }

        private void propertyRemovedFromType(object sender, PropertyChangeEventArgs args)
        {
            List<PropertyDescriptor> badProps = propDescs.Where(p => p.Name.Equals(args.Property.Name)).ToList();
            badProps.ForEach((p) => { propDescs.Remove(p); });
        }

        #endregion

        private void initCheck()

        {
            if (!initialized)
            {
                throw new WhiskeyException("Instance not initialized ");
            }
        }


        public TypeVal getTypeValOfName(string name)
        {
            return lookUpPropertyDescriptor(name).TypeVal;
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

        private PropertyDescriptor lookUpPropertyDescriptor(String name)
        {
            List<PropertyDescriptor> props = propDescs.Where(p => p.Name.Equals(name)).ToList();
            if (props.Count == 1)
            {
                return props[0];
            }
            else throw new WhiskeyException("Property Name : " + name + " had " + props.Count + " instances.");
        }

        public List<String> getScriptNames()
        {
            
            return scriptNames;
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
