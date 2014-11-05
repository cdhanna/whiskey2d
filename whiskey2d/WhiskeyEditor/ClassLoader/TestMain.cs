using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using System.Reflection;
using Whiskey2D.Services;

namespace WhiskeyEditor.ClassLoader
{


    static class TestMain
    {

  

        [STAThread]
        static void Main()
        {

            Project.ProjectManager.Instance.ActiveProject = Project.ProjectManager.Instance.createNewProject("HomeTest", "HomeTest");
            GameObjectDescriptor gobd = new GameObjectDescriptor("Project", "A");
            gobd.compile();
            //Type gobdType = TypeManager.getInstance().addDescriptor(gobd);


            ServiceHandle<GameObjectService> sHandle = new ServiceHandle<GameObjectService>(Project.ProjectManager.Instance.ActiveProject.PathBin + "\\Project.A0.dll", "Project.A");

            Library lib = ServiceLoader.Instance.createLibrary();
            lib.add(sHandle);

            lib.unload();


            //Library lib = ServiceLoader.Instance.createLibrary();
            //lib.addService(new ServiceHandle<

        }

        private static void old()
        {


            Console.WriteLine("running test main");

            List<object> objList = new List<object>();

            //////////AAAAAAAAAAAAAAAAAAa
            GameObjectDescriptor aGobd = new GameObjectDescriptor("SomeNameSpace", "A");
            Type aType = TypeManager.getInstance().addDescriptor(aGobd);

            object aInst = TypeManager.instantiate(aType);
            objList.Add(aInst);


            /////BBBBBBBBBBBBBBBBBBBBBBb

            GameObjectDescriptor bGobd = new GameObjectDescriptor("SomeNameSpace", "B");
            bGobd.addProperty(new PropertyDescriptor("AProp", aType, aInst));

            Type bType = TypeManager.getInstance().addDescriptor(bGobd);

            object bInst = TypeManager.instantiate(bType);
            objList.Add(bInst);
            /////CCCCCCCCCCCCCCCCCCCCCCCc

            GameObjectDescriptor cGobd = new GameObjectDescriptor("SomeNameSpace", "C");
            cGobd.addProperty(new PropertyDescriptor("BProp", bType, bInst));

            Type cType = TypeManager.getInstance().addDescriptor(cGobd);

            object cInst = TypeManager.instantiate(cType);
            objList.Add(cInst);

            ////////
            // GameObjectDescriptor aGobdNew = new GameObjectDescriptor(aGobd);
            aGobd.addProperty(new PropertyDescriptor("Fluff", typeof(int), 4));
            //aGobd.addProperty(new PropertyDescriptor("WRECKYOU", cType, cInst));
            //TypeManager.getInstance().recReplace(aGobd, aGobdNew, cGobd);
            //Type aTypeOld = aType;
            Type aType2 = TypeManager.getInstance().updateDescriptor(aGobd);

            TypeManager.getInstance().replace(aType, aType2);

            //List<object> awesome = TypeManager.getInstance().updateObjects(objList);

            //bool equal = (cInst == awesome[2]);
        }

    }
}
