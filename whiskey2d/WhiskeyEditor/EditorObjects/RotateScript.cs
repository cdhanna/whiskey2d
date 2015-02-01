using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;
using WhiskeyEditor.MonoHelp;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.EditorObjects
{
    [Serializable]
    class RotateScript : Script<ObjectController>
    {
        public override void onStart()
        {
        }

        public override void onUpdate()
        {

            if (Gob.Selected != null)
            {

                if (WhiskeyControl.InputManager.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                {
                    float dir = 0;
                    if (WhiskeyControl.InputManager.isNewKeyDown(Microsoft.Xna.Framework.Input.Keys.R))
                    {
                        dir = 1;
                    }
                    if (WhiskeyControl.InputManager.isNewKeyDown(Microsoft.Xna.Framework.Input.Keys.E))
                    {
                        dir = -1;
                    }

                    Gob.Selected.Sprite.Rotation += (float)(Math.PI / 12) * dir;

                }

            }

        }

        public override void onClose()
        {
        }
    }
}
