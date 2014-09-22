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

            if (args.Length == 2)
            {
                try
                {
                    int m = int.Parse(args[1]);
                    GameManager.getInstance().TargetElapsedTime = new TimeSpan(0, 0, 0, 0, m);
                }
                catch (Exception e)
                {
                    console.writeLine("not a integer");
                }
                
            }
        }
    }
}
