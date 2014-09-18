using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;


namespace Whiskey2D.Core
{
    public interface InputSource
    {

        Dictionary<Keys, bool> getAllKeysDown();

    }
}
