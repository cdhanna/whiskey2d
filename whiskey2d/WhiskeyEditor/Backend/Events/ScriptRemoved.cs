using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallMVC;

namespace WhiskeyEditor.Backend.Events
{
    class ScriptRemoved : Event
    {
        private string scriptName;
        public ScriptRemoved(string scriptName)
        {
            this.scriptName = scriptName;
        }
        public string ScriptName { get { return scriptName; } }

    }
}
