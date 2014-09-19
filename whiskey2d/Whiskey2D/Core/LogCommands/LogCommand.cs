using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Core.LogCommands
{
    abstract class LogCommand
    {
        //TODO less hacky
        private static Dictionary<string, LogCommand> typeTable = new Dictionary<string, LogCommand>();

        static LogCommand()
        {
            typeTable.Add("LOG", new LogMessage(0, LogManager.LogLevel.DEBUG, ""));
            typeTable.Add("KEY", new InputCommand(0, 0, null));
        }


        private string name;
        private long time;

        /// <summary>
        /// Create a log entry 
        /// </summary>
        /// <param name="time">the time at which the log entry was created </param>
        /// <param name="name">the name of the log entry</param>
        public LogCommand(long time, string name)
        {
            this.name = name;
            this.time = time;

        }

        /// <summary>
        /// The name of the log entry. This is the type of the log entry. 
        /// </summary>
        public string Name { get { return this.name; } }

        /// <summary>
        /// The time at which the log entry happened
        /// </summary>
        public long Time { get { return this.time; } }

        /// <summary>
        /// convert the log command to a string, to entry into a log file
        /// </summary>
        /// <returns></returns>
        public string toCommand()
        {

            string c = time + "\t\t| " + name + " | " + toCommandText();
            return c;

        }

        /// <summary>
        /// convert a string back into a log command, for interpretation
        /// </summary>
        /// <param name="time">the time at which the log command should have happened</param>
        /// <param name="comm">the string on the log file</param>
        /// <returns></returns>
        protected abstract LogCommand fromCommand(long time, string comm);
        

        protected abstract string toCommandText();

      
        /// <summary>
        /// convert a line in a log file back into a logcommand
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static LogCommand parse(string line)
        {
            string[] parts = line.Split('|');   //break into parts
            string timePart = parts[0].Trim();  //get time string
            string typePart = parts[1].Trim();  //get type string
            string commPart = parts[2].Trim();  //get the actual data in the message

            //figure out what to make
            LogCommand commandType = typeTable[typePart];

            LogCommand command = commandType.fromCommand(int.Parse(timePart), commPart);


            return command;
        }

    }
}
