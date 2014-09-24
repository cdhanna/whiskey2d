using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.LogCommands
{
    /// <summary>
    /// Tells the log file what the random seed should be
    /// </summary>
    class RandCommand : LogCommand
    {
        private int seed;

        /// <summary>
        /// Creates a new RandomCommand
        /// </summary>
        /// <param name="time">the time at which the seed was changed</param>
        /// <param name="seed">the seed number </param>
        public RandCommand(long time, int seed)
            : base(time, "RAND")
        {
            this.seed = seed;
        }

        /// <summary>
        /// The seed number
        /// </summary>
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
