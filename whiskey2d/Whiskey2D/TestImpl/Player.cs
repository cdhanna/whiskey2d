using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
namespace Whiskey2D.TestImpl
{
    class Player : GameObject
    {

        public int test;

       // public List<Script<Player>> scripts;

        public Player()
        {
            
            this.addScript( new PlayerMoveScript());
        }


        //public override void update()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
