using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Microsoft.Xna.Framework;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.EditorObjects
{
    [Serializable]
    public class ObjectController : EditorGameObject
    {
        public InstanceDescriptor Dragging { get; set; }
        public InstanceDescriptor Selected { get; set; }
        public InstanceDescriptor ControlPointObject { get; set; }
        public bool Unselect { get; set; }
        public EditorLevel CurrentLevel { get; set; }

        public ObjectControlPoint ControlPointRight { get; set; }
        public ObjectControlPoint ControlPointRightTop { get; set; }
        public ObjectControlPoint ControlPointTop { get; set; }
        public ObjectControlPoint ControlPointLeftTop { get; set; }
        public ObjectControlPoint ControlPointLeft { get; set; }
        public ObjectControlPoint ControlPointLeftBot { get; set; }
        public ObjectControlPoint ControlPointBot { get; set; }
        public ObjectControlPoint ControlPointRightBot { get; set; }

        public ObjectControlPoint ControlPointLightRadius { get; set; }

        private Whiskey2D.Core.Convex convex;

        public ObjectControlPoint[] ControlPoints {get; set;} 

        public ObjectController(ObjectManager manager) : base(manager)
        {
            Sprite = new Sprite();
            Unselect = false;
            convex = new Convex(Vector.Zero, 0, VectorSet.Dodecahedren);

            ControlPointLightRadius = new ObjectControlPoint(manager);

            ControlPointRight = new ObjectControlPoint(manager);
            ControlPointRightTop = new ObjectControlPoint(manager);
            ControlPointTop = new ObjectControlPoint(manager);
            ControlPointLeftTop = new ObjectControlPoint(manager);
            ControlPointLeft = new ObjectControlPoint(manager);
            ControlPointLeftBot = new ObjectControlPoint(manager);
            ControlPointBot = new ObjectControlPoint(manager);
            ControlPointRightBot = new ObjectControlPoint(manager);
            ControlPoints = new ObjectControlPoint[] { ControlPointRight, ControlPointRightTop, ControlPointTop, ControlPointLeftTop, ControlPointLeft, ControlPointLeftBot, ControlPointBot, ControlPointRightBot };
        }

        protected override void addInitialScripts()
        {
            addScript(new DragControllerScript());
            addScript(new SelectScript());
            addScript(new CopyPasteScript());
            addScript(new CameraMovementScript());
            addScript(new ScaleScript());
            addScript(new RotateScript());
            addScript(new LightScaleScript());
            addScript(new GridSizeScript());
        }

        public void setControlPointVisibility(bool vis, ObjectControlPoint ocp)
        {

            ocp.Sprite.Visible = vis;
            ocp.Sprite.Depth = Sprite.Depth + .01f;
            ocp.Sprite.Color = Sprite.Color;//Gob.CurrentLevel.BackgroundColor.invert().lerp(Microsoft.Xna.Framework.Color.Black, .5f);
            ocp.Sprite.Scale = Vector.One * Math.Min(35, Math.Max(6, 20 / (CurrentLevel.Camera.Zoom*1.2f)));
        }

        public bool isSelectingControlPoint(Vector mousePos)
        {
            return ControlPointRight.Bounds.vectorWithin(mousePos)
                                    || ControlPointRightTop.Bounds.vectorWithin(mousePos)
                                    || ControlPointTop.Bounds.vectorWithin(mousePos)
                                    || ControlPointLeftTop.Bounds.vectorWithin(mousePos)
                                    || ControlPointLeft.Bounds.vectorWithin(mousePos)
                                    || ControlPointLeftBot.Bounds.vectorWithin(mousePos)
                                    || ControlPointBot.Bounds.vectorWithin(mousePos)
                                    || ControlPointRightBot.Bounds.vectorWithin(mousePos)
                                    || ControlPointLightRadius.Bounds.vectorWithin(mousePos);

        }
        
    }

    [Serializable]
    public class ObjectControlPoint : EditorGameObject
    {

        public ObjectControlPoint(ObjectManager manager)
            : base(manager)
        {
            Sprite = new Sprite();
            Sprite.setRender(WhiskeyEditor.MonoHelp.WhiskeyControl.Renderer);
            Sprite.Scale = new Vector(24);
            Sprite.Depth = .3f;
            
        }

        

        protected override void addInitialScripts()
        {
        }
    }

}
