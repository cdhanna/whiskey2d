using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey2D.Core
{
    [Serializable]
    public class ShadowProperties
    {
        public Boolean CastsShadows { get; set; }
        public Boolean IncludeLight { get; set; }
        public Boolean SelfShadows { get; set; }
        public ShadowProperties() : this(false, true, false)
        {
           
        }

        public ShadowProperties(Boolean castsShadows, Boolean includeLight, Boolean selfShadows)
        {
            CastsShadows = castsShadows;
            IncludeLight = includeLight;
            SelfShadows = selfShadows;
        }

    }
}
