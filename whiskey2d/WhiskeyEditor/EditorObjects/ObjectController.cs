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
        public bool Unselect { get; set; }
        public EditorLevel CurrentLevel { get; set; }
    

        public ObjectController(ObjectManager manager) : base(manager)
        {
            Sprite = new Sprite();
            Unselect = false;

        }

        protected override void addInitialScripts()
        {
            addScript(new DragControllerScript());
            addScript(new SelectScript());
            addScript(new CopyPasteScript());
        }
    }
}
