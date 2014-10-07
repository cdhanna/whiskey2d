using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    using XnaColor = Microsoft.Xna.Framework.Color;

    [Serializable]
    public struct Color
    {
        #region presets
        static Color()
        {
            TransparentBlack = XnaColor.TransparentBlack;
            Transparent = XnaColor.Transparent;
            AliceBlue = XnaColor.AliceBlue;
            AntiqueWhite = XnaColor.AntiqueWhite;
            Aqua = XnaColor.Aqua;
            Aquamarine = XnaColor.Aquamarine;
            Azure = XnaColor.Azure;
            Beige = XnaColor.Beige;
            Bisque = XnaColor.Bisque;
            Black = XnaColor.Black;
            BlanchedAlmond = XnaColor.BlanchedAlmond;
            Blue = XnaColor.Blue;
            BlueViolet = XnaColor.BlueViolet;
            Brown = XnaColor.Brown;
            BurlyWood = XnaColor.BurlyWood;
            CadetBlue = XnaColor.CadetBlue;
            Chartreuse = XnaColor.Chartreuse;
            Chocolate = XnaColor.Chocolate;
            Coral = XnaColor.Coral;
            CornflowerBlue = XnaColor.CornflowerBlue;
            Cornsilk = XnaColor.Cornsilk;
            Crimson = XnaColor.Crimson;
            Cyan = XnaColor.Cyan;
            DarkBlue = XnaColor.DarkBlue;
            DarkCyan = XnaColor.DarkCyan;
            DarkGoldenrod = XnaColor.DarkGoldenrod;
            DarkGray = XnaColor.DarkGray;
            DarkGreen = XnaColor.DarkGreen;
            DarkKhaki = XnaColor.DarkKhaki;
            DarkMagenta = XnaColor.DarkMagenta;
            DarkOliveGreen = XnaColor.DarkOliveGreen;
            DarkOrange = XnaColor.DarkOrange;
            DarkOrchid = XnaColor.DarkOrchid;
            DarkRed = XnaColor.DarkRed;
            DarkSalmon = XnaColor.DarkSalmon;
            DarkSeaGreen = XnaColor.DarkSeaGreen;
            DarkSlateBlue = XnaColor.DarkSlateBlue;
            DarkSlateGray = XnaColor.DarkSlateGray;
            DarkTurquoise = XnaColor.DarkTurquoise;
            DarkViolet = XnaColor.DarkViolet;
            DeepPink = XnaColor.DeepPink;
            DeepSkyBlue = XnaColor.DeepSkyBlue;
            DimGray = XnaColor.DimGray;
            DodgerBlue = XnaColor.DodgerBlue;
            Firebrick = XnaColor.Firebrick;
            FloralWhite = XnaColor.FloralWhite;
            ForestGreen = XnaColor.ForestGreen;
            Fuchsia = XnaColor.Fuchsia;
            Gainsboro = XnaColor.Gainsboro;
            GhostWhite = XnaColor.GhostWhite;
            Gold = XnaColor.Gold;
            Goldenrod = XnaColor.Goldenrod;
            Gray = XnaColor.Gray;
            Green = XnaColor.Green;
            GreenYellow = XnaColor.GreenYellow;
            Honeydew = XnaColor.Honeydew;
            HotPink = XnaColor.HotPink;
            IndianRed = XnaColor.IndianRed;
            Indigo = XnaColor.Indigo;
            Ivory = XnaColor.Ivory;
            Khaki = XnaColor.Khaki;
            Lavender = XnaColor.Lavender;
            LavenderBlush = XnaColor.LavenderBlush;
            LawnGreen = XnaColor.LawnGreen;
            LemonChiffon = XnaColor.LemonChiffon;
            LightBlue = XnaColor.LightBlue;
            LightCoral = XnaColor.LightCoral;
            LightCyan = XnaColor.LightCyan;
            LightGoldenrodYellow = XnaColor.LightGoldenrodYellow;
            LightGray = XnaColor.LightGray;
            LightGreen = XnaColor.LightGreen;
            LightPink = XnaColor.LightPink;
            LightSalmon = XnaColor.LightSalmon;
            LightSeaGreen = XnaColor.LightSeaGreen;
            LightSkyBlue = XnaColor.LightSkyBlue;
            LightSlateGray = XnaColor.LightSlateGray;
            LightSteelBlue = XnaColor.LightSteelBlue;
            LightYellow = XnaColor.LightYellow;
            Lime = XnaColor.Lime;
            LimeGreen = XnaColor.LimeGreen;
            Linen = XnaColor.Linen;
            Magenta = XnaColor.Magenta;
            Maroon = XnaColor.Maroon;
            MediumAquamarine = XnaColor.MediumAquamarine;
            MediumBlue = XnaColor.MediumBlue;
            MediumOrchid = XnaColor.MediumOrchid;
            MediumPurple = XnaColor.MediumPurple;
            MediumSeaGreen = XnaColor.MediumSeaGreen;
            MediumSlateBlue = XnaColor.MediumSlateBlue;
            MediumSpringGreen = XnaColor.MediumSpringGreen;
            MediumTurquoise = XnaColor.MediumTurquoise;
            MediumVioletRed = XnaColor.MediumVioletRed;
            MidnightBlue = XnaColor.MidnightBlue;
            MintCream = XnaColor.MintCream;
            MistyRose = XnaColor.MistyRose;
            Moccasin = XnaColor.Moccasin;
            NavajoWhite = XnaColor.NavajoWhite;
            Navy = XnaColor.Navy;
            OldLace = XnaColor.OldLace;
            Olive = XnaColor.Olive;
            OliveDrab = XnaColor.OliveDrab;
            Orange = XnaColor.Orange;
            OrangeRed = XnaColor.OrangeRed;
            Orchid = XnaColor.Orchid;
            PaleGoldenrod = XnaColor.PaleGoldenrod;
            PaleGreen = XnaColor.PaleGreen;
            PaleTurquoise = XnaColor.PaleTurquoise;
            PaleVioletRed = XnaColor.PaleVioletRed;
            PapayaWhip = XnaColor.PapayaWhip;
            PeachPuff = XnaColor.PeachPuff;
            Peru = XnaColor.Peru;
            Pink = XnaColor.Pink;
            Plum = XnaColor.Plum;
            PowderBlue = XnaColor.PowderBlue;
            Purple = XnaColor.Purple;
            Red = XnaColor.Red;
            RosyBrown = XnaColor.RosyBrown;
            RoyalBlue = XnaColor.RoyalBlue;
            SaddleBrown = XnaColor.SaddleBrown;
            Salmon = XnaColor.Salmon;
            SandyBrown = XnaColor.SandyBrown;
            SeaGreen = XnaColor.SeaGreen;
            SeaShell = XnaColor.SeaShell;
            Sienna = XnaColor.Sienna;
            Silver = XnaColor.Silver;
            SkyBlue = XnaColor.SkyBlue;
            SlateBlue = XnaColor.SlateBlue;
            SlateGray = XnaColor.SlateGray;
            Snow = XnaColor.Snow;
            SpringGreen = XnaColor.SpringGreen;
            SteelBlue = XnaColor.SteelBlue;
            Tan = XnaColor.Tan;
            Teal = XnaColor.Teal;
            Thistle = XnaColor.Thistle;
            Tomato = XnaColor.Tomato;
            Turquoise = XnaColor.Turquoise;
            Violet = XnaColor.Violet;
            Wheat = XnaColor.Wheat;
            White = XnaColor.White;
            WhiteSmoke = XnaColor.WhiteSmoke;
            Yellow = XnaColor.Yellow;
            YellowGreen = XnaColor.YellowGreen;
        }
        #endregion

        private int r, g, b, a;

        public int R { get { return r; } set { r = value; } }
        public int G { get { return g; } set { g = value; } }
        public int B { get { return b; } set { b = value; } }
        public int A { get { return a; } set { a = value; } }

        public Color(float r, float g, float b, float a)
        {
            this.r = (int)(255 * r);
            this.g = (int)(255 * g);
            this.b = (int)(255 * b);
            this.a = (int)(255 * a);
        }

        public Color(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        #region converters

        public static implicit operator XnaColor(Color v)
        {
            return new XnaColor(v.R, v.G, v.B, v.A);
        }

        public static implicit operator Color(XnaColor v)
        {
            return new Color(v.R, v.G, v.B, v.A);
        }

        #endregion


        #region Color Bank
        /// <summary>
        /// TransparentBlack color (R:0,G:0,B:0,A:0).
        /// </summary>
        public static Color TransparentBlack
        {
            get;
            private set;
        }

        /// <summary>
        /// Transparent color (R:0,G:0,B:0,A:0).
        /// </summary>
        public static Color Transparent
        {
            get;
            private set;
        }

        /// <summary>
        /// AliceBlue color (R:240,G:248,B:255,A:255).
        /// </summary>
        public static Color AliceBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// AntiqueWhite color (R:250,G:235,B:215,A:255).
        /// </summary>
        public static Color AntiqueWhite
        {
            get;
            private set;
        }

        /// <summary>
        /// Aqua color (R:0,G:255,B:255,A:255).
        /// </summary>
        public static Color Aqua
        {
            get;
            private set;
        }

        /// <summary>
        /// Aquamarine color (R:127,G:255,B:212,A:255).
        /// </summary>
        public static Color Aquamarine
        {
            get;
            private set;
        }

        /// <summary>
        /// Azure color (R:240,G:255,B:255,A:255).
        /// </summary>
        public static Color Azure
        {
            get;
            private set;
        }

        /// <summary>
        /// Beige color (R:245,G:245,B:220,A:255).
        /// </summary>
        public static Color Beige
        {
            get;
            private set;
        }

        /// <summary>
        /// Bisque color (R:255,G:228,B:196,A:255).
        /// </summary>
        public static Color Bisque
        {
            get;
            private set;
        }

        /// <summary>
        /// Black color (R:0,G:0,B:0,A:255).
        /// </summary>
        public static Color Black
        {
            get;
            private set;
        }

        /// <summary>
        /// BlanchedAlmond color (R:255,G:235,B:205,A:255).
        /// </summary>
        public static Color BlanchedAlmond
        {
            get;
            private set;
        }

        /// <summary>
        /// Blue color (R:0,G:0,B:255,A:255).
        /// </summary>
        public static Color Blue
        {
            get;
            private set;
        }

        /// <summary>
        /// BlueViolet color (R:138,G:43,B:226,A:255).
        /// </summary>
        public static Color BlueViolet
        {
            get;
            private set;
        }

        /// <summary>
        /// Brown color (R:165,G:42,B:42,A:255).
        /// </summary>
        public static Color Brown
        {
            get;
            private set;
        }

        /// <summary>
        /// BurlyWood color (R:222,G:184,B:135,A:255).
        /// </summary>
        public static Color BurlyWood
        {
            get;
            private set;
        }

        /// <summary>
        /// CadetBlue color (R:95,G:158,B:160,A:255).
        /// </summary>
        public static Color CadetBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// Chartreuse color (R:127,G:255,B:0,A:255).
        /// </summary>
        public static Color Chartreuse
        {
            get;
            private set;
        }

        /// <summary>
        /// Chocolate color (R:210,G:105,B:30,A:255).
        /// </summary>
        public static Color Chocolate
        {
            get;
            private set;
        }

        /// <summary>
        /// Coral color (R:255,G:127,B:80,A:255).
        /// </summary>
        public static Color Coral
        {
            get;
            private set;
        }

        /// <summary>
        /// CornflowerBlue color (R:100,G:149,B:237,A:255).
        /// </summary>
        public static Color CornflowerBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// Cornsilk color (R:255,G:248,B:220,A:255).
        /// </summary>
        public static Color Cornsilk
        {
            get;
            private set;
        }

        /// <summary>
        /// Crimson color (R:220,G:20,B:60,A:255).
        /// </summary>
        public static Color Crimson
        {
            get;
            private set;
        }

        /// <summary>
        /// Cyan color (R:0,G:255,B:255,A:255).
        /// </summary>
        public static Color Cyan
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkBlue color (R:0,G:0,B:139,A:255).
        /// </summary>
        public static Color DarkBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkCyan color (R:0,G:139,B:139,A:255).
        /// </summary>
        public static Color DarkCyan
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkGoldenrod color (R:184,G:134,B:11,A:255).
        /// </summary>
        public static Color DarkGoldenrod
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkGray color (R:169,G:169,B:169,A:255).
        /// </summary>
        public static Color DarkGray
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkGreen color (R:0,G:100,B:0,A:255).
        /// </summary>
        public static Color DarkGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkKhaki color (R:189,G:183,B:107,A:255).
        /// </summary>
        public static Color DarkKhaki
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkMagenta color (R:139,G:0,B:139,A:255).
        /// </summary>
        public static Color DarkMagenta
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOliveGreen color (R:85,G:107,B:47,A:255).
        /// </summary>
        public static Color DarkOliveGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOrange color (R:255,G:140,B:0,A:255).
        /// </summary>
        public static Color DarkOrange
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOrchid color (R:153,G:50,B:204,A:255).
        /// </summary>
        public static Color DarkOrchid
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkRed color (R:139,G:0,B:0,A:255).
        /// </summary>
        public static Color DarkRed
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSalmon color (R:233,G:150,B:122,A:255).
        /// </summary>
        public static Color DarkSalmon
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSeaGreen color (R:143,G:188,B:139,A:255).
        /// </summary>
        public static Color DarkSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSlateBlue color (R:72,G:61,B:139,A:255).
        /// </summary>
        public static Color DarkSlateBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSlateGray color (R:47,G:79,B:79,A:255).
        /// </summary>
        public static Color DarkSlateGray
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkTurquoise color (R:0,G:206,B:209,A:255).
        /// </summary>
        public static Color DarkTurquoise
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkViolet color (R:148,G:0,B:211,A:255).
        /// </summary>
        public static Color DarkViolet
        {
            get;
            private set;
        }

        /// <summary>
        /// DeepPink color (R:255,G:20,B:147,A:255).
        /// </summary>
        public static Color DeepPink
        {
            get;
            private set;
        }

        /// <summary>
        /// DeepSkyBlue color (R:0,G:191,B:255,A:255).
        /// </summary>
        public static Color DeepSkyBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// DimGray color (R:105,G:105,B:105,A:255).
        /// </summary>
        public static Color DimGray
        {
            get;
            private set;
        }

        /// <summary>
        /// DodgerBlue color (R:30,G:144,B:255,A:255).
        /// </summary>
        public static Color DodgerBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// Firebrick color (R:178,G:34,B:34,A:255).
        /// </summary>
        public static Color Firebrick
        {
            get;
            private set;
        }

        /// <summary>
        /// FloralWhite color (R:255,G:250,B:240,A:255).
        /// </summary>
        public static Color FloralWhite
        {
            get;
            private set;
        }

        /// <summary>
        /// ForestGreen color (R:34,G:139,B:34,A:255).
        /// </summary>
        public static Color ForestGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// Fuchsia color (R:255,G:0,B:255,A:255).
        /// </summary>
        public static Color Fuchsia
        {
            get;
            private set;
        }

        /// <summary>
        /// Gainsboro color (R:220,G:220,B:220,A:255).
        /// </summary>
        public static Color Gainsboro
        {
            get;
            private set;
        }

        /// <summary>
        /// GhostWhite color (R:248,G:248,B:255,A:255).
        /// </summary>
        public static Color GhostWhite
        {
            get;
            private set;
        }
        /// <summary>
        /// Gold color (R:255,G:215,B:0,A:255).
        /// </summary>
        public static Color Gold
        {
            get;
            private set;
        }

        /// <summary>
        /// Goldenrod color (R:218,G:165,B:32,A:255).
        /// </summary>
        public static Color Goldenrod
        {
            get;
            private set;
        }

        /// <summary>
        /// Gray color (R:128,G:128,B:128,A:255).
        /// </summary>
        public static Color Gray
        {
            get;
            private set;
        }

        /// <summary>
        /// Green color (R:0,G:128,B:0,A:255).
        /// </summary>
        public static Color Green
        {
            get;
            private set;
        }

        /// <summary>
        /// GreenYellow color (R:173,G:255,B:47,A:255).
        /// </summary>
        public static Color GreenYellow
        {
            get;
            private set;
        }

        /// <summary>
        /// Honeydew color (R:240,G:255,B:240,A:255).
        /// </summary>
        public static Color Honeydew
        {
            get;
            private set;
        }

        /// <summary>
        /// HotPink color (R:255,G:105,B:180,A:255).
        /// </summary>
        public static Color HotPink
        {
            get;
            private set;
        }

        /// <summary>
        /// IndianRed color (R:205,G:92,B:92,A:255).
        /// </summary>
        public static Color IndianRed
        {
            get;
            private set;
        }

        /// <summary>
        /// Indigo color (R:75,G:0,B:130,A:255).
        /// </summary>
        public static Color Indigo
        {
            get;
            private set;
        }

        /// <summary>
        /// Ivory color (R:255,G:255,B:240,A:255).
        /// </summary>
        public static Color Ivory
        {
            get;
            private set;
        }

        /// <summary>
        /// Khaki color (R:240,G:230,B:140,A:255).
        /// </summary>
        public static Color Khaki
        {
            get;
            private set;
        }

        /// <summary>
        /// Lavender color (R:230,G:230,B:250,A:255).
        /// </summary>
        public static Color Lavender
        {
            get;
            private set;
        }

        /// <summary>
        /// LavenderBlush color (R:255,G:240,B:245,A:255).
        /// </summary>
        public static Color LavenderBlush
        {
            get;
            private set;
        }

        /// <summary>
        /// LawnGreen color (R:124,G:252,B:0,A:255).
        /// </summary>
        public static Color LawnGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LemonChiffon color (R:255,G:250,B:205,A:255).
        /// </summary>
        public static Color LemonChiffon
        {
            get;
            private set;
        }

        /// <summary>
        /// LightBlue color (R:173,G:216,B:230,A:255).
        /// </summary>
        public static Color LightBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightCoral color (R:240,G:128,B:128,A:255).
        /// </summary>
        public static Color LightCoral
        {
            get;
            private set;
        }

        /// <summary>
        /// LightCyan color (R:224,G:255,B:255,A:255).
        /// </summary>
        public static Color LightCyan
        {
            get;
            private set;
        }

        /// <summary>
        /// LightGoldenrodYellow color (R:250,G:250,B:210,A:255).
        /// </summary>
        public static Color LightGoldenrodYellow
        {
            get;
            private set;
        }

        /// <summary>
        /// LightGray color (R:211,G:211,B:211,A:255).
        /// </summary>
        public static Color LightGray
        {
            get;
            private set;
        }

        /// <summary>
        /// LightGreen color (R:144,G:238,B:144,A:255).
        /// </summary>
        public static Color LightGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LightPink color (R:255,G:182,B:193,A:255).
        /// </summary>
        public static Color LightPink
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSalmon color (R:255,G:160,B:122,A:255).
        /// </summary>
        public static Color LightSalmon
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSeaGreen color (R:32,G:178,B:170,A:255).
        /// </summary>
        public static Color LightSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSkyBlue color (R:135,G:206,B:250,A:255).
        /// </summary>
        public static Color LightSkyBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSlateGray color (R:119,G:136,B:153,A:255).
        /// </summary>
        public static Color LightSlateGray
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSteelBlue color (R:176,G:196,B:222,A:255).
        /// </summary>
        public static Color LightSteelBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightYellow color (R:255,G:255,B:224,A:255).
        /// </summary>
        public static Color LightYellow
        {
            get;
            private set;
        }

        /// <summary>
        /// Lime color (R:0,G:255,B:0,A:255).
        /// </summary>
        public static Color Lime
        {
            get;
            private set;
        }

        /// <summary>
        /// LimeGreen color (R:50,G:205,B:50,A:255).
        /// </summary>
        public static Color LimeGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// Linen color (R:250,G:240,B:230,A:255).
        /// </summary>
        public static Color Linen
        {
            get;
            private set;
        }

        /// <summary>
        /// Magenta color (R:255,G:0,B:255,A:255).
        /// </summary>
        public static Color Magenta
        {
            get;
            private set;
        }

        /// <summary>
        /// Maroon color (R:128,G:0,B:0,A:255).
        /// </summary>
        public static Color Maroon
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumAquamarine color (R:102,G:205,B:170,A:255).
        /// </summary>
        public static Color MediumAquamarine
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumBlue color (R:0,G:0,B:205,A:255).
        /// </summary>
        public static Color MediumBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumOrchid color (R:186,G:85,B:211,A:255).
        /// </summary>
        public static Color MediumOrchid
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumPurple color (R:147,G:112,B:219,A:255).
        /// </summary>
        public static Color MediumPurple
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSeaGreen color (R:60,G:179,B:113,A:255).
        /// </summary>
        public static Color MediumSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSlateBlue color (R:123,G:104,B:238,A:255).
        /// </summary>
        public static Color MediumSlateBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSpringGreen color (R:0,G:250,B:154,A:255).
        /// </summary>
        public static Color MediumSpringGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumTurquoise color (R:72,G:209,B:204,A:255).
        /// </summary>
        public static Color MediumTurquoise
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumVioletRed color (R:199,G:21,B:133,A:255).
        /// </summary>
        public static Color MediumVioletRed
        {
            get;
            private set;
        }

        /// <summary>
        /// MidnightBlue color (R:25,G:25,B:112,A:255).
        /// </summary>
        public static Color MidnightBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MintCream color (R:245,G:255,B:250,A:255).
        /// </summary>
        public static Color MintCream
        {
            get;
            private set;
        }

        /// <summary>
        /// MistyRose color (R:255,G:228,B:225,A:255).
        /// </summary>
        public static Color MistyRose
        {
            get;
            private set;
        }

        /// <summary>
        /// Moccasin color (R:255,G:228,B:181,A:255).
        /// </summary>
        public static Color Moccasin
        {
            get;
            private set;
        }

        /// <summary>
        /// NavajoWhite color (R:255,G:222,B:173,A:255).
        /// </summary>
        public static Color NavajoWhite
        {
            get;
            private set;
        }

        /// <summary>
        /// Navy color (R:0,G:0,B:128,A:255).
        /// </summary>
        public static Color Navy
        {
            get;
            private set;
        }

        /// <summary>
        /// OldLace color (R:253,G:245,B:230,A:255).
        /// </summary>
        public static Color OldLace
        {
            get;
            private set;
        }

        /// <summary>
        /// Olive color (R:128,G:128,B:0,A:255).
        /// </summary>
        public static Color Olive
        {
            get;
            private set;
        }

        /// <summary>
        /// OliveDrab color (R:107,G:142,B:35,A:255).
        /// </summary>
        public static Color OliveDrab
        {
            get;
            private set;
        }

        /// <summary>
        /// Orange color (R:255,G:165,B:0,A:255).
        /// </summary>
        public static Color Orange
        {
            get;
            private set;
        }

        /// <summary>
        /// OrangeRed color (R:255,G:69,B:0,A:255).
        /// </summary>
        public static Color OrangeRed
        {
            get;
            private set;
        }

        /// <summary>
        /// Orchid color (R:218,G:112,B:214,A:255).
        /// </summary>
        public static Color Orchid
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleGoldenrod color (R:238,G:232,B:170,A:255).
        /// </summary>
        public static Color PaleGoldenrod
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleGreen color (R:152,G:251,B:152,A:255).
        /// </summary>
        public static Color PaleGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleTurquoise color (R:175,G:238,B:238,A:255).
        /// </summary>
        public static Color PaleTurquoise
        {
            get;
            private set;
        }
        /// <summary>
        /// PaleVioletRed color (R:219,G:112,B:147,A:255).
        /// </summary>
        public static Color PaleVioletRed
        {
            get;
            private set;
        }

        /// <summary>
        /// PapayaWhip color (R:255,G:239,B:213,A:255).
        /// </summary>
        public static Color PapayaWhip
        {
            get;
            private set;
        }

        /// <summary>
        /// PeachPuff color (R:255,G:218,B:185,A:255).
        /// </summary>
        public static Color PeachPuff
        {
            get;
            private set;
        }

        /// <summary>
        /// Peru color (R:205,G:133,B:63,A:255).
        /// </summary>
        public static Color Peru
        {
            get;
            private set;
        }

        /// <summary>
        /// Pink color (R:255,G:192,B:203,A:255).
        /// </summary>
        public static Color Pink
        {
            get;
            private set;
        }

        /// <summary>
        /// Plum color (R:221,G:160,B:221,A:255).
        /// </summary>
        public static Color Plum
        {
            get;
            private set;
        }

        /// <summary>
        /// PowderBlue color (R:176,G:224,B:230,A:255).
        /// </summary>
        public static Color PowderBlue
        {
            get;
            private set;
        }

        /// <summary>
        ///  Purple color (R:128,G:0,B:128,A:255).
        /// </summary>
        public static Color Purple
        {
            get;
            private set;
        }

        /// <summary>
        /// Red color (R:255,G:0,B:0,A:255).
        /// </summary>
        public static Color Red
        {
            get;
            private set;
        }

        /// <summary>
        /// RosyBrown color (R:188,G:143,B:143,A:255).
        /// </summary>
        public static Color RosyBrown
        {
            get;
            private set;
        }

        /// <summary>
        /// RoyalBlue color (R:65,G:105,B:225,A:255).
        /// </summary>
        public static Color RoyalBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// SaddleBrown color (R:139,G:69,B:19,A:255).
        /// </summary>
        public static Color SaddleBrown
        {
            get;
            private set;
        }

        /// <summary>
        /// Salmon color (R:250,G:128,B:114,A:255).
        /// </summary>
        public static Color Salmon
        {
            get;
            private set;
        }

        /// <summary>
        /// SandyBrown color (R:244,G:164,B:96,A:255).
        /// </summary>
        public static Color SandyBrown
        {
            get;
            private set;
        }

        /// <summary>
        /// SeaGreen color (R:46,G:139,B:87,A:255).
        /// </summary>
        public static Color SeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// SeaShell color (R:255,G:245,B:238,A:255).
        /// </summary>
        public static Color SeaShell
        {
            get;
            private set;
        }

        /// <summary>
        /// Sienna color (R:160,G:82,B:45,A:255).
        /// </summary>
        public static Color Sienna
        {
            get;
            private set;
        }

        /// <summary>
        /// Silver color (R:192,G:192,B:192,A:255).
        /// </summary>
        public static Color Silver
        {
            get;
            private set;
        }

        /// <summary>
        /// SkyBlue color (R:135,G:206,B:235,A:255).
        /// </summary>
        public static Color SkyBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// SlateBlue color (R:106,G:90,B:205,A:255).
        /// </summary>
        public static Color SlateBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// SlateGray color (R:112,G:128,B:144,A:255).
        /// </summary>
        public static Color SlateGray
        {
            get;
            private set;
        }

        /// <summary>
        /// Snow color (R:255,G:250,B:250,A:255).
        /// </summary>
        public static Color Snow
        {
            get;
            private set;
        }

        /// <summary>
        /// SpringGreen color (R:0,G:255,B:127,A:255).
        /// </summary>
        public static Color SpringGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// SteelBlue color (R:70,G:130,B:180,A:255).
        /// </summary>
        public static Color SteelBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// Tan color (R:210,G:180,B:140,A:255).
        /// </summary>
        public static Color Tan
        {
            get;
            private set;
        }

        /// <summary>
        /// Teal color (R:0,G:128,B:128,A:255).
        /// </summary>
        public static Color Teal
        {
            get;
            private set;
        }

        /// <summary>
        /// Thistle color (R:216,G:191,B:216,A:255).
        /// </summary>
        public static Color Thistle
        {
            get;
            private set;
        }

        /// <summary>
        /// Tomato color (R:255,G:99,B:71,A:255).
        /// </summary>
        public static Color Tomato
        {
            get;
            private set;
        }

        /// <summary>
        /// Turquoise color (R:64,G:224,B:208,A:255).
        /// </summary>
        public static Color Turquoise
        {
            get;
            private set;
        }

        /// <summary>
        /// Violet color (R:238,G:130,B:238,A:255).
        /// </summary>
        public static Color Violet
        {
            get;
            private set;
        }

        /// <summary>
        /// Wheat color (R:245,G:222,B:179,A:255).
        /// </summary>
        public static Color Wheat
        {
            get;
            private set;
        }

        /// <summary>
        /// White color (R:255,G:255,B:255,A:255).
        /// </summary>
        public static Color White
        {
            get;
            private set;
        }

        /// <summary>
        /// WhiteSmoke color (R:245,G:245,B:245,A:255).
        /// </summary>
        public static Color WhiteSmoke
        {
            get;
            private set;
        }

        /// <summary>
        /// Yellow color (R:255,G:255,B:0,A:255).
        /// </summary>
        public static Color Yellow
        {
            get;
            private set;
        }

        /// <summary>
        /// YellowGreen color (R:154,G:205,B:50,A:255).
        /// </summary>
        public static Color YellowGreen
        {
            get;
            private set;
        }
        #endregion


    }



}
