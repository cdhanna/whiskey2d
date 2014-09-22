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

        public TextBox()
        {
            textStart = new Vector2(2, 2);

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

        public void pushText(string line)
        {
            float lineHeight = (font.MeasureString("A").Y+1) * textSize;
            textStart.Y = size.Y - lineHeight;
            textStart.Y -= lineHeight * lines.Count;
            if (textStart.Y < 2)
            {
            //    textStart.Y = 2;
            }
            //Text = "hello\nthis\nisnew";
            Text = text + line + '\n';
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                clearText();

                text = value;
                string lineText = text;
                Vector2 lineOffset = textStart;

                string textLeft = text;
                
                while (textLeft.Length > 0)
                {
                    float height = (font.MeasureString("A").Y+1) * TextSize;
                    string line = textLeft;

                    int extra = 0;

                    //are there any line breaks?
                    int lineBreakIndex = textLeft.IndexOf('\n');
                    if (lineBreakIndex > -1)
                    {
                        
                        line = textLeft.Substring(0, lineBreakIndex);
                        extra = 1;
                    }

                    //line has been set. Create line
                    textLeft = textLeft.Substring(line.Length + extra).Trim();

                    TextLine textLine = new TextLine();
                    textLine.Color = textColor;
                    textLine.Text = line;
                    textLine.Size = Vector2.One * TextSize;
                    textLine.Position = Position + lineOffset;
                    lines.Add(textLine);
                    Console.WriteLine("TEXT: " + line);
                    if (textLine.Position.Y < position.Y || textLine.Position.Y + height > position.Y + size.Y)
                    {
                        textLine.Visible = false;
                    }
//
                    lineOffset += new Vector2(0, height);
                }


                //while (text.Length > 0)
                //{
                //    Vector2 measure = textSize * font.MeasureString(lineText);
                //    while (measure.X > size.X)
                //    {
                //        int indexOfSpace = lineText.LastIndexOf(' ');
                //        if (indexOfSpace < 0)
                //        {
                //            indexOfSpace = lineText.Length - 1;

                //        }
                //        lineText = lineText.Substring(0, indexOfSpace);

                //        measure = textSize * font.MeasureString(lineText);
                //    }
                //    int newLineIndex = lineText.IndexOf('\n');
                //    if (newLineIndex > -1)
                //    {
                //        lineText = lineText.Substring(0, newLineIndex);
                //        lineText.Replace("\n", "");
                //    }
                    
                    
                //    text = text.Substring(lineText.Length).Trim();

                //    if (text != "")
                //    {


                //        TextLine line = new TextLine();
                //        line.Text = lineText;
                //        line.Color = textColor;
                //        line.Position += Position;
                //        line.Position += lineOffset;
                //        line.Size = Vector2.One * textSize;
                //        lineText = text;

                //        lines.Add(line);
                //        if (line.Position.Y < Position.Y)
                //        {
                //            line.Visible = false;
                //        }
                //    }
                //    lineOffset.Y += measure.Y+1;
                //    //lineOffset.Y = lines.Count * (1 + measure.Y);
                

                   

                //}
                this.text = value;
            }
        }

    }
}
