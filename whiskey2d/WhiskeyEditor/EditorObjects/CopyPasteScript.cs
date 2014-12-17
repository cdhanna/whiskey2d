using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using WhiskeyEditor.Backend;
using WhiskeyEditor.MonoHelp;
using WhiskeyEditor.Backend.Managers;
using Microsoft.Xna.Framework.Input;

namespace WhiskeyEditor.EditorObjects
{
    class CopyPasteScript : Script<ObjectController>
    {
        public override void onStart()
        {
            
        }

        public override void onUpdate()
        {
            if (WhiskeyControl.InputManager.isKeyDown(Keys.C))
            {
                Console.WriteLine("SDF");
            }

            if (WhiskeyControl.InputManager.isNewKeyDown(Keys.C) && WhiskeyControl.InputManager.isKeyDown(Keys.LeftControl))
            {
                CopyPasteManager.Instance.copyToBuffer(Gob.Selected);
            }
            if (WhiskeyControl.InputManager.isNewKeyDown(Keys.V) && WhiskeyControl.InputManager.isKeyDown(Keys.LeftControl))
            {
                InstanceDescriptor inst = CopyPasteManager.Instance.pasteFromBuffer(Gob.CurrentLevel);
                if (inst != null)
                {
                    inst.X = WhiskeyControl.InputManager.getMousePosition().X;
                    inst.Y = WhiskeyControl.InputManager.getMousePosition().Y;
                }
            }
        }
    }
}
