using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using WhiskeyEditor;
using Microsoft.Xna.Framework.Graphics;
using WhiskeyRunner;
using System.Reflection;
using WhiskeyEditor.Project;
namespace WhiskeyEditor.TestFrame
{
    abstract class Tester
    {
        [AttributeUsage(AttributeTargets.Method)]
        protected class Test : Attribute
        {

        }


        MonoBaseGame game;
            

        public Tester()
        {
            game = new MonoBaseGame();
            Parsers.Parse p = Parsers.Parse.Instance;

            ProjectManager.Instance.ActiveProject = getStartProject();
        }


        protected abstract Project.Project getStartProject();

        public void runTests()
        {
            MethodInfo[] methods = GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (null != method.GetCustomAttribute(typeof(Test)))
                {
                    if (method.GetParameters().Length == 0)
                    {
                        method.Invoke(this, new object[] { });
                    }
                    else
                    {
                        Console.WriteLine("ERROR: " + method.Name + " must have no arguements");
                    }

                }
            }
        }
    }
}
