﻿using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    class CameraMaster : CoreTypeDescriptor
    {
        public CameraMaster()
            : base("CameraMaster")
        {

        }

        public override void configure()
        {

            Sprite spr = getTypeValOfName("Sprite").Value as Sprite;
            spr.Scale = new Vector(100, 100);
            spr.Color = Color.Green;
            getTypeValOfName("IsDebug").Value = true;

            addPropertyDescriptor(new PropertyDescriptor("Target", new RealType(typeof(String), "")));

            addScript("CameraMasterControl");

            base.configure();
        }

    }
}
