using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class InstanceDescriptorPropertyEditor : PropertyContent
    {

        protected PropertyDescriptorListEditor PropertyGrid { get; private set; }
        protected PropertyAdapter Adapter { get; private set; }
        protected InstanceDescriptor Descriptor { get; private set; }

        public InstanceDescriptorPropertyEditor(InstanceDescriptor desc) : base(desc)
        {
            Title = "Instance Properties";
            Descriptor = desc;

            initControls();
            addControls();

        }

        public override void Refresh()
        {
            PropertyGrid.Refresh();
            base.Refresh();
        }

        private void initControls()
        {
            
            PropertyGrid = new PropertyDescriptorListEditor();
            List<PropertyDescriptor> pList = Descriptor.getPropertySet();
            PropertyGrid.PropertyList = pList;

        }

        private void addControls()
        {
            PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(PropertyGrid);
        }

    }
}
