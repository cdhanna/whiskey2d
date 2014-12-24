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
    class SelectScript : Script<ObjectController>
    {


        public override void onStart()
        {

            SelectionManager.Instance.SelectedInstanceChanged += (s, a) =>
            {
                Gob.Selected = SelectionManager.Instance.SelectedInstance;
                if (Gob.Selected == null)
                {
                    Gob.Unselect = true;

                }
            };

        }
        public override void onClose()
        {

        }
        public override void onUpdate()
        {
            if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {
                Gob.Selected = null;
                Gob.Sprite.Color = Color.Transparent;
                //GameManager.Controller.SelectedGob = null;
                List<InstanceDescriptor> objs = Gob.CurrentLevel.getInstances();//GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();

                foreach (InstanceDescriptor obj in objs)
                {
                    Vector mousePos = WhiskeyControl.InputManager.getMousePosition();
                    mousePos = WhiskeyControl.ActiveCamera.getGameCoordinate(mousePos);

                    if (obj.Sprite != null && obj.Bounds.vectorWithin(mousePos) 
                        || new Bounds(obj.Position - Vector.One * 8, Vector.One * 16).vectorWithin(mousePos)
                        
                        
                        || obj == Gob.ControlPointObject && (
                               Gob.ControlPointRight.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointRightTop.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointTop.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointLeftTop.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointLeft.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointLeftBot.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointBot.Bounds.vectorWithin(mousePos)
                            || Gob.ControlPointRightBot.Bounds.vectorWithin(mousePos)) 

                        )
                    {
                        Gob.Selected = obj;
                        Gob.ControlPointObject = Gob.Selected;
                        SelectionManager.Instance.SelectedInstance = obj;
                        

                        break;
                    }

                }

                if (Gob.Selected == null)
                {
                    SelectionManager.Instance.SelectedInstance = null;
                }

            }

            

            if (Gob.Selected != null)
            {

                Gob.Sprite.Color = Gob.CurrentLevel.BackgroundColor.invert();

                Gob.Position = Gob.Selected.Position;
                Gob.Sprite.Scale = Gob.Selected.Bounds.Size + new Vector(12);
               
            }

            
            else if (Gob.Unselect)
            {
                Gob.Unselect = false;
                Gob.Sprite.Color = Color.Transparent;
            }

        }
    }
}
