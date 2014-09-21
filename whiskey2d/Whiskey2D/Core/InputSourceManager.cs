using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    class InputSourceManager
    {

        private static InputSourceManager instance = new InputSourceManager();
        public static InputSourceManager getInstance()
        {
            return instance;
        }


        private RealKeyBoard keyboardSource;
        private ReplayService replSource;

        private InputSource source;

        private InputSourceManager()
        {
            keyboardSource = new RealKeyBoard();
            source = keyboardSource;
            //replSource = new ReplayService("");
        }


        

        //public void setInputSource(InputSource inputSource)
        //{

        //}

        //public InputSource getInputSource()
        //{

        //}


        public InputSource getSource()
        {
            return source;
        }

        public void update()
        {


            if (keyboardSource.getAllKeysDown()[Microsoft.Xna.Framework.Input.Keys.R] && source != replSource)
            {
               
                replSource = new ReplayService(LogManager.getInstance().getOldLogPath());
                source = replSource;
                GameManager.getInstance().reset();

                
            }

            if (keyboardSource.getAllKeysDown()[Microsoft.Xna.Framework.Input.Keys.P])
            {
                source = keyboardSource;
                GameManager.getInstance().reset();
            }

            if (replSource != null && replSource.ReplayOver)
            {
                source = keyboardSource;
                replSource = null;
            }

            if (replSource != null)
            {
                replSource.update();
            }

        }

    }
}
