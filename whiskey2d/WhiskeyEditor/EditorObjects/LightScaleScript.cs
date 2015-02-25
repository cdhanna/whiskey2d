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
    public class LightScaleScript : Script<ObjectController>
    {
        Vector startDrag = Vector.Zero;
        bool dragging = false;

        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            if (Gob.Selected != null)
            {
                Vector mousePos = WhiskeyControl.InputManager.getMousePosition();
                mousePos = WhiskeyControl.ActiveCamera.getGameCoordinate(mousePos);

                ObjectControlPoint cp = Gob.ControlPointLightRadius;

                Gob.setControlPointVisibility(Gob.Selected.Light.Visible, cp);


                cp.Position = Gob.Position + (Gob.Selected.Light.Radius / 2) * Vector.UnitX;
                cp.X = cp.Position.X;
                cp.Y = cp.Position.Y;


                if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left) && Gob.Selected.Light.Visible)
                {
                   

                    if (Gob.ControlPointLightRadius.Bounds.vectorWithin(mousePos))
                    {
                        dragging = true;
                        startDrag = Gob.Selected.Position;
                    }

                }

                if (dragging && Gob.Selected.Light.Visible)
                {

                    Vector diff = mousePos - startDrag;
                    Gob.Selected.Light.Radius = diff.Length * 2;


                    if (!WhiskeyControl.InputManager.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                    {
                        dragging = false;
                    }

                }

            }

        }

        public override void onClose()
        {
        }
    }
}
