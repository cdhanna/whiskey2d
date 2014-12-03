using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;


namespace WhiskeyEditor.EditorObjects
{
    class SelectScript : Script<ObjectController>
    {


        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            if (GameManager.Input.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {
                Gob.Selected = null;
                Gob.Sprite.Color = Color.Transparent;
                //GameManager.Controller.SelectedGob = null;
                List<InstanceDescriptor> objs = Gob.CurrentLevel.Descriptors;//GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();

                foreach (GameObject obj in objs)
                {
                    Vector v = GameManager.Input.getMousePosition();
                    if (obj.Sprite != null && obj.Bounds.vectorWithin(GameManager.Input.getMousePosition()))
                    {
                        Gob.Selected = obj;

                        GameManager.Controller.SelectedGob = obj;

                        break;
                    }

                }
               

            }

            if (Gob.Selected != null)
            {

                Gob.Sprite.Color = Color.Orange;
                Gob.Position = Gob.Selected.Bounds.Position - new Vector(5, 5);
                Gob.Sprite.Scale = Gob.Selected.Bounds.Size + new Vector(10, 10);
            }
            else if (Gob.Unselect)
            {
                Gob.Unselect = false;
                Gob.Sprite.Color = Color.Transparent;
            }

        }
    }
}
