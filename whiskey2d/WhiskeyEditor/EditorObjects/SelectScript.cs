using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;


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
                GameManager.Controller.SelectedGob = null;
                List<GameObject> objs = GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();

                foreach (GameObject obj in objs)
                {
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

                Gob.Sprite.Color = Color.Blue;
                Gob.Position = Gob.Selected.Bounds.Position - new Vector(5, 5);
                Gob.Sprite.Scale = Gob.Selected.Bounds.Size + new Vector(10, 10);
            }

        }
    }
}
