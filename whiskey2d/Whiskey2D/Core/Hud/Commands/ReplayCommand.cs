using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    /// <summary>
    /// The ReplayCommand will allow the designer to replay the last play session. As an optional arg, they can specifiy how many frames should take place between ticks.
    /// </summary>
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
