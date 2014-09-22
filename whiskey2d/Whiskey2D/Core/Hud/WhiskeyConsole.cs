using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Hud.Commands;

namespace Whiskey2D.Core.Hud
{
    class WhiskeyConsole
    {



        private TextBox textBox;
        private TextBox inputBox;

        private RealKeyBoard keyBoard;
        private Dictionary<Keys, bool> oldKeys, currentKeys;
        private Dictionary<Keys, int> keyTimes;

        private Dictionary<string, ConsoleCommand> commandTable;

        public WhiskeyConsole()
        {
            this.textBox = new TextBox();
            this.textBox.Position = new Vector2(2, 2);
            this.textBox.Size = new Vector2(GameManager.getInstance().ScreenWidth-4, 200);
            this.textBox.TextSize = .8f;
            this.textBox.BackGroundColor = new Color(80, 20, 40, 70);

            this.inputBox = new TextBox();
            this.inputBox.Position = new Vector2(2, 210);
            this.inputBox.Size = new Vector2(GameManager.getInstance().ScreenWidth-4, 40);
            this.inputBox.TextSize = .8f;
            this.inputBox.BackGroundColor = new Color(80, 20, 40, 70);
            keyBoard = new RealKeyBoard();
            oldKeys = keyBoard.getAllKeysDown();
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

        }

        public void addCommand(ConsoleCommand command)
        {
            commandTable.Add(command.CommandName, command);
        }

        public List<string> getCommandNames()
        {
            return commandTable.Keys.ToList();
        }

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

        public void writeLine(string line)
        {
            textBox.pushTextFromBottom(line);
        }

        public void update()
        {
            oldKeys = currentKeys;
            currentKeys = keyBoard.getAllKeysDown();

            Keys[] allkeys = (Keys[])Enum.GetValues(typeof(Keys));
            foreach (Keys k in allkeys)
            {
                if (isNewKey(k))
                {
                    inputBox.append(RealKeyBoard.keyToString(k));
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
