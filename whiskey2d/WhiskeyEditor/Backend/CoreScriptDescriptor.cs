using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.compile_types;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    class CoreScriptDescriptor : ScriptDescriptor, CoreDescriptor
    {
        public CoreScriptDescriptor(string name, string typeName)
            : base(CoreTypes.corePathScripts.Substring(0, CoreTypes.corePathScripts.LastIndexOf("\\")), name, typeName)
        {

            

        }

        public String Description { get; protected set; }


        public virtual void configure()
        {

        }

        public virtual string getStartCode()
        {
            return "//start code";
        }
        public virtual string getUpdateCode()
        {
            return "//update code";
        }
        public virtual string getCloseCode()
        {
            return "//close code";
        }

        protected override void addSpecializedCode(System.IO.StreamWriter writer)
        {

            writer.WriteLine("\t\tpublic override void onStart()");
            writer.WriteLine("\t\t{");
            writer.WriteLine("\t\t" + getStartCode() );
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t\t");

            writer.WriteLine("\t\tpublic override void onUpdate() ");
            writer.WriteLine("\t\t{");
            writer.WriteLine("\t\t"  + getUpdateCode() );
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t\t");

            writer.WriteLine("\t\tpublic override void onClose()  ");
            writer.WriteLine("\t\t{");
            writer.WriteLine("\t\t" +getCloseCode());
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t\t");
        }

    }
}
