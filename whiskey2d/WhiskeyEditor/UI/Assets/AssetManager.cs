using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
namespace WhiskeyEditor.UI.Assets
{
    class AssetManager
    {
        public static readonly string PATH_ASSETS = "UI" + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar;
        public static readonly string FILE_ICON_FILE = PATH_ASSETS + "fileIcon.png";
        public static readonly string FILE_ICON_FLDR = PATH_ASSETS + "folderIcon.png";
        public static readonly string FILE_ICON_CLOSE = PATH_ASSETS + "closeIcon.png";
        public static readonly string FILE_ICON_SAVE = PATH_ASSETS + "saveIcon.png";
        public static readonly string FILE_ICON_PLAY = PATH_ASSETS + "playIcon.png";
        public static readonly string FILE_ICON_COMPILE = PATH_ASSETS + "compileIcon.png";
        public static readonly string FILE_ICON_PLUS = PATH_ASSETS + "plus.png";
        public static readonly string FILE_ICON_MINUS = PATH_ASSETS + "minus.png";



        public static readonly Image ICON_FILE;
        public static readonly Image ICON_FLDR;
        public static readonly Image ICON_CLOSE;
        public static readonly Image ICON_SAVE;
        public static readonly Image ICON_PLAY;
        public static readonly Image ICON_COMPILE;
        public static readonly Image ICON_PLUS;
        public static readonly Image ICON_MINUS;

        static AssetManager()
        {
            ICON_FILE = loadImage(FILE_ICON_FILE);
            ICON_FLDR = loadImage(FILE_ICON_FLDR);
            ICON_CLOSE = loadImage(FILE_ICON_CLOSE);
            ICON_SAVE = loadImage(FILE_ICON_SAVE);
            ICON_PLAY = loadImage(FILE_ICON_PLAY);
            ICON_COMPILE = loadImage(FILE_ICON_COMPILE);
            ICON_PLUS = loadImage(FILE_ICON_PLUS);
            ICON_MINUS = loadImage(FILE_ICON_MINUS);
        }

        public static Image loadImage(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private AssetManager()
        {

        }

    }
}
