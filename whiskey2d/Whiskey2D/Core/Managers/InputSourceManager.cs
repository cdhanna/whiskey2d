using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core.Inputs;
namespace Whiskey2D.Core.Managers
{


    /// <summary>
    /// The InputSourceManager provides an InputSource to the InputManager.
    /// </summary>
    public interface InputSourceManager
    {

        /// <summary>
        /// Get the input source that will provide input to the game
        /// </summary>
        /// <returns>the current input source</returns>
        InputSource getSource();

        /// <summary>
        /// Sets the regular input source
        /// </summary>
        /// <param name="inputSource"></param>
        void setRegularSource(InputSource inputSource);

        /// <summary>
        /// Ask the InputSource Manager to reset, using the regular source
        /// </summary>
        void resetRegularSource();

        /// <summary>
        /// Ask the InputSource Manager to switch the input source to a replay service
        /// </summary>
        void requestReplay();
        /// <summary>
        /// Ask the InputSource Manager to switch the input source to the default one (the keyboard)
        /// </summary>
        void requestRegular();

        /// <summary>
        /// Ask the InputSource Manager to switch the input source to a new input source, without reseting.
        /// </summary>
        /// <param name="inputSource">Some new InputSource</param>
        void hotSwapInput(InputSource inputSource);

        /// <summary>
        /// Update the InputSource manager
        /// </summary>
        void update();

    }
}
