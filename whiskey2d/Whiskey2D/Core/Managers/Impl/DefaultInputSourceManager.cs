using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core.Inputs;

namespace Whiskey2D.Core.Managers.Impl
{

    /// <summary>
    /// The inputSource Manager decides what kind of input source the game will be using
    /// </summary>
    public class DefaultInputSourceManager : InputSourceManager
    {

        //private static DefaultInputSourceManager instance = new DefaultInputSourceManager();
        //public static DefaultInputSourceManager getInstance()
        //{
        //    return instance;
        //}


        private RealKeyBoard keyboardSource;
        private ReplayService replSource;

        private InputSource activeSource;
        private InputSource defaultSource;

        public DefaultInputSourceManager()
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
            replSource = new ReplayService(DefaultLogManager.getInstance().getOldLogPath());
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

        public void hotSwapInput(InputSource source)
        {
            
            activeSource = source;
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
