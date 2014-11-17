using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    class ScriptDescriptor : FileDescriptor
    {

        private String typeName;

        public ScriptDescriptor(string name, string typeName)
            : base(name)
        {
            this.typeName = typeName;
            ScriptManager.Instance.addScript(this);
        }

        public ScriptDescriptor(string filePath, string name, string typeName)
            : base(filePath, name)
        {
            this.typeName = typeName;
            ScriptManager.Instance.addScript(this);
        }

        protected override string CodeClassDef
        {
            get
            {
                return base.CodeClassDef + " : Script<" + typeName + ">" ;
            }
        }
        protected override void addSpecializedCode(System.IO.StreamWriter writer)
        {

            writer.WriteLine("\t\tpublic override void onStart(){ }");
            writer.WriteLine("\t\tpublic override void onUpdate(){ }");


        }

    }
}
