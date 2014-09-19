using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Core.LogCommands
{
    abstract class LogCommand
    {

        private static Dictionary<string, LogCommand> typeTable = new Dictionary<string, LogCommand>();

        static LogCommand()
        {
            typeTable.Add("LOG", new LogMessage(0, LogManager.LogLevel.DEBUG, ""));
            typeTable.Add("KEY", new InputCommand(0, 0, null));
        }


        private string name;
        private long time;

        public LogCommand(long time, string name)
        {
            this.name = name;
            this.time = time;

            //if (!typeTable.ContainsKey(name))
            //{
            //    typeTable.Add(name, this);
            //}


        }

        public string Name { get { return this.name; } }
        public long Time { get { return this.time; } }


        public string toCommand()
        {

            string c = time + "\t\t| " + name + " | " + toCommandText();
            return c;

        }

        public abstract LogCommand fromCommand(long time, string comm);
        

        protected abstract string toCommandText();

      

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
