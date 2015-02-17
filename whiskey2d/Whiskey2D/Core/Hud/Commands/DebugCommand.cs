using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    /// <summary>
    /// The debugcommand allows the designer to toggle the debug window on/off
    /// </summary>
    class DebugCommand : ConsoleCommand
    {
        public DebugCommand() : base("debug") { }

        public override void run(WhiskeyConsole console, string[] args)
        {

            if (args.Length == 2)
            {
                string arg = args[1];

                arg = arg.ToLower();

                if (arg.Equals("0") || arg.Equals("n") || arg.Equals("off") || arg.Equals("no"))
                    HudManager.Instance.DebugVisible = false;
                else if (arg.Equals("1") || arg.Equals("y") || arg.Equals("yes") || arg.Equals("on"))
                    HudManager.Instance.DebugVisible = true;
            }

            

        }
    }
}
