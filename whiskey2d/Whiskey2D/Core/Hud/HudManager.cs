using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core.LogCommands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Inputs;


namespace Whiskey2D.Core.Hud
{
    /// <summary>
    /// The HudManager is responsible for keeping track of all HUD state, including the debug window and the console
    /// </summary>
    public class HudManager
    {
        private static HudManager instance = new HudManager();
        public static HudManager getInstance()
        {
            return instance;
        }

        private List<TextLine> textLines;
        private List<Box> boxes;

        private TextBox debugWindow;
        private WhiskeyConsole console;
        private RealKeyBoard keyboard;
        private Dictionary<Keys, bool> oldKeys, newKeys;
        private bool consoleMode;

        private HudManager()
        {
            
        }

        /// <summary>
        /// initialize the HUD
        /// </summary>
        public void init()
        {
            textLines = new List<TextLine>();
            boxes = new List<Box>();

            DebugLevel = LogLevel.DEBUG;

            debugWindow = new TextBox();
            debugWindow.Position = new Vector2(2, GameManager.getInstance().ScreenHeight- 100);
            debugWindow.Size = new Vector2(GameManager.getInstance().ScreenWidth-4, 98);
            debugWindow.BackGroundColor = Color.Transparent;
            debugWindow.BorderColor = Color.Transparent;
            debugWindow.TextColor = Color.White;
            debugWindow.TextSize = .8f;
            keyboard = new RealKeyBoard();
            console = new WhiskeyConsole();
            ConsoleMode = false;
        }

        /// <summary>
        /// Gets and Sets if the Console is visible. If the Console is on, the game is paused.
        /// </summary>
        public bool ConsoleMode
        {
            get
            {
                return this.consoleMode;
            }
            set
            {
                this.consoleMode = value;
                this.console.Visible = value;
            }
        }

        /// <summary>
        /// Gets and Sets if the DebugWindow is visible. 
        /// </summary>
        public bool DebugVisible
        {
            get
            {
                return debugWindow.Visible;
            }
            set
            {
                debugWindow.Visible = value;
            }
        }

        /// <summary>
        /// Sets the current log level of the console. 
        /// </summary>
        public LogLevel DebugLevel { get; set; }

        /// <summary>
        /// Gets and Sets the debug window text color
        /// </summary>
        public Color DebugColor
        {
            get
            {
                return debugWindow.TextColor;
            }
            set
            {
                debugWindow.TextColor = value;
            }
        }

        /// <summary>
        /// Close the HudManager
        /// </summary>
        public void close()
        {
            textLines.Clear();
            boxes.Clear();
            debugWindow.clearText();

        }

        /// <summary>
        /// update the HudManager
        /// </summary>
        public void update()
        {
            newKeys = keyboard.getAllKeysDown();

            if (newKeys[Keys.OemTilde] && !oldKeys[Keys.OemTilde])
            {
                ConsoleMode = !ConsoleMode;
            }

            if (ConsoleMode)
            {
                console.update();
            }

            oldKeys = newKeys;
        }


        /// <summary>
        /// adds a line of text
        /// </summary>
        /// <param name="line"></param>
        public void addTextLine(TextLine line)
        {
            this.textLines.Add(line);
        }

        /// <summary>
        /// removes a line of text
        /// </summary>
        /// <param name="line"></param>
        public void removeTextLine(TextLine line)
        {
            this.textLines.Remove(line);
        }

        /// <summary>
        /// gets all of the lines of text
        /// </summary>
        /// <returns></returns>
        public List<TextLine> getAllTextLines()
        {
            return textLines;
        }

        /// <summary>
        /// adds a box
        /// </summary>
        /// <param name="box"></param>
        public void addBox(Box box)
        {
            this.boxes.Add(box);
        }

        /// <summary>
        /// removes a box
        /// </summary>
        /// <param name="box"></param>
        public void removeBox(Box box)
        {
            this.boxes.Remove(box);
        }

        /// <summary>
        /// gets all of the boxes
        /// </summary>
        /// <returns></returns>
        public List<Box> getAllBoxes()
        {
            return this.boxes;
        }




        public void writeMessage(LogMessage message)
        {
            if (message.Level == DebugLevel)
            {
                debugWindow.pushTextFromBottom(message.Time + "> " + message.Message);
            }

            else if (message.Level == LogLevel.ERROR)
            {
                debugWindow.pushTextFromBottom(message.Time + "> " + message.Message).ForEach( l => l.Color = Color.Red);
            }

        }

    }
}
