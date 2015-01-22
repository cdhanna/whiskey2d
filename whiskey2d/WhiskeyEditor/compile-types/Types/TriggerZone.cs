using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    class TriggerZone : CoreTypeDescriptor
    {

        public TriggerZone()
            : base("TriggerZone")
        {
            Description = "A way to trigger objects to activate";
        }

        public override void configure()
        {
            Sprite spr = getTypeValOfName("Sprite").Value as Sprite;
            spr.Scale = new Vector(2, 6);
            spr.Color = Color.Yellow;
            spr.ImagePath = "triggerZone.png";
            spr.Tiled = true;

            getTypeValOfName("IsDebug").Value = true;

            addPropertyDescriptor(new PropertyDescriptor("Tripper", new RealType(typeof(String), "")));
            addPropertyDescriptor(new PropertyDescriptor("Trip", new RealType(typeof(String), "")));
            addPropertyDescriptor(new PropertyDescriptor("Tripped", new RealType(typeof(Boolean), false)));

            addScript("TriggerActivate");

            base.configure();
        }

    }
}
