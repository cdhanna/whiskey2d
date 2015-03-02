using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Scripts
{
    [Serializable]
    class SpriteEffectControl : CoreScriptDescriptor
    {

        public SpriteEffectControl()
            : base ("SpriteEffectControl", "SpriteEffect")
        {

        }

        public override string getClassCode()
        {


            return "Animation a = null;";
             
        }

        public override string getStartCode()
        {

            String code = ""
                + "Gob.Sprite.ImagePath = Gob.Effect + \".png\"; "
                + "Gob.Sprite.Rows = (int)Gob.Frames.X; " 
                + "Gob.Sprite.Columns = (int)Gob.Frames.Y;" 
                + "int end = Gob.EndFrame < 0 ? Gob.Sprite.FrameCount : Gob.EndFrame;"
                + "a = Gob.Sprite.createAnimation(Gob.StartFrame, end, Gob.Speed, Gob.Looped);";

            return code;
        }


        public override string getUpdateCode()
        {

            String code = "a.advanceFrame();"

                + "if (Gob.OnUpdate != null) {"
                + "Gob.OnUpdate(a);"
                + "}"

            + "if (a.CurrentFrame == a.EndFrame && !a.Looped)"
            + "{"
            + " Gob.close();"
            + "}";

            return code;
        }

    }
}
