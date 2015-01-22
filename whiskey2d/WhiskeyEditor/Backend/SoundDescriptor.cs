using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class SoundDescriptor : MediaDescriptor
    {
        public SoundDescriptor(string fullPath)
            : base(fullPath)
        {

        }
    }
}
