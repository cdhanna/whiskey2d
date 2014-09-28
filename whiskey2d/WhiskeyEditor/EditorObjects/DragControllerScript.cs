using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
namespace WhiskeyEditor.EditorObjects
{
    class DragControllerScript : Script<ObjectController>
    {

        private Vector2 grabOffset;

        public override void onStart()
        {
            
        }

        public override void onUpdate()
        {

            if (GameManager.Input.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {

                List<GameObject> objs = GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();

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
                Gob.Dragging.Position = GameManager.Input.getMousePosition() + grabOffset;
                if (!GameManager.Input.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                {
                    Gob.Dragging = null;
                }
            }


        }
    }
}
