using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Whiskey2D.Core.Inputs;

namespace Whiskey2D.Core.Managers
{
    /// <summary>
    /// The InputManager gives access to what input is coming into the game. 
    /// </summary>
    public interface InputManager
    {
        /// <summary>
        /// Initializes the InputManager
        /// </summary>
        void init();

        /// <summary>
        /// Initializes the InputManager
        /// </summary>
        /// <param name="source">A custom InputSourceManager to provide an Input source. By default, the GameManager's InputSourceManager is used</param>
        void init(InputSourceManager source);

        /// <summary>
        /// Closes the InputManager
        /// </summary>
        void close();

        /// <summary>
        /// Updates the InputManager
        /// </summary>
        void update();

        /// <summary>
        /// Determines if a specific Key is being pressed down.
        /// </summary>
        /// <param name="k">Some Key</param>
        /// <returns>True, if the key is being held down, False otherwise</returns>
        bool isKeyDown(Keys k);

        /// <summary>
        /// Determines if a specific Key has just been pressed
        /// </summary>
        /// <param name="k">Some Key</param>
        /// <returns>True if the key has just been pushed, False otherwise</returns>
        bool isNewKeyDown(Keys k);


        /// <summary>
        /// Determines if a Mouse Key is being pressed down
        /// </summary>
        /// <param name="b">Some MouseButton</param>
        /// <returns>True if the key is being held down, False otherwise</returns>
        bool isMouseDown(MouseButtons b);

        /// <summary>
        /// Determines if a Mouse Key has just been pushed
        /// </summary>
        /// <param name="b">Some MouseButton</param>
        /// <returns>True if the Key has just been pressed, False otherwise</returns>
        bool isNewMouseDown(MouseButtons b);

        /// <summary>
        /// Determines if the Mouse wheel just scrolled up
        /// </summary>
        bool ScrolledUp { get; }

        /// <summary>
        /// Determines if the Mouse wheel just scrolled down
        /// </summary>
        bool ScrolledDown { get; }

        /// <summary>
        /// Gets the position of the mouse, in ScreenCoordinates
        /// </summary>
        Vector MousePosition { get; }

        /// <summary>
        /// Gets the position of the mouse, in GameCoordinates
        /// </summary>
        Vector MouseGamePosition { get; }

        /// <summary>
        /// Gets the mouse wheel delta of the mouse
        /// </summary>
        int MouseWheelDelta { get; }


    }
}
