using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Hud.Commands;
using Whiskey2D.Core.Inputs;
namespace Whiskey2D.Core.Hud
{
    /// <summary>
    /// The WhiskeyConsole allows commands to be entered and interp'd into the Whiskey System
    /// </summary>
    class WhiskeyConsole
    {

        private TextBox textBox;
        private TextBox inputBox;

        private RealKeyBoard keyBoard;
        private Dictionary<Keys, bool> oldKeys, currentKeys;
        private Dictionary<Keys, int> keyTimes;

        private Dictionary<string, ConsoleCommand> commandTable;

        /// <summary>
        /// Create a new WhiskeyConsole with default values
        /// </summary>
        public WhiskeyConsole()
        {
            this.textBox = new TextBox();
            this.textBox.Position = new Vector2(2, 2);
            this.textBox.Size = new Vector2(GameManager.Instance.WindowScreenWidth - 4, 200);
            this.textBox.TextSize = .8f;
            this.textBox.BackGroundColor = new Color(80, 20, 40, 70);

            this.inputBox = new TextBox();
            this.inputBox.Position = new Vector2(2, 210);
            this.inputBox.Size = new Vector2(GameManager.Instance.WindowScreenWidth - 4, 40);
            this.inputBox.TextSize = .8f;
            this.inputBox.BackGroundColor = new Color(80, 20, 40, 70);
            keyBoard = new RealKeyBoard();
            oldKeys = keyBoard.AllDownKeys;
            currentKeys = oldKeys;
            keyTimes = new Dictionary<Keys, int>();
            foreach (Keys k in currentKeys.Keys)
            {
                keyTimes.Add(k, 0);
            }

            commandTable = new Dictionary<string, ConsoleCommand>();

            //default commands
            addCommand(new ExitCommand());
            addCommand(new HelpCommand());
            addCommand(new ReplayCommand());
            addCommand(new ResetCommand());
            addCommand(new DebugCommand());
            addCommand(new ReadValueCommand());
            addCommand(new GameObjectCounterCommand());
        }

        /// <summary>
        /// Add a command to the Console. By adding a command, the console will direct input of that type of command to the given command for processing
        /// </summary>
        /// <param name="command"></param>
        public void addCommand(ConsoleCommand command)
        {
            commandTable.Add(command.CommandName.ToLower(), command);
        }

        /// <summary>
        /// Get a list of all command names that the console is working with
        /// </summary>
        /// <returns></returns>
        public List<string> getCommandNames()
        {
            return commandTable.Keys.ToList();
        }

        /// <summary>
        /// True if the console is visible, false otherwise
        /// </summary>
        public bool Visible
        {
            get
            {
                return textBox.Visible;
            }
            set
            {
                textBox.Visible = value;
                inputBox.Visible = value;
            }
        }

        /// <summary>
        /// writes a line to the console output
        /// </summary>
        /// <param name="line"></param>
        public void writeLine(string line)
        {
            textBox.pushTextFromBottom(line);
        }

        /// <summary>
        /// updates the console
        /// </summary>
        public void update()
        {
            oldKeys = currentKeys;
            currentKeys = keyBoard.AllDownKeys;

            Keys[] allkeys = (Keys[])Enum.GetValues(typeof(Keys));
            bool isShift = currentKeys.Where(x => x.Key == Keys.LeftShift && x.Value).Count() == 1;
            foreach (Keys k in allkeys)
            {
                if (isNewKey(k))
                {
                  
                    inputBox.append(RealKeyBoard.keyToString(k, isShift));
                }
                if (currentKeys[k])
                {
                    keyTimes[k]++;
                }
                else
                {
                    keyTimes[k] = 0;
                }
            }


            if (isNewKey(Keys.Enter))
            {
                submitCommand(inputBox.Text);
                inputBox.clearText();
            }

            if (isNewKey(Keys.Back))
            {
                if (inputBox.Text.Length > 0)
                {
                    inputBox.removeFromEnd();
                }
            }
            

            
        }

        private bool isNewKey(Keys k)
        {
            
            return currentKeys[k] && (!oldKeys[k] || keyTimes[k] > 30);
        }

        private void submitCommand(string command)
        {
            
            textBox.pushTextFromBottom(command);

            string[] parts = command.Split(' ');
            if (parts.Length > 0)
            {
                string commandName = parts[0];
                if (commandTable.ContainsKey(commandName))
                {
                    ConsoleCommand c = commandTable[commandName];
                    c.run(this, parts);
                }
                else
                {
                    textBox.pushTextFromBottom("unknown command");
                }

            }
        }
    }
}
