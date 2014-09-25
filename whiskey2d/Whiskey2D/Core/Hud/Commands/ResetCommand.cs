using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    /// <summary>
    /// ResetCommand resets the game to the initial configuration
    /// </summary>
    class ResetCommand : ConsoleCommand
    {
        public ResetCommand() : base("reset") { }



        public override void run(WhiskeyConsole console, string[] args)
        {
            GameManager.InputSource.requestRegular();
            HudManager.getInstance().ConsoleMode = false;
            GameManager.getInstance().TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 17);
        }
    }
}
