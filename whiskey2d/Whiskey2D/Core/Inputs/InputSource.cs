using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;


namespace Whiskey2D.Core.Inputs
{

    /// <summary>
    /// An InputSource provides input to the game. An InputSource can be a Keyboard, or a Log file, or something else.
    /// An InputSource provides Instanteous information. In only reports what is happening at the given moment.
    /// </summary>
    public interface InputSource
    {
        /// <summary>
        /// Called to allow the input source to set up
        /// </summary>
        void init();

        /// <summary>
        /// Gets the current down keys
        /// </summary>
        Dictionary<Keys, bool> AllDownKeys { get; }

        /// <summary>
        /// Gets the current MouseState
        /// </summary>
        MouseState MouseState { get; }


    }
}
