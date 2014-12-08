using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Properties;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class AddPropertyAction : AbstractAction
    {

        private TypeDescriptor desc;
        private PropertyDescriptorListEditor editor;

        public AddPropertyAction(TypeDescriptor desc, PropertyDescriptorListEditor editor)
            : base("Add Property", WhiskeyEditor.UI.Assets.AssetManager.ICON_PLUS)
        {
            this.editor = editor;
            this.desc = desc;
        }

        protected override void run()
        {

            string uniqueName = "Unnamed" + desc.getPropertySet().Count; //TODO make a better unique name generator

            PropertyDescriptor prop = new PropertyDescriptor(uniqueName, new RealType(typeof(int), 0));
            desc.addPropertyDescriptor(prop);
            editor.Invoke(new NoArgFunction(() =>
            {
                editor.PropertyList = desc.getPropertySet();
                desc.ensureFileExists();
            }));
        }
    }
}
