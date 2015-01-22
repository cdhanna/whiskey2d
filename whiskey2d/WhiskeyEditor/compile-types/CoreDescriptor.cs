using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types
{
    interface CoreDescriptor : Descriptor
    {
        String FilePath { get; }
        void configure();
        String Description { get; }
    }
}
