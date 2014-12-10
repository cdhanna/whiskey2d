using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class AddRemoveInstanceScriptsAction : EditScriptsAction
    {

        public InstanceDescriptor Descriptor { get; private set; }
        public InstanceDescriptorPropertyEditor Editor { get; private set; }


        public AddRemoveInstanceScriptsAction(InstanceDescriptor descriptor, InstanceDescriptorPropertyEditor editor)
            : base()
        {
            Editor = editor;
            Descriptor = descriptor;
        }


        protected override void updateScripts(bool accepted, List<string> newScriptNames)
        {
            if (accepted)
            {
                Descriptor.clearScripts();
                newScriptNames.ForEach((name) => { Descriptor.addScript(name); });
                Editor.Refresh();
            }
        }

        protected override List<string> getCurrentScriptNames()
        {
            return Descriptor.getScriptNames();
        }

        protected override string getTypeDescriptorName()
        {
            return Descriptor.TypeDescriptorInFileManager.ClassName;
        }
    }
}
