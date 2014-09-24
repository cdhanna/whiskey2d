using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// The inputSource Manager decides what kind of input source the game will be using
    /// </summary>
    public class InputSourceManager
    {

        private static InputSourceManager instance = new InputSourceManager();
        public static InputSourceManager getInstance()
        {
            return instance;
        }


        private RealKeyBoard keyboardSource;
        private ReplayService replSource;

        private InputSource activeSource;
        private InputSource defaultSource;

        private InputSourceManager()
        {
            keyboardSource = new RealKeyBoard();
            activeSource = keyboardSource;
            defaultSource = keyboardSource;
        }


        /// <summary>
        /// Get the input source that will provide input to the game
        /// </summary>
        /// <returns>the current input source</returns>
        public InputSource getSource()
        {
            return activeSource;
        }

        public void setRegularSource(InputSource inputSource)
        {
            defaultSource = inputSource;
        }

        public void resetRegularSource()
        {
            defaultSource = keyboardSource;
        }

        /// <summary>
        /// Ask the InputSource Manager to switch the input source to a replay service
        /// </summary>
        public void requestReplay()
        {
            replSource = new ReplayService(LogManager.getInstance().getOldLogPath());
            activeSource = replSource;
            GameManager.getInstance().reset();
        }

        /// <summary>
        /// Ask the InputSource Manager to switch the input source to the default one (the keyboard)
        /// </summary>
        public void requestRegular()
        {
            activeSource = defaultSource;
            GameManager.getInstance().reset();
        }

        /// <summary>
        /// Update the InputSource manager
        /// </summary>
        public void update()
        {
         
            if (replSource != null && replSource.ReplayOver)
            {
                activeSource = defaultSource;
                replSource = null;
            }

            if (replSource != null)
            {
                replSource.update();
            }

        }

    }
}
