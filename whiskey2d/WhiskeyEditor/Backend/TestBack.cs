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

            Project.Project proj = ProjectManager.Instance.createNewProject("RedoProject", "RedoProjet");
            
            ProjectManager.Instance.ActiveProject = proj;




            TypeDescriptor t2 = new TypeDescriptor("TestMeAgain");

            t2.addPropertyDescriptor(new PropertyDescriptor("Injected", new RealType(typeof(String), "GoodBye")));

            Sprite spr = (Sprite)t2.getTypeValOfName("Sprite").value;
            spr.Scale = new Vector(100, 100);
            spr.Color = Color.Turquoise;

            InstanceDescriptor t2Instance = new InstanceDescriptor(t2);
            t2Instance.getTypeValOfName("X").value = (Single) 100;

            

            ScriptDescriptor s = new ScriptDescriptor("RunMe", "TestMeAgain");


            t2Instance.addScript("RunMe");

           // object testPropVal = i.getTypeValOfName("TestProp").value;

            string dllPath = CompileManager.Instance.compile();
            InstanceManager.Instance.convertToGobs(dllPath, "default");


            ProjectManager.Instance.ActiveProject.buildExecutable();

        }

    }
}
