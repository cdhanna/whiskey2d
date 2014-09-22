using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core.LogCommands;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core.Hud
{
    class HudManager
    {
        private static HudManager instance = new HudManager();
        public static HudManager getInstance()
        {
            return instance;
        }

        private List<TextLine> textLines;
        private List<Box> boxes;

        private TextBox debugWindow;


        private HudManager()
        {
            
        }


        public void init()
        {
            textLines = new List<TextLine>();
            boxes = new List<Box>();

            DebugLevel = LogLevel.DEBUG;

            debugWindow = new TextBox();
            debugWindow.Position = new Vector2(2, GameManager.getInstance().ScreenHeight- 300);
            debugWindow.Size = new Vector2(GameManager.getInstance().ScreenWidth-4, 298);
            debugWindow.BackGroundColor = Color.Transparent;
            debugWindow.BorderColor = Color.Transparent;
        }

        public void close()
        {
            textLines.Clear();
            boxes.Clear();
        }


        public void addTextLine(TextLine line)
        {
            this.textLines.Add(line);
        }

        public void removeTextLine(TextLine line)
        {
            this.textLines.Remove(line);
        }

        public List<TextLine> getAllTextLines()
        {
            return textLines;
        }


        public void addBox(Box box)
        {
            this.boxes.Add(box);
        }
        public void removeBox(Box box)
        {
            this.boxes.Remove(box);
        }
        public List<Box> getAllBoxes()
        {
            return this.boxes;
        }


        public bool DebugVisible { get; set; }
        public LogLevel DebugLevel { get; set; }

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


        public void writeMessage(LogMessage message)
        {
            if (message.Level == DebugLevel)
            {
                debugWindow.pushText("");
                //debugWindow.Text += message.Message + "\n";
            }

        }

    }
}
