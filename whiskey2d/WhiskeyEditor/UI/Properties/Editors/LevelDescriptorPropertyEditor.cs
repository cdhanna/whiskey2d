using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class LevelDescriptorPropertyEditor : FileDescriptorPropertyEditor<LevelDescriptor>
    {

        private GeneralPropertyDescriptor ColorDesc;

        public LevelDescriptorPropertyEditor(LevelDescriptor lDesc)
            : base(lDesc)
        {
            
            Title = "Level Properties";

          

            initControls();
            addControls();
        }


        private void initControls()
        {
            //PropertyGrid.PropertyList = Descriptor.getPropertySet();

            
            ColorDesc = WhiskeyPropertyListGrid.addOtherProperty("Color", "Details", Descriptor.Color);

            ColorDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Color = (Whiskey2D.Core.Color) ColorDesc.PropValue;
            };


            WhiskeyPropertyListGrid.PropertyGrid.PropertyValueChanged += (s, a) =>
            {
               // Descriptor.ensureFileExists();
            };
        }

        private void addControls()
        {
            
        }

    }
}
