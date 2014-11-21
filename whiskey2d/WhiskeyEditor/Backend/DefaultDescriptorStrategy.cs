using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class DefaultDescriptorStrategy : DescriptorStrategy
    {



        protected virtual String CodeNameSpace
        {
            get { return "Project"; }
        }

        protected virtual String CodeClassDef
        {
            get
            {
                return "[Serializable] " + Environment.NewLine + "\tpublic class " + Descriptor.Name;
            }
        }
        protected virtual String[] CodeUsingStatements
        {
            get
            {
                return new string[] { "System", "Whiskey2D.Core" };
            }
        }
    }
}
