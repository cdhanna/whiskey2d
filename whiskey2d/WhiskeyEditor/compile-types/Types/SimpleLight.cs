using System;
using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;
namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    public class SimpleLight : CoreTypeDescriptor
    {
       

        public SimpleLight()
            : base("SimpleLight")
        {

        }

        public override void configure()
        {
            TypeVal rt = this.getTypeValOfName("Sprite");
            Sprite spr = (Sprite)rt.Value;
            spr.ImagePath = "lightIcon.png";
            spr.Depth = 1;
            spr.Scale *= .5f;

            TypeVal lt = this.getTypeValOfName("Light");
            Light light = (Light)lt.Value;
            light.Visible = true;
            

            getTypeValOfName("IsDebug").Value = true;
            
            base.configure();
        }
    }
}
