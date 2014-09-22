using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    abstract class ConsoleCommand
    {

        private string commandName;

        public ConsoleCommand(string commandName)
        {
            this.commandName = commandName;
        }

        public string CommandName { get { return this.commandName; } }

        public abstract void run(WhiskeyConsole console, string[] args);

        

    }
}
