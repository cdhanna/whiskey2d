using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    public delegate void ScriptAddedEventHandler(object sender, ScriptChangedEventArgs args);
    public delegate void ScriptRemovedEventHandler(object sende, ScriptChangedEventArgs args);
    public class ScriptChangedEventArgs : EventArgs
    {
        private ScriptDescriptor sDesc;
        public ScriptDescriptor Script { get { return sDesc; } }
        public ScriptChangedEventArgs(ScriptDescriptor sDesc)
        {
            this.sDesc = sDesc;
        }
    }


    [Serializable]
    public class ScriptDescriptor : CodeDescriptor
    {

        private String typeName;

        public ScriptDescriptor(string name, string typeName)
            : base(name)
        {
            this.typeName = typeName;
            ScriptManager.Instance.addScript(this);
            FileManager.Instance.addFileDescriptor(this);
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
