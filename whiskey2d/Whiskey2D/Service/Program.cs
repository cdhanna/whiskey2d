#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#endregion

namespace Whiskey2D.Service
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class WhiskeyLauncher
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            /**
             *   RUN GAME
             *///

            //add core to path
            Assembly coreAssmebly = Assembly.LoadFrom("Whiskey.Core.dll");

            //add game data to path
            Assembly gameAssembly = Assembly.LoadFrom("Whiskey.TestImpl.dll");


            //find gameManager
            Type[] coreTypes = coreAssmebly.GetTypes();
            foreach (Type type in coreTypes)
            {
                if (type.Name.Equals("GameManager"))
                {
                    object gameManager = Activator.CreateInstance(type, "Whiskey.TestImpl.dll");
                    gameManager.GetType().GetMethod("go").Invoke(gameManager, new object[] { });
                    break;
                }
            }


            //using (var game = new GameManager("Whiskey.TestImpl.dll"))
            //    game.Run();
        }
    }
#endif
}
