using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    /// <summary>
    /// GameObjectCounter will count how many GameObjects exist in ObjectManager, and display it to the designer
    /// </summary>
    class GameObjectCounterCommand : ConsoleCommand
    {
        public GameObjectCounterCommand() : base("countObjects") { }

        public override void run(WhiskeyConsole console, string[] args)
        {

            console.writeLine("current object count: " + GameManager.Objects.getAllObjects().Count);


        }
    }
}
