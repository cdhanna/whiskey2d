using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


namespace WhiskeyEditor.UI.Assets
{
    class AssetManager
    {
        public static readonly string PATH_ASSETS = "UI" + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Set" + Path.DirectorySeparatorChar;
        public static readonly string FILE_ICON_FILE = PATH_ASSETS + "document.png";
        public static readonly string FILE_ICON_FILE_LEVEL = PATH_ASSETS + "leveldocument.png";
        public static readonly string FILE_ICON_FILE_SCRIPT = PATH_ASSETS + "scriptdocument.png";
        public static readonly string FILE_ICON_FILE_TYPE = PATH_ASSETS + "typedocument.png";
        public static readonly string FILE_ICON_FILE_PICTURE = PATH_ASSETS + "picturedocument.png";
        public static readonly string FILE_ICON_FLDR = PATH_ASSETS + "folder.png";
        public static readonly string FILE_ICON_CLOSE = PATH_ASSETS + "closeIcon.png";
        public static readonly string FILE_ICON_SAVE = PATH_ASSETS + "save.png";
        public static readonly string FILE_ICON_PLAY = PATH_ASSETS + "play.png";
        public static readonly string FILE_ICON_COMPILE = PATH_ASSETS + "properties.png";
        public static readonly string FILE_ICON_PLUS = PATH_ASSETS + "plus.png";
        public static readonly string FILE_ICON_MINUS = PATH_ASSETS + "minus.png";


        //public static readonly string PATH_ASSETS = "UI" + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar;
        //public static readonly string FILE_ICON_FILE = PATH_ASSETS + "fileIcon.png";
        //public static readonly string FILE_ICON_FLDR = PATH_ASSETS + "folderIcon.png";
        //public static readonly string FILE_ICON_CLOSE = PATH_ASSETS + "closeIcon.png";
        //public static readonly string FILE_ICON_SAVE = PATH_ASSETS + "saveIcon.png";
        //public static readonly string FILE_ICON_PLAY = PATH_ASSETS + "playIcon.png";
        //public static readonly string FILE_ICON_COMPILE = PATH_ASSETS + "compileIcon.png";
        //public static readonly string FILE_ICON_PLUS = PATH_ASSETS + "plus.png";
        //public static readonly string FILE_ICON_MINUS = PATH_ASSETS + "minus.png";

        //public static readonly string PATH_ASSETS = "UI" + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "RealSet" + Path.DirectorySeparatorChar;
        //public static readonly string FILE_ICON_FILE = PATH_ASSETS + "docEmpty.png";
        //public static readonly string FILE_ICON_CODE_FILE = PATH_ASSETS + "docCode.png";
        //public static readonly string FILE_ICON_LEVEL_FILE = PATH_ASSETS + "docLevel.png";
        //public static readonly string FILE_ICON_FLDR = PATH_ASSETS + "folder.png";
        //public static readonly string FILE_ICON_CLOSE = PATH_ASSETS + "close.png";
        //public static readonly string FILE_ICON_SAVE = PATH_ASSETS + "save.png";
        //public static readonly string FILE_ICON_PLAY = PATH_ASSETS + "play.png";
        //public static readonly string FILE_ICON_COMPILE = PATH_ASSETS + "gear.png";
        //public static readonly string FILE_ICON_PLUS = PATH_ASSETS + "plus.png";
        //public static readonly string FILE_ICON_MINUS = PATH_ASSETS + "minus.png";




        public static readonly Image ICON_FILE;
        public static readonly Image ICON_FILE_LEVEL;
        public static readonly Image ICON_FILE_PICTURE;
        public static readonly Image ICON_FILE_TYPE;
        public static readonly Image ICON_FILE_SCRIPT;
        //public static readonly Image ICON_CODE_FILE;
        //public static readonly Image ICON_LEVEL_FILE;
        public static readonly Image ICON_FLDR;
        public static readonly Image ICON_CLOSE;
        public static readonly Image ICON_SAVE;
        public static readonly Image ICON_PLAY;
        public static readonly Image ICON_COMPILE;
        public static readonly Image ICON_PLUS;
        public static readonly Image ICON_MINUS;


        public static readonly ImageList Images = new ImageList();

        static AssetManager()
        {
            ICON_FILE = loadImage(FILE_ICON_FILE);

            ICON_FILE_LEVEL = loadImage(FILE_ICON_FILE_LEVEL);
            ICON_FILE_PICTURE = loadImage(FILE_ICON_FILE_PICTURE);
            ICON_FILE_TYPE = loadImage(FILE_ICON_FILE_TYPE);
            ICON_FILE_SCRIPT = loadImage(FILE_ICON_FILE_SCRIPT);
            //ICON_CODE_FILE = loadImage(FILE_ICON_CODE_FILE);
            //ICON_LEVEL_FILE = loadImage(FILE_ICON_LEVEL_FILE);
            ICON_FLDR = loadImage(FILE_ICON_FLDR);
            ICON_CLOSE = loadImage(FILE_ICON_CLOSE);
            ICON_SAVE = loadImage(FILE_ICON_SAVE);
            ICON_PLAY = loadImage(FILE_ICON_PLAY);
            ICON_COMPILE = loadImage(FILE_ICON_COMPILE);
            ICON_PLUS = loadImage(FILE_ICON_PLUS);
            ICON_MINUS = loadImage(FILE_ICON_MINUS);
        }

        public static ImageList getImageList()
        {
            ImageList list = new ImageList();
            foreach (string key in Images.Images.Keys)
            {
                list.Images.Add(key, Images.Images[key]);
            }
            return list;
        }

        public static Image loadImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                Images.Images.Add(path, img);
                return img;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int indexOf(string path)
        {
            return Images.Images.IndexOfKey(path);
        }

        private AssetManager()
        {

        }

    }
}
