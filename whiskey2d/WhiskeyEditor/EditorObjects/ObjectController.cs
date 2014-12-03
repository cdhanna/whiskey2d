using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.EditorObjects
{
    public class ObjectController : EditorGameObject
    {
        public GameObject Dragging { get; set; }
        public GameObject Selected { get; set; }
        public bool Unselect { get; set; }
        public Level CurrentLevel { get; set; }
    

        public ObjectController()
        {
            Sprite = new Sprite();
            Unselect = false;

        }

        protected override void addInitialScripts()
        {
            addScript(new DragControllerScript());
            addScript(new SelectScript());
        }
    }
}
