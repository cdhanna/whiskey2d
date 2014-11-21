using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallMVC;

namespace WhiskeyEditor.Backend.Events
{
    class ScriptAdded : Event
    {
        private string scriptName;

        public ScriptAdded(String scriptName)
        {
            this.scriptName = scriptName;
        }

        public String ScriptName { get { return scriptName; } }

    }

    
}
