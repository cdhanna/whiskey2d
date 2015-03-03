using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    class SoundZone : CoreTypeDescriptor
    {
        public SoundZone()
            : base("SoundZone")
        {

        }

        public override void configure()
        {

            Sprite spr = getTypeValOfName("Sprite").Value as Sprite;
            spr.Scale = new Vector(2, 6);
            spr.Color = Color.Blue;
            spr.ImagePath = "triggerZone.png";
            spr.Tiled = true;

            getTypeValOfName("IsDebug").Value = true;

            addPropertyDescriptor(new PropertyDescriptor("Sound", new RealType(typeof(String), "")));
            addPropertyDescriptor(new PropertyDescriptor("TripOn", new RealType(typeof(String), "")));
            addPropertyDescriptor(new PropertyDescriptor("OnEnter", new RealType(typeof(Boolean), true)));
            addPropertyDescriptor(new PropertyDescriptor("OnExit", new RealType(typeof(Boolean), false)));
            addPropertyDescriptor(new PropertyDescriptor("PlayCount", new RealType(typeof(int), 1)));
            addPropertyDescriptor(new PropertyDescriptor("Loop", new RealType(typeof(Boolean), false)));


            addScript("SoundZoneControl");

            base.configure();
        }

    }
}
