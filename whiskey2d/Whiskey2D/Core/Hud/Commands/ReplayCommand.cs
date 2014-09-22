using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    class ReplayCommand : ConsoleCommand
    {
        public ReplayCommand() : base("replay") { }



        public override void run(WhiskeyConsole console, string[] args)
        {
            InputSourceManager.getInstance().requestReplay();
            HudManager.getInstance().ConsoleMode = false;
        }
    }
}
