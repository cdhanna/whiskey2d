using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Project;
using Whiskey2D.Core;

namespace WhiskeyEditor.Backend
{
    class TestBack
    {

        [STAThread]
        public static void Main()
        {

            //set active project
            Project.Project proj = ProjectManager.Instance.createNewProject("RedoProject", "RedoProjet");
            ProjectManager.Instance.ActiveProject = proj;

            //create a type
            TypeDescriptor t2 = new TypeDescriptor("TestMeAgain");
            
            t2.addPropertyDescriptor(new PropertyDescriptor("Injected", new RealType(typeof(String), "GoodBye")));

            //set default value for sprite
            Sprite spr = (Sprite)t2.getTypeValOfName("Sprite").value;
            spr.Scale = new Vector(100, 100);
            spr.Color = Color.Turquoise;

            //create a script
            ScriptDescriptor s = new ScriptDescriptor("RunMe", t2.Name);
            s.ensureFileExists();
            t2.addScript(s.Name);
            
            
            //create an instance
            InstanceDescriptor t2Instance = new InstanceDescriptor(t2);
            t2Instance.getTypeValOfName("X").value = (Single) 100;

            t2Instance.removeScript("RunMe");
            
            
            


            //add the script to an instance
            //t2Instance.addScript("RunMe");

            //compile all sources
            string dllPath = CompileManager.Instance.compile();
            InstanceManager.Instance.convertToGobs(dllPath, "default");

            //build exe
            ProjectManager.Instance.ActiveProject.buildExecutable();

        }

    }
}
