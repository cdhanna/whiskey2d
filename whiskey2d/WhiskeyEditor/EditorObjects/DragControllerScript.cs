using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.EditorObjects
{
    class DragControllerScript : Script<ObjectController>
    {

        private Vector grabOffset;

        public override void onStart()
        {


        }

        public override void onUpdate()
        {

            if (GameManager.Input.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {

                List<InstanceDescriptor> objs = Gob.CurrentLevel.Descriptors;

                foreach (GameObject obj in objs)
                {
                    if (obj.Sprite != null && obj.Bounds.vectorWithin(GameManager.Input.getMousePosition()))
                    {
                        Gob.Dragging = obj;
                        grabOffset = Gob.Dragging.Position - GameManager.Input.getMousePosition();
                        break;
                    }

                }


            }

            if (Gob.Dragging != null)
            {
                GameManager.Controller.SelectedGob = Gob.Dragging;
                Gob.Dragging.Position = GameManager.Input.getMousePosition() + grabOffset;
                if (!GameManager.Input.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                {
                    Gob.Dragging = null;
                }
            }


        }
    }
}
