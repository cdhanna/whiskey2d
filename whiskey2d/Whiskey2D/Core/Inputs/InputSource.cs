using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;


namespace Whiskey2D.Core.Inputs
{

    /// <summary>
    /// An InputSource provides input to the game.
    /// </summary>
    public interface InputSource
    {
        /// <summary>
        /// Called to allow the input source to set up
        /// </summary>
        void init();

        /// <summary>
        /// Returns a mapping of all Keys to on, or off. 
        /// </summary>
        /// <returns></returns>
        Dictionary<Keys, bool> getAllKeysDown();


        MouseState getMouseState();

    }
}
