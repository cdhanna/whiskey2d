#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Whiskey2D.Core;

#endregion

namespace WhiskeyRunner
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);


            using (var game = new MonoBaseGame())
                game.Run();
        }

        static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            GameManager.Log.error("NOOO COULD NOT FIND IT!");
            Console.WriteLine("failed to load " + args.Name);
           // string folderPath = Path.GetDirectoryName("lib");
           // string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
            string assemblyPath = "lib\\" + args.Name + ".dll";
            if (File.Exists(assemblyPath) == false) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }

    }
#endif
}
