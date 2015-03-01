using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Types
{

    [Serializable]
    public class SpriteEffect : CoreTypeDescriptor
    {
        public SpriteEffect()
            : base("SpriteEffect")
        {
            
        }


        

        public override void configure()
        {

            Sprite spr = getTypeValOfName("Sprite").Value as Sprite;
            spr.Scale = Vector.One ;
            
            addPropertyDescriptor(new PropertyDescriptor("Effect", new RealType(typeof(String), "")));
            addPropertyDescriptor(new PropertyDescriptor("Frames", new RealType(typeof(Vector), Vector.One)));
            addPropertyDescriptor(new PropertyDescriptor("Speed", new RealType(typeof(int), 7)));
            addPropertyDescriptor(new PropertyDescriptor("StartFrame", new RealType(typeof(int), 0)));
            addPropertyDescriptor(new PropertyDescriptor("EndFrame", new RealType(typeof(int), -1)));
            addPropertyDescriptor(new PropertyDescriptor("Looped", new RealType(typeof(bool), false)));


            addPropertyDescriptor(new PropertyDescriptor("OnUpdate", new RealType(typeof(WhiskeyFunction<Animation>), new WhiskeyFunction<Animation>(nothing))));

            addScript("SpriteEffectControl");

            base.configure();
        }

        private void nothing(Animation a)
        {

        }
      

    }
}
