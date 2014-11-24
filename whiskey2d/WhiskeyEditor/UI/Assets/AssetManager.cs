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
        public static readonly string FILE_ICON_TEST = PATH_ASSETS + "TestIcon.png";
        public static readonly string FILE_ICON_FILE = PATH_ASSETS + "fileIcon.png";
        public static readonly string FILE_ICON_FLDR = PATH_ASSETS + "folderIcon.png";
        public static readonly string FILE_ICON_CLOSE = PATH_ASSETS + "closeIcon.png";

        public static readonly Image ICON_TEST;
        public static readonly Image ICON_FILE;
        public static readonly Image ICON_FLDR;
        public static readonly Image ICON_CLOSE;

        static AssetManager()
        {
            ICON_TEST = Image.FromFile(FILE_ICON_TEST);
            ICON_FILE = Image.FromFile(FILE_ICON_FILE);
            ICON_FLDR = Image.FromFile(FILE_ICON_FLDR);
            ICON_CLOSE = Image.FromFile(FILE_ICON_CLOSE);
        }

        private AssetManager()
        {

        }

    }
}
