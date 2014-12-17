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

namespace WhiskeyEditor.EditorObjects
{

    [Serializable]
    class DragControllerScript : Script<ObjectController>
    {

        private Vector grabOffset;

        public override void onStart()
        {


        }

        public override void onUpdate()
        {
            
            

            if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {

                List<InstanceDescriptor> objs = Gob.CurrentLevel.getInstances();

                foreach (InstanceDescriptor obj in objs)
                {
                    if (obj.Sprite != null && obj.Bounds.vectorWithin(WhiskeyControl.InputManager.getMousePosition()) || new Bounds(obj.Position - obj.Sprite.Offset - Vector.One * 8, Vector.One * 16).vectorWithin(WhiskeyControl.InputManager.getMousePosition()))
                    {
                        Gob.Dragging = obj;
                        grabOffset = Gob.Dragging.Position - WhiskeyControl.InputManager.getMousePosition();
                        break;
                    }

                }


            }

            if (Gob.Dragging != null)
            {
                //WhiskeyControl.Controller.SelectedGob = Gob.Dragging;
                SelectionManager.Instance.SelectedInstance = Gob.Dragging;
                
                Gob.Dragging.Position = WhiskeyControl.InputManager.getMousePosition() + grabOffset;
                Gob.Dragging.X = Gob.Dragging.Position.X;
                Gob.Dragging.Y = Gob.Dragging.Position.Y;

                if (!WhiskeyControl.InputManager.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                {
                    Gob.Dragging = null;
                }
            }


        }
    }
}
