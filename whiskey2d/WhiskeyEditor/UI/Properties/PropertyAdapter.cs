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
namespace WhiskeyEditor.UI.Properties
{
    using WhiskeyProperty = WhiskeyEditor.Backend.PropertyDescriptor;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    class PropertyAdapter : ICustomTypeDescriptor
    {

        /// <summary>
        /// A set of WhiskeyProperties to edit
        /// </summary>
        public List<WhiskeyProperty> WhiskeyPropertys { get; private set; }
        
        /// <summary>
        /// The propertygrid that this property adapter belongs to
        /// </summary>
        public PropertyGrid PropertyGrid { get; private set; }


        public List<GeneralPropertyDescriptor> OtherProperties { get; private set; }

        /// <summary>
        /// The most recent set of propertyModels created 
        /// </summary>
        private List<WhiskeyPropertyContainer> LastModelSet { get; set; }


        public event EventHandler PropertyValueChanged = new EventHandler((s, a) => { });

        public PropertyAdapter(List<WhiskeyProperty> props, PropertyGrid grid)
        {
            WhiskeyPropertys = props;
            PropertyGrid = grid;
            LastModelSet = new List<WhiskeyPropertyContainer>();
            OtherProperties = new List<GeneralPropertyDescriptor>();
        }

        #region required by customTypeDesc
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }


        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return WhiskeyPropertys;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        PropertyDescriptorCollection
            System.ComponentModel.ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }
        #endregion



        /// <summary>
        /// Getting a set of properties to give to the propertygrid
        /// </summary>
        /// <param name="attributes">?</param>
        /// <returns>a set of properties for the propertygrid</returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {

            //clear previous property model set
            LastModelSet.Clear();
            List<PropertyDescriptor> properties = new List<PropertyDescriptor>();

            //for every property in our set...
            foreach (WhiskeyProperty prop in WhiskeyPropertys)
            {
                if (prop.Visible)
                {
                    //create a new property descriptor (for the property grid) for it....
                    GeneralPropertyDescriptor gpd = new GeneralPropertyDescriptor(prop.Name);

                    //create a model, that is either secure or unsecure depending on if the WhiskeyProp.Secure is true/false
                    WhiskeyPropertyContainer model;
                    if (prop.Secure)
                    {
                        model = new WhiskeyPropertyContainerSecured(prop);

                        gpd.PropCategory = "Base Properties";

                    }
                    else
                    {
                        model = new WhiskeyPropertyContainer(prop);

                        gpd.PropCategory = "Properties";

                    }

                    //set the value of the propertygrid property descriptor, to our model
                    gpd.PropValue = model;


                    //add a listener that will update the propertygrid when the model changes
                    model.Changed += (s, a) =>
                    {
                        gpd.PropDisplayName = a.WhiskeyProperty.Name;

                        if (PropertyGrid.IsHandleCreated)
                        {

                            //PropertyGrid.BeginInvoke(new NoArgFunction(() => {
                                PropertyGrid.Refresh();
                            //}));
                                PropertyValueChanged(this, new EventArgs());

                        }
                    };

                    //add the model to our set
                    LastModelSet.Add(model);
                    //add the propertygrid descriptor to the set to be returned
                    properties.Add(gpd);
                }
            }

            //converting the set of propertygrid descriptors to a returnable array
            //properties.Sort();
            
            //other
            foreach (PropertyDescriptor prop in OtherProperties)
            {
                properties.Add(prop);
            }
            
            PropertyDescriptor[] props = (PropertyDescriptor[])properties.ToArray();

            return new PropertyDescriptorCollection(props);
        }




    }

    

}
