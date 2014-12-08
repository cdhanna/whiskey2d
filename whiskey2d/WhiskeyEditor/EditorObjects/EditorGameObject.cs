using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;

namespace WhiskeyEditor.EditorObjects
{
    [Serializable]
    public abstract class EditorGameObject : GameObject
    {

       
       // public GameController Controller { get; private set; }
        public EditorGameObject(ObjectManager manager)//, GameController controller)
            : base(manager)
        {
            
            //Controller = controller;
        }

    }
}
