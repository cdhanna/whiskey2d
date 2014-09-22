using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    class GameObjectCounterCommand : ConsoleCommand
    {
        public GameObjectCounterCommand() : base("countObjects") { }

        public override void run(WhiskeyConsole console, string[] args)
        {

            console.writeLine("current object count: " + ObjectManager.getInstance().getAllObjects().Count);


        }
    }
}
