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
                Vector mousePos = WhiskeyControl.InputManager.getMousePosition();
                mousePos = WhiskeyControl.ActiveCamera.getGameCoordinate(mousePos);

                //check if selection already exists. If so, check for controlPoints
               
                {


                    Gob.Selected = null;
                    Gob.Sprite.Color = Color.Transparent;
                    //GameManager.Controller.SelectedGob = null;
                    List<InstanceDescriptor> objs = Gob.CurrentLevel.getInstances();//GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();


                    float highestDepth = 0;

                    foreach (InstanceDescriptor obj in objs)
                    {
                        if (!obj.Layer.Locked && obj.Layer.Visible)
                        {



                            if (obj.Sprite != null && obj.Bounds.vectorWithin(mousePos)
                                || new Bounds(obj.Position - Vector.One * 8, Vector.One * 16, 0).vectorWithin(mousePos)


                                || obj == Gob.ControlPointObject && (
                                    Gob.isSelectingControlPoint(mousePos))

                                )
                            {



                                if (obj.Sprite.Depth > highestDepth)
                                {
                                    Gob.Selected = obj;
                                    highestDepth = obj.Sprite.Depth;
                                    Gob.ControlPointObject = Gob.Selected;
                                    SelectionManager.Instance.SelectedInstance = obj;
                                }

                                //break;
                            }
                        }

                    }

                    if (Gob.Selected == null)
                    {
                        SelectionManager.Instance.SelectedInstance = null;
                    }
                }
            }

            

            if (Gob.Selected != null)
            {
                //Sprite clone = new Sprite(Gob.Selected.Sprite);
               // Gob.Sprite = clone;
                Gob.Sprite.Color = Gob.CurrentLevel.BackgroundColorCompliment;
                Gob.Sprite.Rotation = Gob.Selected.Sprite.Rotation;
                //Color c = Gob.Sprite.Color;
                //c.A = 128;
                //Gob.Sprite.Color = c;
                
                Gob.Sprite.Depth = Gob.Selected.Sprite.Depth /2;
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
