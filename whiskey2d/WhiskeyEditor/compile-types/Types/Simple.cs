using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;
namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    public class Simple : CoreTypeDescriptor
    {
        public Simple()
            : base("Simple")
        {
            Description =
@"
The Simple Object is a basic GameObject with no properties added. Its default sprite is a 50x50 pixel. If you drag an art object into a level, a new Simple object will be created with the given art.";
        }

        public override void configure()
        {
            TypeVal rt = this.getTypeValOfName("Sprite");
            Sprite spr = (Sprite)rt.Value;
            spr.Scale *= 50;
            
            base.configure();
        }
         
    }
}
