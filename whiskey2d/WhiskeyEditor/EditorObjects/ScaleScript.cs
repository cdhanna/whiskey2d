using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Inputs;
using WhiskeyEditor.Backend;
using WhiskeyEditor.MonoHelp;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.EditorObjects
{
    class ScaleScript : Script<ObjectController>
    {

        Vector startDrag = Vector.Zero;
        Vector startScale = Vector.Zero;
        Vector startPos = Vector.Zero;

        ObjectControlPoint draggedOCP = null;
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            

            Vector mousePos = WhiskeyControl.InputManager.getMousePosition();
            mousePos = WhiskeyControl.ActiveCamera.getGameCoordinate(mousePos);

            if (!WhiskeyControl.InputManager.isMouseDown(MouseButtons.Left))
            {
                //if (draggedOCP != null)
                //{
                //    process(mousePos);
                //}
                draggedOCP = null;
            }

            if (Gob.Selected != null)
            {

                showControlPoints();

                
                

                Gob.ControlPointRight.Position = Gob.Position + new Vector( Gob.Bounds.Size.X / 2, 0);
                Gob.ControlPointRightTop.Position = Gob.Position + new Vector(Gob.Bounds.Size.X / 2, - Gob.Bounds.Size.Y /2);
                Gob.ControlPointTop.Position = Gob.Position + new Vector(0, -Gob.Bounds.Size.Y / 2);
                Gob.ControlPointLeftTop.Position = Gob.Position + new Vector(-Gob.Bounds.Size.X / 2, -Gob.Bounds.Size.Y / 2);
                Gob.ControlPointLeft.Position = Gob.Position + new Vector(-Gob.Bounds.Size.X / 2, 0);
                Gob.ControlPointLeftBot.Position = Gob.Position + new Vector(-Gob.Bounds.Size.X / 2, Gob.Bounds.Size.Y / 2);
                Gob.ControlPointBot.Position = Gob.Position + new Vector(0, Gob.Bounds.Size.Y / 2);
                Gob.ControlPointRightBot.Position = Gob.Position + new Vector(Gob.Bounds.Size.X / 2, Gob.Bounds.Size.Y / 2);

                

                if (draggedOCP == null && WhiskeyControl.InputManager.isNewMouseDown(MouseButtons.Left))
                {
                    
                    foreach (ObjectControlPoint ocp in Gob.ControlPoints)
                    {
                        if (ocp.Bounds.vectorWithin(mousePos))
                        {
                            startDrag = mousePos;
                            startScale = Gob.Selected.Sprite.Scale;
                            startPos = Gob.Selected.Position;
                            draggedOCP = ocp;
                            
                            break;
                        }
                    }

                }

                if (draggedOCP != null && WhiskeyControl.InputManager.isMouseDown(MouseButtons.Left))
                {

                    process(mousePos);

                    //if (draggedOCP != null && !WhiskeyControl.InputManager.isMouseDown(MouseButtons.Left))
                    //{
                    //    draggedOCP = null;
                    //}


                }
                
            }
            else
            {
                //draggedOCP = null;
                hideControlPoints();
            }

           
        }

        public override void onClose()
        {
        }
        private void setPos(Vector pos)
        {
            Gob.Selected.Position = pos;
            Gob.Selected.X = Gob.Selected.Position.X;
            Gob.Selected.Y = Gob.Selected.Position.Y;
        }
        private void process(Vector mousePos)
        {
            Vector diff = mousePos - startDrag;
            Vector scaleDiff = new Vector(diff.X / Gob.Selected.Sprite.FrameWidth, diff.Y / Gob.Selected.Sprite.FrameHeight);

            if (draggedOCP == Gob.ControlPointRight)
            {
                scaleDiff.Y = 0;
                diff.Y = 0;

                Gob.Selected.Sprite.Scale = startScale + scaleDiff;

                
                setPos(startPos + diff / 2);
               
                Gob.Selected.update();

            }
            else if (draggedOCP == Gob.ControlPointLeft)
            {
                scaleDiff.Y = 0;
                diff.Y = 0;

                Gob.Selected.Sprite.Scale = startScale - scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointTop)
            {
                scaleDiff.X = 0;
                diff.X = 0;

                Gob.Selected.Sprite.Scale = startScale - scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointBot)
            {
                scaleDiff.X = 0;
                diff.X = 0;

                Gob.Selected.Sprite.Scale = startScale + scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointRightTop)
            {
                scaleDiff.Y *= -1;
                //diff.Y *= -1;
                Gob.Selected.Sprite.Scale = startScale + scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointLeftTop)
            {
                scaleDiff.Y *= -1;
                scaleDiff.X *= -1;
                //diff.Y *= -1;
                Gob.Selected.Sprite.Scale = startScale + scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointRightBot)
            {
                //diff.Y *= -1;
                Gob.Selected.Sprite.Scale = startScale + scaleDiff;
                setPos(startPos + diff / 2);

            }
            else if (draggedOCP == Gob.ControlPointLeftBot)
            {
                //diff.Y *= -1;
                scaleDiff.X *= -1;

                Gob.Selected.Sprite.Scale = startScale + scaleDiff;
                setPos(startPos + diff / 2);

            }
            Gob.Selected.Sprite.Scale = new Vector(Math.Abs(Gob.Selected.Sprite.Scale.X), Math.Abs(Gob.Selected.Sprite.Scale.Y));

        }

        private void setControlPointVisibility(bool vis, ObjectControlPoint ocp)
        {

            ocp.Sprite.Visible = vis;
            ocp.Sprite.Depth = Gob.Sprite.Depth + .01f;
            ocp.Sprite.Color = Gob.CurrentLevel.BackgroundColor.invert().lerp(Color.Black, .5f);
        
        }

        private void setControlPointsVisibility(bool visible)
        {
            setControlPointVisibility(visible, Gob.ControlPointRight);
            setControlPointVisibility(visible, Gob.ControlPointRightTop);
            setControlPointVisibility(visible, Gob.ControlPointTop);
            setControlPointVisibility(visible, Gob.ControlPointLeftTop);
            setControlPointVisibility(visible, Gob.ControlPointLeft);
            setControlPointVisibility(visible, Gob.ControlPointLeftBot);
            setControlPointVisibility(visible, Gob.ControlPointBot);
            setControlPointVisibility(visible, Gob.ControlPointRightBot);
        }

        private void hideControlPoints()
        {
            setControlPointsVisibility(false);
        }
        private void showControlPoints()
        {
            setControlPointsVisibility(true);
        }

    }
}
