using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend.Managers
{
    class ScriptManager
    {

        private static ScriptManager instance = new ScriptManager();
        public static ScriptManager Instance { get { return instance; } }

        private ScriptManager()
        {
            scriptTable = new Dictionary<string, ScriptDescriptor>();
        }

        private Dictionary<String, ScriptDescriptor> scriptTable;

        public void addScript(ScriptDescriptor sDesc)
        {
            scriptTable.Add(sDesc.Name, sDesc);
        }

        public Dictionary<String, ScriptDescriptor> getScriptTable()
        {
            return scriptTable;
        }

        public ScriptDescriptor lookUpScript(string name)
        {
            return scriptTable[name];
        }


    }
}
