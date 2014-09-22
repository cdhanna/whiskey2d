using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    class ExitCommand : ConsoleCommand
    {
        public ExitCommand() : base("exit") { }




        public override void run(WhiskeyConsole console, string[] args)
        {
            console.writeLine("SYS EXIT");
            GameManager.getInstance().close();
        }
    }
}
