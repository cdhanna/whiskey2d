using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class ScriptDescriptor : FileDescriptor
    {

        public ScriptDescriptor(string filePath, string name)
            : base(filePath, name)
        {
        }

    }
}
