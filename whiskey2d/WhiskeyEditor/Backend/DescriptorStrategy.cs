using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WhiskeyEditor.Backend
{
    public abstract class DescriptorStrategy
    {

        private FileDescriptor desc;

        public DescriptorStrategy(FileDescriptor desc)
        {
            this.desc = desc;
        }

        protected FileDescriptor Descriptor { get { return desc; } }

        public virtual string CodeNameSpace
        {
            get
            {
                return "";
            }
        }

        public virtual string CodeClassDefinition
        {
            get
            {
                return "";
            }
        }
        public virtual string[] CodeUsingStatements
        {
            get
            {
                return new string[] { };
            }
        }

        public virtual void addSpecializedCode(StreamWriter writer)
        {
            
        }

        public virtual void processExistingCode(string[] allLines)
        {

        }



    }
}
