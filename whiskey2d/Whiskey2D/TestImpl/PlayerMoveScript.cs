using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.TestImpl
{
    class PlayerMoveScript : Script<Player>
    {
        

        public override void onStart()
        {
           // throw new NotImplementedException();
        }

        public override void onUpdate()
        {
            //Player plr = (Player)this.GameObject;


           
            int x= this.GameObject.test;
            
            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                GameObject.Position = new Microsoft.Xna.Framework.Vector2(GameObject.Position.X + 1, GameObject.Position.Y);

            if (InputManager.getInstance().isKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                GameObject.Position = new Microsoft.Xna.Framework.Vector2(GameObject.Position.X - 1, GameObject.Position.Y);
        }



       
    }
}
