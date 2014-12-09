using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using System.IO;

namespace WhiskeyEditor.Backend
{
    class MediaDescriptor : FileDescriptor
    {



        public MediaDescriptor(string filePath)
            : base(filePath, filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar)))
        {

        }

    }
}
