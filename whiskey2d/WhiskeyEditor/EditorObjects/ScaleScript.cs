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
using Microsoft.Xna.Framework;

namespace WhiskeyEditor.EditorObjects
{
    class ScaleScript : Script<ObjectController>
    {

        Vector startDrag = Vector.Zero;
        Vector startScale = Vector.Zero;
        Vector startPos = Vector.Zero;
        Vector startDragMod = Vector.Zero;

        ObjectControlPoint draggedOCP = null;
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            

            Vector mousePos = WhiskeyControl.InputManager.MousePosition;
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

                
                
                Matrix m = Matrix.Identity;
                m *= Matrix.CreateRotationZ(Gob.Sprite.Rotation);

                Gob.ControlPointRight.Position = Gob.Position + (Vector)Vector2.Transform( new Vector( Gob.Bounds.Size.X / 2, 0), m);
                Gob.ControlPointRightTop.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(Gob.Bounds.Size.X / 2, - Gob.Bounds.Size.Y /2), m);
                Gob.ControlPointTop.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(0, -Gob.Bounds.Size.Y / 2), m);
                Gob.ControlPointLeftTop.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(-Gob.Bounds.Size.X / 2, -Gob.Bounds.Size.Y / 2), m);
                Gob.ControlPointLeft.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(-Gob.Bounds.Size.X / 2, 0), m);
                Gob.ControlPointLeftBot.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(-Gob.Bounds.Size.X / 2, Gob.Bounds.Size.Y / 2), m);
                Gob.ControlPointBot.Position = Gob.Position + (Vector)Vector2.Transform( new Vector(0, Gob.Bounds.Size.Y / 2), m);
                Gob.ControlPointRightBot.Position = Gob.Position + (Vector)Vector2.Transform(new Vector(Gob.Bounds.Size.X / 2, Gob.Bounds.Size.Y / 2), m);

                

                if (draggedOCP == null && WhiskeyControl.InputManager.isNewMouseDown(MouseButtons.Left))
                {
                    
                    foreach (ObjectControlPoint ocp in Gob.ControlPoints)
                    {
                        if (ocp.Bounds.vectorWithin(mousePos))
                        {
                            

                            startDrag = mousePos;
                            startDragMod = startDrag;
                            if (ocp == Gob.ControlPointRight || ocp == Gob.ControlPointRightTop || ocp == Gob.ControlPointRightBot)
                            {
                                startDragMod.X = Gob.Selected.Bounds.Right;
                            }
                            if (ocp == Gob.ControlPointLeft || ocp == Gob.ControlPointLeftTop || ocp == Gob.ControlPointLeftBot)
                            {
                                startDragMod.X = Gob.Selected.Bounds.Left;
                            }
                            if (ocp == Gob.ControlPointTop || ocp == Gob.ControlPointRightTop || ocp == Gob.ControlPointLeftTop)
                            {
                                startDragMod.Y = Gob.Selected.Bounds.Top;
                            }
                            if (ocp == Gob.ControlPointBot || ocp == Gob.ControlPointRightBot || ocp == Gob.ControlPointRightBot)
                            {
                                startDragMod.Y = Gob.Selected.Bounds.Bottam;
                            }

                            startScale = Gob.Selected.Sprite.Scale;
                            startPos = Gob.Selected.Position;

                            
                            //if (WhiskeyEditor.MonoHelp.WhiskeyControl.InputManager.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                            //{
                            //    mousePos = GridManager.Instance.snap(mousePos);
                            //    //mousePos = GridManager.Instance.snapRound(mousePos);
                            //}

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

            Matrix m = Matrix.Identity;
            m *= Matrix.CreateRotationZ(Gob.Sprite.Rotation);


            if (WhiskeyEditor.MonoHelp.WhiskeyControl.InputManager.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {

                startDrag = startDragMod;
                //mousePos = GridManager.Instance.snap(mousePos);
                mousePos = GridManager.Instance.snap(mousePos);
                

            }

            Vector diff = mousePos - startDrag;

            


            Vector scaleDiff = new Vector(diff.X / Gob.Selected.Sprite.FrameWidth, diff.Y / Gob.Selected.Sprite.FrameHeight);

            //diff = Vector2.Transform(diff, m);
            //scaleDiff = Vector2.Transform(scaleDiff, m)

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

       

        private void setControlPointsVisibility(bool visible)
        {
            Gob.setControlPointVisibility(visible, Gob.ControlPointRight);
            Gob.setControlPointVisibility(visible, Gob.ControlPointRightTop);
            Gob.setControlPointVisibility(visible, Gob.ControlPointTop);
            Gob.setControlPointVisibility(visible, Gob.ControlPointLeftTop);
            Gob.setControlPointVisibility(visible, Gob.ControlPointLeft);
            Gob.setControlPointVisibility(visible, Gob.ControlPointLeftBot);
            Gob.setControlPointVisibility(visible, Gob.ControlPointBot);
            Gob.setControlPointVisibility(visible, Gob.ControlPointRightBot);
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
