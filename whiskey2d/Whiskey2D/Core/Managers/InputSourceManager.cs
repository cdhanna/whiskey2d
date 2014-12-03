using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core.Inputs;
namespace Whiskey2D.Core.Managers
{
    public interface InputSourceManager
    {

        /// <summary>
        /// Get the input source that will provide input to the game
        /// </summary>
        /// <returns>the current input source</returns>
        InputSource getSource();
        void setRegularSource(InputSource inputSource);
        void resetRegularSource();

        /// <summary>
        /// Ask the InputSource Manager to switch the input source to a replay service
        /// </summary>
        void requestReplay();
        /// <summary>
        /// Ask the InputSource Manager to switch the input source to the default one (the keyboard)
        /// </summary>
        void requestRegular();

        void hotSwapInput(InputSource inputSource);

        /// <summary>
        /// Update the InputSource manager
        /// </summary>
        void update();

    }
}
