using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    class HelpCommand : ConsoleCommand
    {
        public HelpCommand() : base("help") { }

        public override void run(WhiskeyConsole console, string[] args)
        {
            List<String> commands = console.getCommandNames();
            foreach (string command in commands)
            {
                if (!command.Equals("help"))
                    console.writeLine(" " + command);
            }
        }
    }
}
