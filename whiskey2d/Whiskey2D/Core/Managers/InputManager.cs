using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Whiskey2D.Core.Managers
{
    public interface InputManager
    {

        void init();
        void close();
        void update();

        bool isKeyDown(Keys k);
        bool isNewKeyDown(Keys k);

    }
}
