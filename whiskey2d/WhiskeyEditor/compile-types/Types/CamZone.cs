using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{

    [Serializable]
    class CamZone : CoreTypeDescriptor
    {

        public CamZone()
            : base("CamZone")
        {

        }

        public override void configure()
        {

            Sprite spr = getTypeValOfName("Sprite").Value as Sprite;
            spr.Scale = new Vector(200, 200);
            spr.Depth = .2f;
            spr.Color = new Color(80, 255, 80, 100);
         
            getTypeValOfName("IsDebug").Value = true;

            addPropertyDescriptor(new PropertyDescriptor("Zoom", new RealType(typeof(float), 1)));

            base.configure();
        }


    }
}
