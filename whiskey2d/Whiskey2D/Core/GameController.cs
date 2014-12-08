using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{
    public interface GameController
    {

        

        GameObject SelectedGob { get; set; }
        void Exit();
    }
}
