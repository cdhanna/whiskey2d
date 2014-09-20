using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.LogCommands
{
    class RandCommand : LogCommand
    {
        private int seed;

        public RandCommand(long time, int seed)
            : base(time, "RAND")
        {
            this.seed = seed;
        }

        public int Seed { get { return this.seed; } }

        protected override LogCommand fromCommand(long time, string comm)
        {
            int s = int.Parse(comm.Trim());
            return new RandCommand(time, s);
        }

        protected override string toCommandText()
        {
            return "" + seed;
        }
    }
}
