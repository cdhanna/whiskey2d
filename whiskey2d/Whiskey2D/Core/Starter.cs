using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// A temporary class that a designer must implement to start their game. When the GameManager loads, it will call the first implementation
    /// of Starter it finds inside a user game.dll. 
    /// </summary>
    public abstract class Starter
    {
        
        /// <summary>
        /// Use this method to run startup initilization of the game
        /// </summary>
        public abstract void start();
    }
}
