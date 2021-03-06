﻿using System;
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
        public override void onClose()
        {

        }
        public override void onUpdate()
        {


            Vector mousePos = WhiskeyControl.InputManager.MousePosition;
            mousePos = WhiskeyControl.ActiveCamera.getGameCoordinate(mousePos);

            mousePos = new Vector((int)mousePos.X, (int)mousePos.Y);

            if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {

                //make sure no control points are being selected
                if (!Gob.isSelectingControlPoint(mousePos))
                {



                    List<InstanceDescriptor> objs = Gob.CurrentLevel.getInstances();

                    float highestDepth = 0f;

                    foreach (InstanceDescriptor obj in objs)
                    {
                        if (!obj.Layer.Locked && obj.Layer.Visible)
                        {
                            if (obj.Sprite != null && obj.Bounds.vectorWithin(mousePos) || new Bounds(obj.Position - Vector.One * 8, Vector.One * 16, 0).vectorWithin(mousePos))
                            {
                                if (obj.Sprite.Depth > highestDepth)
                                {
                                    highestDepth = obj.Sprite.Depth;
                                    Gob.Dragging = obj;
                                    grabOffset = Gob.Dragging.Position - mousePos;
                                    //break;
                                }
                            }
                        }

                    }
                }

            }

            

            if (Gob.Dragging != null)
            {
                //WhiskeyControl.Controller.SelectedGob = Gob.Dragging;
                SelectionManager.Instance.SelectedInstance = Gob.Dragging;
                
                Gob.Dragging.Position = mousePos + grabOffset;

                if (WhiskeyControl.InputManager.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                {
                    Vector topLeft = Gob.Dragging.Bounds.TopLeft;
                    topLeft = GridManager.Instance.snap(topLeft);
                    Gob.Dragging.Position = topLeft + Gob.Dragging.Sprite.ScaledOffset;
                }

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
