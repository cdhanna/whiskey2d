using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class ScriptDescriptor : FileDescriptor
    {

        private String typeName;

        public ScriptDescriptor(string filePath, string name, string typeName)
            : base(filePath, name)
        {
            this.typeName = typeName;

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
