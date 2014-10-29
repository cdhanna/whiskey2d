using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyEditor
{
    public class ResourceFiles
    {

        public const string Lib = "compile-lib";

        public static readonly string DllMonoGame = Lib + Path.DirectorySeparatorChar + "MonoGame.Framework.dll";
        public static readonly string DllWhiskeyCore = Lib + Path.DirectorySeparatorChar + "Whiskey2D.dll";
        public static readonly string DllSystem = "system.dll";


        public static readonly string SettingsFile = ".props";

    }
}
