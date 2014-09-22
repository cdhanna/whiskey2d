using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core.Hud
{
    class TextBox
    {

        private SpriteFont font;
        private Vector2 position;
        private Vector2 size;
        private Vector2 textStart;
        private float textSize;
        private List<TextLine> lines;
        private Box background;
        private Box borderLeft, borderRight, borderTop, borderLow;
        private string text;
        private float borderSize;
        private bool visible;
        private Color textColor;
        private Vector2 lineOffset;
        private int maxSize = 10;


        public TextBox()
        {
            textStart = new Vector2(2, 2);
            lineOffset = textStart;

            textColor = Color.White;
            position = Vector2.Zero;
            size = new Vector2(100, 50);
            font = ResourceManager.getInstance().getDefaultFont();
            textSize = 1;
            text = "";
            lines = new List<TextLine>();
            background = new Box();
            background.Position = position;
            background.Size = size;
            background.Color = Color.DimGray;

            borderSize = 2;
            borderLeft = new Box();
            borderLeft.Position = position - Vector2.One * borderSize;
            borderLeft.Size = new Vector2(borderSize, size.Y + borderSize*2);
            borderLeft.Depth = .899f;
            borderLeft.Color = Color.Black;
            borderRight = new Box();
            borderRight.Position = position +new Vector2(size.X, 0);
            borderRight.Size = new Vector2(borderSize, size.Y);
            borderRight.Depth = .899f;
            borderRight.Color = Color.Black;
            borderTop = new Box();
            borderTop.Position = position - Vector2.One * borderSize;
            borderTop.Size = new Vector2(size.X + borderSize*2, borderSize);
            borderTop.Depth = .899f;
            borderTop.Color = Color.Black;
            borderLow = new Box();
            borderLow.Position = position + new Vector2(-borderSize, size.Y);
            borderLow.Size = new Vector2(size.X + borderSize * 2, borderSize);
            borderLow.Depth = .899f;
            borderLow.Color = Color.Black;

            Visible = true;
        }

        public void clearText()
        {
            foreach (TextLine l in lines)
            {
                l.close();
            }
            lines.Clear();
            text = "";
        }

        public bool Visible
        {

            get
            {
                return this.visible;
            }
            set
            {
                this.visible = value;
                background.Visible = value;
                borderLeft.Visible = value;
                borderRight.Visible = value;
                borderTop.Visible = value;
                borderLow.Visible = value;
                lines.ForEach((l) => { l.Visible = value; });
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                this.background.Position = value;
                borderLeft.Position = position - Vector2.One * borderSize;
                borderRight.Position = position + new Vector2(size.X, 0) ;
                borderLeft.Size = new Vector2(borderSize, size.Y + borderSize * 2);
                borderRight.Size = new Vector2(borderSize, size.Y);

                borderTop.Position = position - Vector2.One * borderSize;
                borderTop.Size = new Vector2(size.X + borderSize*2, borderSize);
                borderLow.Position = position + new Vector2(-borderSize, size.Y);
                borderLow.Size = new Vector2(size.X + borderSize * 2, borderSize);
                Text = this.text;
            }
        }

        public Vector2 Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                this.background.Size = value;
                borderLeft.Position = position - Vector2.One * borderSize;
                borderRight.Position = position + new Vector2(size.X, 0) ;
                borderLeft.Size = new Vector2(borderSize, size.Y + borderSize * 2);
                borderRight.Size = new Vector2(borderSize, size.Y);

                borderTop.Position = position - Vector2.One * borderSize;
                borderTop.Size = new Vector2(size.X + borderSize*2, borderSize);
                borderLow.Position = position + new Vector2(-borderSize, size.Y);
                borderLow.Size = new Vector2(size.X + borderSize * 2, borderSize);
                Text = this.text;
            }
        }
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                this.lines.ForEach((l) => { l.Color = value; });
            }
        }
        public Color BackGroundColor
        {
            get
            {
                return this.background.Color;
            }
            set
            {
                this.background.Color = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return this.borderLeft.Color;
            }
            set
            {
                this.borderLeft.Color = value;
                this.borderRight.Color = value;
                this.borderTop.Color = value;
                this.borderLow.Color = value;
            }
        }

        public float TextSize
        {
            get
            {
                return this.textSize;
            }
            set
            {
                this.textSize = value;
                Text = this.text;
            }
        }

        private float getHeight()
        {
            return font.MeasureString("A").Y * textSize;
        }

        public void pushTextFromBottom(string moreText)
        {
            this.addLinesToEnd(this.convertToLines(moreText));
            this.text += moreText;
            //this.textStart = new Vector2(2, -(font.MeasureString("A").Y + 1) * lines.Count);
            this.textStart = new Vector2(2, size.Y - (getHeight()* lines.Count)) ;
            this.formatLines();

        }

        public void removeFromEnd()
        {
            if (lines.Count > 0)
            {
                TextLine last = lines[lines.Count - 1];
                string t = last.Text;
                if (t.Length > 0)
                {
                    last.Text = t.Substring(0, t.Length - 1);
                }
                if (last.Text.Length == 0)
                {
                    lines.Remove(last);
                    last.close();
                }
            }
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }

        }

        public void append(string moreText)
        {
            this.textStart = new Vector2(2, 2);

            if (lines.Count > 0)
            {
                TextLine last = this.lines[lines.Count - 1];
                lines.Remove(last);
                this.addLinesToEnd(this.convertToLines(last.Text + moreText));
                last.close();
            } else this.addLinesToEnd(this.convertToLines(moreText));
            
            this.formatLines();
            this.text = text + moreText;
            
        }

        public void prepend(string moreText)
        {
            this.textStart = new Vector2(2, 2);
            this.insertLines(0, this.convertToLines(moreText));
            this.formatLines();
            this.text = moreText + text;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.clearText();
                this.text = value;
                this.addLinesToEnd(this.convertToLines(value));
                this.formatLines();
            }
        }

        private void addLinesToEnd(List<TextLine> lines)
        {
            this.lines.AddRange(lines);
        }

        private void insertLines(int index, List<TextLine> lines)
        {
            this.lines.InsertRange(index, lines);
        }

        private void formatLines()
        {
            lineOffset = textStart;
            float height = getHeight();

            for (int i = 0; i < lines.Count; i++)
            {
                TextLine line = lines[i];

                line.Position = Position + lineOffset;
                lineOffset.Y += height;
                if (line.Position.Y < position.Y || line.Position.Y + height > position.Y + size.Y)
                {
                    //line.Color = Color.Red;
                    line.Visible = false;
                }
            }




        }

        private List<TextLine> convertToLines(string text)
        {
            List<TextLine> lines = new List<TextLine>();

            while (text.Length > 0)
            {
                string line = "";
                int index = text.Length;


                int newLineIndex = text.IndexOf("\n");
                if (newLineIndex > -1)
                {
                    index = newLineIndex + 1;
                }

                line = text.Substring(0, index);

                float width = font.MeasureString(line).X * textSize;
                while (width > size.X)
                {

                    int makeSmallerIndex = line.Length - 1;
                    if (line.Contains(" "))
                    {
                        makeSmallerIndex = Math.Min(makeSmallerIndex, line.LastIndexOf(" ")+1);
                    }

                    line = line.Substring(0, makeSmallerIndex);
                    width = font.MeasureString(line).X * textSize;
                }
                
                lines.Add(createTextLine(line));
                text = text.Substring(line.Length);

            }
            
           
            return lines;
        }

        private TextLine createTextLine(string text)
        {
            TextLine temp = new TextLine();
            temp.Size = textSize * Vector2.One;
            temp.Color = textColor;
            temp.Text = text;
            return temp;
        }

    }
}
