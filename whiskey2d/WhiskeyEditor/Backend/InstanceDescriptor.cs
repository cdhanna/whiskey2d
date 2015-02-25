using System;
using System.Collections.Generic;
using System.Linq;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor;
using WhiskeyEditor.EditorObjects;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class InstanceDescriptor : EditorGameObject, Descriptor
    {
        public const string PROP_X = "X";
        public const string PROP_Y = "Y";
        public const string PROP_SPRITE = "Sprite";
        public const string PROP_NAME = "Name";
        public const string PROP_ACTIVE = "Active";
        public const string PROP_ISDEBUG = "IsDebug";
        public const string PROP_LIGHT = "Light";
        public const string PROP_SHADOWS = "Shadows";
        public const string PROP_HUD = "HudObject";
        private bool initialized;
        private TypeDescriptor typeDesc;
        private List<PropertyDescriptor> propDescs;
        private List<String> scriptNames;
       




        public InstanceDescriptor(TypeDescriptor typeDesc, ObjectManager manager )
            : base(manager)
        {
            propDescs = new List<PropertyDescriptor>();
            scriptNames = new List<string>();

            initialize(typeDesc);
        }

        public InstanceDescriptor(ObjectManager manager)
            : base(manager)
        {
            propDescs = new List<PropertyDescriptor>();
            scriptNames = new List<string>();

            initialized = false;
        }


        public TypeDescriptor TypeDescriptorInFileManager { get { return FileManager.Instance.lookUpFileByName<TypeDescriptor>(typeDesc.Name); } }
        public TypeDescriptor TypeDescriptorCompile { get { return typeDesc; } }

        public void updateTypeDescriptor()
        {
            typeDesc = TypeDescriptorInFileManager;
        }


        public void registerListeners()
        {
            //TypeDescriptor.PropertyAdded += new PropertyAddedEventHandler(propertyAddedToType);
            //TypeDescriptor.PropertyChanged += new PropertyChangedEventHandler(propertyChangedInType);
            //TypeDescriptor.PropertyRemoved += new PropertyRemovedEventHandler(propertyRemovedFromType);

            TypeDescriptorInFileManager.ScriptAdded += new ScriptAddedEventHandler(scriptAddedInType);
            TypeDescriptorInFileManager.ScriptRemoved += new ScriptRemovedEventHandler(scriptRemovedFromType);


            lookUpPropertyDescriptor(PROP_X).TypeVal.ValueChanged += (s, a) =>
            {
                base.X = (float)lookUpPropertyDescriptor(PROP_X).TypeVal.Value;
            };
            lookUpPropertyDescriptor(PROP_Y).TypeVal.ValueChanged += (s, a) =>
            {
                base.Y = (float)lookUpPropertyDescriptor(PROP_Y).TypeVal.Value;
            };
        }

        public void initialize(TypeDescriptor typeDesc)
        {
            initialized = true;
            this.typeDesc = typeDesc;
            Layer = WhiskeyEditor.MonoHelp.WhiskeyControl.Active.Level.getLayer("Default");
           
            propDescs = typeDesc.getPropertySetClone();

            scriptNames = typeDesc.getScriptNamesClone();
           // registerListeners();


            lookUpPropertyDescriptor(PROP_X).TypeVal.ValueChanged += (s, a) =>
            {
                base.X = (float)lookUpPropertyDescriptor(PROP_X).TypeVal.Value;
            };
            lookUpPropertyDescriptor(PROP_Y).TypeVal.ValueChanged += (s, a) =>
            {
                base.Y = (float)lookUpPropertyDescriptor(PROP_Y).TypeVal.Value;
            };
            
            

            Name = objectManager.getDefaultNameFor(this);

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

        //private void propertyChangedInType(object sender, PropertyChangeEventArgs args)
        //{
        //    PropertyDescriptor prop = lookUpPropertyDescriptor(args.PropertyName);
        //    prop.Name = args.Property.Name;
            
            
        //    //propDescs.Remove(prop);
        //    //propDescs.Add(args.Property.clone());
            
        //}

        //private void propertyAddedToType(object sender, PropertyChangeEventArgs args)
        //{
        //    propDescs.Add(args.Property.clone());
        //}

        //private void propertyRemovedFromType(object sender, PropertyChangeEventArgs args)
        //{
        //    List<PropertyDescriptor> badProps = propDescs.Where(p => p.Name.Equals(args.Property.Name)).ToList();
        //    badProps.ForEach((p) => { propDescs.Remove(p); });
        //}

        #endregion

        private void initCheck()

        {
            if (!initialized)
            {
                throw new WhiskeyException("Instance not initialized ");
            }
        }

        public void syncType()
        {
            initCheck();

            List<PropertyDescriptor> checkedSet = new List<PropertyDescriptor>();
            propDescs.ForEach((p) => { checkedSet.Add(p); });

            foreach (PropertyDescriptor typeProperty in TypeDescriptorInFileManager.getPropertySet())
            {
                //List<PropertyDescriptor> matches = propDescs.Where(s => s.Id.Equals(typeProperty.Id)).ToList(); //too strict.
                List<PropertyDescriptor> matches = propDescs.Where(s => s.Name.Equals(typeProperty.Name) && s.TypeVal.TypeName.Equals(typeProperty.TypeVal.TypeName)).ToList();

                if (matches.Count == 0)
                {
                    PropertyDescriptor newProp = typeProperty.clone(true);
                    propDescs.Add(newProp);
                }
                else if (matches.Count == 1)
                {
                    PropertyDescriptor prop = matches[0];
                    prop.Name = typeProperty.Name;
                    if (!prop.TypeVal.TypeName.Equals(typeProperty.TypeVal.TypeName))
                    {
                        prop.TypeVal = typeProperty.TypeVal.clone();
                    }
                    checkedSet.Remove(prop);
                }

            }

            checkedSet.ForEach((p) => { propDescs.Remove(p); });


            //get synced!
        }

        public void updateObjectManager(ObjectManager objMan)
        {
            objectManager = objMan;

        }


        public TypeVal getTypeValOfName(string name)
        {
            return lookUpPropertyDescriptor(name).TypeVal;
        }

        public override string getTypeName()
        {
            if (initialized)
                return typeDesc.Name;
            else return "unnamed";
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
        public override void clearScripts()
        {
            scriptNames.Clear();
            base.clearScripts();
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

        public Layer Layer { get; set; }

        
        public override Boolean Active
        {
            get
            {
                if (!initialized)
                {
                    return base.Active;
                }
                else return (Boolean)getTypeValOfName(PROP_ACTIVE).Value;
            }
            set
            {
                base.Active = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_ACTIVE).Value = value;
                }
            }
        }

        public override Boolean HudObject
        {
            get
            {
                if (!initialized)
                {
                    return base.HudObject;
                }
                else return (Boolean)getTypeValOfName(PROP_HUD).Value;
            }
            set
            {
                base.HudObject = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_HUD).Value = value;
                }
            }
        }

        public override Boolean IsDebug
        {
            get
            {
                if (!initialized)
                {
                    return base.IsDebug;
                }
                else return (Boolean)getTypeValOfName(PROP_ISDEBUG).Value;
            }
            set
            {
                base.IsDebug = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_ISDEBUG).Value = value;
                }
            }
        }
        public override ShadowProperties Shadows
        {
            get
            {
                if (!initialized)
                {
                    return base.Shadows;
                }
                else return (ShadowProperties)getTypeValOfName(PROP_SHADOWS).Value;
            }
            set
            {
                base.Shadows = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_SHADOWS).Value = value;
                }
            }
        }
        public override float X
        {
            get
            {
                if (!initialized)
                {
                    return base.X;
                }
                else return (float)getTypeValOfName(PROP_X).Value;
            }
            set
            {
                base.X = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_X).Value = value;
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
                else return (float)getTypeValOfName(PROP_Y).Value;
            }
            set
            {
                base.Y = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_Y).Value = value;
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
                else return (Sprite)getTypeValOfName(PROP_SPRITE).Value;
            }
            set
            {
                base.Sprite = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_SPRITE).Value = value;
                }
 
            }
        }

        public override Light Light
        {
            get
            {
                if (!initialized)
                {
                    return base.Light;
                }
                else return (Light)getTypeValOfName(PROP_LIGHT).Value;
            }
            set
            {
                base.Light = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_LIGHT).Value = value;
                }

            }
        }


        public override string Name
        {
            get
            {
                if (!initialized)
                {
                    return base.Name;
                }
                else return (String)getTypeValOfName(PROP_NAME).Value;
            }
            set
            {
                base.Name = value;
                if (initialized)
                {
                    getTypeValOfName(PROP_NAME).Value = value;
                }

            }
        }

        public List<PropertyDescriptor> getPropertySet()
        {
            return propDescs;
        }

        protected override void addInitialScripts()
        {
            //none at the moment
        }

        

        public InstanceDescriptor clone(ObjectManager objectManager)
        {
            InstanceDescriptor inst = new InstanceDescriptor(TypeDescriptorInFileManager, objectManager);
            string name = inst.Name;

            inst.X = X;
            inst.Y = Y;
            inst.Light = new Light(Light);
            inst.Sprite = new Sprite(Sprite.getRenderer(), Sprite.getResources(), Sprite);
            inst.Layer = Layer;
            inst.IsDebug = IsDebug;
            inst.Shadows = Shadows;
            inst.HudObject = HudObject;
            for (int i = 0 ; i < getPropertySet().Count ; i ++)
            {
                inst.propDescs[i] = lookUpPropertyDescriptor(propDescs[i].Name).clone();
            }
            foreach (string scriptName in getScriptNames())
            {
                inst.addScript(scriptName);
            }

            inst.Name = name;

            return inst;
        }

    }
}
