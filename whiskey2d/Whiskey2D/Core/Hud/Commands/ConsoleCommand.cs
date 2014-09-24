using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    /// <summary>
    /// A console command can be run from the WhiskeyConsole
    /// </summary>
    abstract class ConsoleCommand
    {

        private string commandName;

        /// <summary>
        /// Create a new Command with a given name. When a user enters that name, this command will be used to process the command
        /// </summary>
        /// <param name="commandName"></param>
        public ConsoleCommand(string commandName)
        {
            this.commandName = commandName;
        }

        /// <summary>
        /// The name of the command
        /// </summary>
        public string CommandName { get { return this.commandName; } }

        /// <summary>
        /// Process the command with given arguements from the console
        /// </summary>
        /// <param name="console">The console</param>
        /// <param name="args">The first element will always be equal to CommandName. After that, the line of text that the user entered on the console was space delim'd and sent as args</param>
        public abstract void run(WhiskeyConsole console, string[] args);

        

    }
}
