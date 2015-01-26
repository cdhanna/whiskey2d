using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using WhiskeyEditor.Backend;
namespace WhiskeyEditor.compile_types.Types
{
    [Serializable]
    class DebugObject : CoreTypeDescriptor
    {
        public DebugObject()
            : base("DebugObject")
        {
      
        }

        public override void configure()
        {
            TypeVal rt = this.getTypeValOfName("Sprite");
            Sprite spr = (Sprite)rt.Value;
            spr.ImagePath = "debug.png";
            spr.Scale = new Vector(2, 6);
            spr.Color = Color.Purple;
            spr.Tiled = true;
            getTypeValOfName("IsDebug").Value = true;
            base.configure();
        }

    }
}
