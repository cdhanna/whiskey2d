using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;

namespace Whiskey2D.Core.LogCommands
{
    /// <summary>
    /// The LogMessage writes Designer messages to the log file
    /// </summary>
    class LogMessage : LogCommand
    {
        private static LogMessage instance = new LogMessage();



        private LogLevel level;
        private string message;

        /// <summary>
        /// Creates a new LogMessage
        /// </summary>
        /// <param name="time">The time at which the log message was given</param>
        /// <param name="level">The level of the log message</param>
        /// <param name="message">The actual designer message</param>
        public LogMessage(long time, LogLevel level, string message) : base(time, "LOG"){
            this.level = level;
            this.message = message;
        }

        protected LogMessage() : base(0, "LOG") { }

        /// <summary>
        /// The designer message
        /// </summary>
        public string Message { get { return this.message; } }

        /// <summary>
        /// The level of the log event
        /// </summary>
        public LogLevel Level { get { return this.level; } }

        protected override string toCommandText()
        {
            return level.ToString() + "\t$ " + message;
        }

        protected override LogCommand fromCommand(long time, string text)
        {
            string[] parts = text.Split('$');
            string levelPart = parts[0].Trim();
            string messagePart = parts[1].Trim();
            return new LogMessage(time, (LogLevel)Enum.Parse(typeof(LogLevel), levelPart), messagePart);
        }
    }
}
