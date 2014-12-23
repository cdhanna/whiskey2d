using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.MonoHelp;
using Microsoft.Xna.Framework.Input;


namespace WhiskeyEditor.EditorObjects
{
    class CameraMovementScript : Script<ObjectController>
    {
        Vector mouseShiftStart = Vector.Zero;
        Vector mouseShiftCameraStart = Vector.Zero;
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            Camera camera = WhiskeyControl.ActiveCamera;
            
            Vector cameraVel = Vector.Zero;
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Left))
            {
                cameraVel.X -= 2;// camera.getCameraUnits(2);
            }
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Right))
            {
                cameraVel.X += 2;// camera.getCameraUnits(2);
            }

            if (WhiskeyControl.InputManager.isKeyDown(Keys.Up))
            {
                cameraVel.Y -= 2;
            }
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Down))
            {
                cameraVel.Y += 2;
            }

            camera.Position -= cameraVel;

            if (Gob.Selected == null && WhiskeyControl.InputManager.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {
                
                if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                {
                    mouseShiftStart = WhiskeyControl.InputManager.getMousePosition();
                    mouseShiftCameraStart = camera.Position;
                }

                Vector mouseDelta = WhiskeyControl.InputManager.getMousePosition() - mouseShiftStart;

                camera.Position = mouseShiftCameraStart + mouseDelta;

                
            }

            
            Vector screenOrigin = new Vector(WhiskeyControl.WhiskeyGraphicsDevice.PresentationParameters.BackBufferWidth, WhiskeyControl.WhiskeyGraphicsDevice.PresentationParameters.BackBufferHeight) /2;
            camera.Origin = WhiskeyControl.InputManager.getMousePosition();



            //camera.Origin = WhiskeyControl.InputManager.getMousePosition();
            if (WhiskeyControl.InputManager.scrolledDown())
            {
                //camera.Origin = origin;
                camera.Zoom -= .1f;
            }
            if (WhiskeyControl.InputManager.scrolledUp())
            {
                //camera.Origin = origin;
                camera.Zoom += .1f;
            }

            camera.Zoom = Math.Max(camera.Zoom, .5f);
            camera.Zoom = Math.Min(camera.Zoom, 1.5f);

            
        }

        public override void onClose()
        {
        }
    }
}
