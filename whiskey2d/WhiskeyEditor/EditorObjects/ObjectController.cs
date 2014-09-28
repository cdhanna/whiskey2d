using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;

namespace WhiskeyEditor.EditorObjects
{
    class ObjectController : EditorGameObject
    {
        public GameObject Dragging { get; set; }
        public GameObject Selected { get; set; }


        public ObjectController()
        {
            Sprite = new Sprite(GameManager.Renderer.getPixel());
            
        }

        protected override void addInitialScripts()
        {
            addScript(new DragControllerScript());
            addScript(new SelectScript());
        }
    }
}
