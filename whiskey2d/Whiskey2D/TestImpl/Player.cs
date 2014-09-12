using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Whiskey2D.Core;
namespace Whiskey2D.TestImpl
{
    class Player : GameObject
    {

        //public Player()
        //{
        //    State = new CharacterState();
        //    addScript( new PlayerMoveScript());
        //}

       // public Vec

        protected override Script getInitialScript()
        {
            return new PlayerMoveScript();
        }

    }
}
