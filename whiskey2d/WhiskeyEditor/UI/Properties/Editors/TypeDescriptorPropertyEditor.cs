using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class TypeDescriptorPropertyEditor : FileDescriptorPropertyEditor<TypeDescriptor>
    {

        //public TypeDescriptor TypeDescriptor { get; private set; }

        private WhiskeyAction addPropertyAction;
        private List<WhiskeyAction> remPropertyActions;
        ToolStripDropDownButton dropDownBtn;

        private EventHandler dropDownOpeningHandler;
        private PropertyRemovedEventHandler propRemovedHandler;

        protected List<GeneralPropertyDescriptor> ScriptProperties;


        protected EditScriptsAction editScriptsAction;

        public TypeDescriptorPropertyEditor(TypeDescriptor tDesc) : base(tDesc)
        {
            
            Title = "Type Properties";

            ScriptProperties = new List<GeneralPropertyDescriptor>();
            

            initControls();
            addControls();
           
        }


        public void refreshScripts()
        {
            ScriptProperties.ForEach((s) => { WhiskeyPropertyListGrid.removeOtherProperty(s); });
            ScriptProperties.Clear();
            Descriptor.getScriptNames().ForEach((s) =>
            {
                GeneralPropertyDescriptor gpd = WhiskeyPropertyListGrid.addOtherProperty(s, "Scripts", 1);
                gpd.PropIsReadOnly = true;
                ScriptProperties.Add(gpd);
            });
        }

        public override void Refresh()
        {
            refreshScripts();


            WhiskeyPropertyListGrid.Refresh();
            base.Refresh();
        }

        private void initControls()
        {
            WhiskeyPropertyListGrid.PropertyList = Descriptor.getPropertySet();

            remPropertyActions = new List<WhiskeyAction>();
           
            WhiskeyPropertyListGrid.PropertyGrid.PropertyValueChanged += (s, a) =>
            {
                Descriptor.ensureFileExists();
                WhiskeyEditor.Backend.Managers.InstanceManager.Instance.syncTypeToInstances(Descriptor);
            };

            addPropertyAction = new AddPropertyAction(Descriptor, WhiskeyPropertyListGrid);
            ToolStripItems.Add(addPropertyAction.generateControl<ToolStripButton>());


            dropDownBtn = new ToolStripDropDownButton("Remove", Assets.AssetManager.ICON_MINUS);

            
            //updateItems();

           // dropDownBtn.DropDownItems.Add(removeBox);
            dropDownBtn.DropDownDirection = ToolStripDropDownDirection.BelowRight;
            dropDownBtn.DropDown.AutoSize = false;
            dropDownBtn.DropDown.Width = 200;

            dropDownOpeningHandler = new EventHandler(onDropDownOpening);
            propRemovedHandler = new PropertyRemovedEventHandler(onPropRemoved);


            dropDownBtn.DropDownOpening += dropDownOpeningHandler;
           // Descriptor.PropertyRemoved += propRemovedHandler;
            //removeBox.Alignment = ToolStripItemAlignment.Right;
            //removeBox.ImageIndex = 1;
            //removeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ToolStripItems.Add(dropDownBtn);

            editScriptsAction = new AddRemoveTypeScriptsAction(Descriptor, this);
            ToolStripItems.Add(editScriptsAction.generateControl());
           

        }

        protected override void Dispose(bool disposing)
        {
            dropDownBtn.DropDownOpening -= dropDownOpeningHandler;
            //Descriptor.PropertyRemoved -= propRemovedHandler;
            base.Dispose(disposing);
        }

        private void onPropRemoved(object sender, PropertyChangeEventArgs args)
        {
            WhiskeyPropertyListGrid.Refresh();
        }

        private void onDropDownOpening(object sender, EventArgs args)
        {
            dropDownBtn.DropDownItems.Clear();
            remPropertyActions.Clear();
            foreach (PropertyDescriptor prop in Descriptor.getPropertySet())
            {
                if (!prop.Secure)
                {
                    RemovePropertyAction rem = new RemovePropertyAction(Descriptor, prop, WhiskeyPropertyListGrid);
                    remPropertyActions.Add(rem);
                    dropDownBtn.DropDownItems.Add(rem.generateControl<ToolStripButton>());
                }
            }
            //dropDownBtn.DropDown.Width = 300;
           // dropDownBtn.DropDown.
        }

        private void addControls()
        {
            
        }

    }
}
