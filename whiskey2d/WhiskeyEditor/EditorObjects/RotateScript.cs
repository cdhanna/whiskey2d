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

                }

            }

        }

        public override void onClose()
        {
        }
    }
}
