using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WhiskeyEditor
{
    public static class ResourceFiles
    {

        public const string CompileLib = "compile-lib";
        public const string LibExe = "compile-exe\\WhiskeyRunner.exe";
        public const string CompileMedia = "compile-media";

        public static readonly string DllMonoGame = CompileLib + Path.DirectorySeparatorChar + "MonoGame.Framework.dll";
        public static readonly string DllWhiskeyCore = CompileLib + Path.DirectorySeparatorChar + "Whiskey2D.dll";
        public static readonly string DllSystem = "system.dll";


        public static readonly string SettingsFile = ".props";

    }
}
