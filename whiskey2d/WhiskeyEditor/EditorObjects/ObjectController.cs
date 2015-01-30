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
        public ObjectControlPoint[] ControlPoints {get; set;} 

        public ObjectController(ObjectManager manager) : base(manager)
        {
            Sprite = new Sprite();
            Unselect = false;

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
            Sprite.Scale = new Vector(12);
            Sprite.Depth = .3f;
            
        }

        

        protected override void addInitialScripts()
        {
        }
    }

}
