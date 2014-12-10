using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
namespace WhiskeyEditor.UI.Properties.Editors
{
    class AddRemoveTypeScriptsAction : EditScriptsAction
    {

        public TypeDescriptor Descriptor { get; private set; }
        public TypeDescriptorPropertyEditor Editor { get; private set; }

        public AddRemoveTypeScriptsAction(TypeDescriptor descriptor, TypeDescriptorPropertyEditor editor)
        {
            Descriptor = descriptor;
            Editor = editor;
        }

        protected override void updateScripts(bool accepted, List<string> newScriptNames)
        {
            if (accepted)
            {
                Descriptor.getScriptNames().Clear();
                newScriptNames.ForEach((name) => { Descriptor.getScriptNames().Add(name); });
                Descriptor.ensureFileExists();
                WhiskeyEditor.Backend.Managers.InstanceManager.Instance.syncTypeToInstances(Descriptor);
                Editor.Refresh();
            }
        }

        protected override List<string> getCurrentScriptNames()
        {
            return Descriptor.getScriptNamesClone();
        }

        protected override string getTypeDescriptorName()
        {
            return Descriptor.ClassName;
        }
    }
}
