﻿using System;
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




            TypeDescriptor t = new TypeDescriptor("TestMe");
            TypeDescriptor t2 = new TypeDescriptor("TestMeAgain");

            t2.addPropertyDescriptor(new PropertyDescriptor("Injected", new RealType(typeof(String), "GoodBye")));

            InstanceDescriptor t2Instance = new InstanceDescriptor(t2);

            t.addPropertyDescriptor(new PropertyDescriptor("TestProp", new RealType(typeof(int), 42)));
            t.addPropertyDescriptor(new PropertyDescriptor("TestPropGameObject", new InstanceType(t2, t2Instance)));

            t.addPropertyDescriptor(new PropertyDescriptor("MySpot", new RealType(typeof(Vector), Vector.One)));
            t.addPropertyDescriptor(new PropertyDescriptor("MyColor", new RealType(typeof(Color), new Color(123, 43, 23, 255))));



            ScriptDescriptor s = new ScriptDescriptor("RunMe", "TestMe");

            InstanceDescriptor i = new InstanceDescriptor(t);

           // object testPropVal = i.getTypeValOfName("TestProp").value;

            CompileManager.Instance.compile();
        }

    }
}
