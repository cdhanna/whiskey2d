using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.compile_types;
namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class CoreTypeDescriptor : TypeDescriptor, CoreDescriptor
    {

        public CoreTypeDescriptor(string name)
            : base(CoreTypes.corePathTypes.Substring(0, CoreTypes.corePathTypes.LastIndexOf("\\")), name)
        {

        }

        public virtual void configure()
        {

        }

        public String Description { get; protected set; }

    }
}
