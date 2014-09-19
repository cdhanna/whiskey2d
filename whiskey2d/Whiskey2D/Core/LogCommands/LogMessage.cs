using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.Core.LogCommands
{
    class LogMessage : LogCommand
    {
        private static LogMessage instance = new LogMessage();


        private string delim = "#";

        private LogManager.LogLevel level;
        private string message;

        public LogMessage(long time, LogManager.LogLevel level, string message) : base(time, "LOG"){
            this.level = level;
            this.message = message;
        }

        protected LogMessage() : base(0, "LOG") { }

        protected override string toCommandText()
        {
            return level.ToString() + "\t# " + message;
        }

        public override LogCommand fromCommand(long time, string text)
        {
            string[] parts = text.Split('#');
            string levelPart = parts[0].Trim();
            string messagePart = parts[1].Trim();
            return new LogMessage(time, (LogManager.LogLevel)Enum.Parse(typeof(LogManager.LogLevel), levelPart), messagePart);
            //throw new NotImplementedException();
        }
    }
}
