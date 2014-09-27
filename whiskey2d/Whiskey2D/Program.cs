#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Whiskey2D.Service;
using Whiskey2D.Core;
using Whiskey2D.ex2;
#endregion

namespace Whiskey2D
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

            /**
             *  COMPILER THE CORE AND GAME
             *///

            //Compiler compiler = Compiler.getInstance();
            //compiler.compileDirectory("Whiskey.Core", "Core", "MonoGame.Framework");
            //compiler.compileDirectory("Whiskey.TestImpl", "TestImpl", "MonoGame.Framework", "Whiskey.Core");

            //////////////////////


            /**
             *   RUN GAME
             *///

            ////add core to path
            //Assembly coreAssmebly = Assembly.LoadFrom("Whiskey.Core.dll");

            ////add game data to path
            //Assembly gameAssembly = Assembly.LoadFrom("Whiskey.TestImpl.dll");


            ////find gameManager
            //Type[] coreTypes = coreAssmebly.GetTypes();
            //foreach (Type type in coreTypes)
            //{
            //    if (type.Name.Equals("GameManager"))
            //    {
            //        object gameManager = Activator.CreateInstance(type, gameAssembly);
            //        gameManager.GetType().GetMethod("go").Invoke(gameManager, new object[] { });
            //        break;
            //    }
            //}


            using (var game = new MonoBaseGame())
                game.Run();
        }
    }
#endif
}
