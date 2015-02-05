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
        public float Solidness { get; set; }
        public float Height { get; set; }

        public ShadowProperties() : this(false, true, false, 1, 100)
        {
           
        }

        public ShadowProperties(Boolean castsShadows, Boolean includeLight, Boolean selfShadows, float solidness, float height)
        {
            CastsShadows = castsShadows;
            IncludeLight = includeLight;
            SelfShadows = selfShadows;
            Solidness = solidness;
            Height = height;
        }

    }
}
