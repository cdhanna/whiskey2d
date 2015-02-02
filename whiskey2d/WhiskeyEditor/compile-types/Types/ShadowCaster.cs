using System;
using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    public class ShadowCaster : CoreTypeDescriptor
    {
        public ShadowCaster()
            : base("ShadowCaster")
        {

        }

        public override void configure()
        {
            TypeVal rt = this.getTypeValOfName("Sprite");
            Sprite spr = (Sprite)rt.Value;
            
            spr.Depth = .3f;
            spr.Color = Color.Black;
            spr.Scale = Vector.One * 50;

           
            getTypeValOfName("ShadowCaster").Value = true;
            
            base.configure();
        }

    }
}
