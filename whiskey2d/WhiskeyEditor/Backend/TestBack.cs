using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using Whiskey2D.Core;
using System.CodeDom.Compiler;
using WhiskeyEditor.UI;
using System.Windows.Forms;
using System.Threading;

namespace WhiskeyEditor.Backend
{
    class TestBack
    {

        

        [STAThread]
        public static void Main()
        {

       

            //set active project
          //  Project proj = ProjectManager.Instance.createNewProject("RedoProject", "RedoProjet");
          //  ProjectManager.Instance.ActiveProject = proj;
          //  ProjectManager.Instance.ActiveProject.loadGameData();
        //    CompileManager.Instance.addListener<CompilerErrorEvent>(compilerErrorHandler);
           // CompileManager.Instance.startBackBuild();
           // string cp = Settings.CurrentProject;

            try
            {
                Project proj = ProjectManager.Instance.openProject(Settings.CurrentProject);
                ProjectManager.Instance.ActiveProject = proj;
            }
            catch (Exception e)
            {
                ProjectManager.Instance.ActiveProject = new NoProject();
            }
            UIManager.Instance.startup();

            return;
            
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


            //create an instance
            //InstanceDescriptor t2Instance = new InstanceDescriptor(t2);
            //t2Instance.getTypeValOfName("X").value = (Single)100;

            //t2.addScript(s.Name);

            State state = State.deserialize("testInstData");
            InstanceManager.Instance.setState(state);

            CompileManager.Instance.Compiled += (sender, args) =>
            {
                foreach (CompilerError err in args.Errors)
                {
                    Console.WriteLine(err.ErrorText);
                }
            };

            //State instState = InstanceManager.Instance.getState();
            //State.serialize(instState, "testInstData.state");

            //add the script to an instance
            //t2Instance.addScript("RunMe");

            //compile all sources
           // string dllPath = CompileManager.Instance.compile();
           // InstanceManager.Instance.convertToGobs(dllPath, "default");


            //GameData data = FileManager.Instance.getGameData();
            //GameData.serialize(data, "testdata.txt");
            
            //build exe
           // ProjectManager.Instance.ActiveProject.buildExecutable();
    

            //setup UI for testing
           
            //Thread t = new Thread(() =>
            //{


            //    while (true)
            //    {
            //        Thread.Sleep(10);
            //    }


            //});
            //t.Start();

          


        }

        
    }
}
