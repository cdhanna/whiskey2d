using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using System.Reflection;
using Whiskey2D.Services;
using System.Diagnostics;

namespace WhiskeyEditor.ClassLoader
{


    class TestMain : MarshalByRefObject
    {

  

        [STAThread]
        static void Main()
        {

            Stopwatch stopWatch = new Stopwatch();
            
            

            Project.ProjectManager.Instance.ActiveProject = Project.ProjectManager.Instance.createNewProject("HomeTest", "HomeTest");
            stopWatch.Start();
            ServiceCollection servColl = new ServiceCollection();


            
            GameObjectDescriptor gobdA = new GameObjectDescriptor(servColl, "Project", "A");
           // GameObjectDescriptor gobdB = new GameObjectDescriptor(servColl, "Project", "B");


            gobdA.compileToDisc();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("TIME: " + ts.TotalMilliseconds);

            //Service instA = gobdA.createServiceInstance();



            //gobdB.addProperty(new PropertyDescriptor("R", typeof(GameObject), (GameObject)instA));
            //gobdB.compileToDisc();

            //Service instB = gobdB.createServiceInstance();

            //gobdA.addProperty(new PropertyDescriptor("T", typeof(GameObject), (GameObject)instB));
            
            //gobdA.compileToDisc();
            //gobdA.addProperty(new PropertyDescriptor("Nine", typeof(int), 9));
            //servColl = gobdA.compileToDisc(servColl);
            //Service refInst = servColl.get(0);

          //  GameObjectService inst2 = gobdA.createServiceInstance();


            //Console.WriteLine("OLD INST NINE = " + refInst.getServiceProperty("Nine").get(inst));
            //Console.WriteLine("OLD INST EIGHT = " + refInst.getServiceProperty("Eight").get(inst));
            //gobdB.addProperty(new PropertyDescriptor("R", typeof(GameObject), (GameObject)inst));
            //gobdB.compileToDisc();

            //GameObjectService instB = gobdB.createServiceInstance();
            


            //object instA = instB.getServiceProperty("R").get(instB);
            //;
            //Console.WriteLine("VALUE IS " + inst.getServiceProperty("Eight").get(inst));
            
           
            //gobd.compileToDisc();


            //inst = gobd.createServiceInstance();
            //Console.WriteLine("VALUE IS " + inst.getServiceProperty("Nine").get(inst));


            //Type gobdType = TypeManager.getInstance().addDescriptor(gobd);


           // ServiceHandle<GameObjectService> sHandle = new ServiceHandle<GameObjectService>(Project.ProjectManager.Instance.ActiveProject.PathBin + "\\Project.A.dll", "Project.A");

           // Library lib = ServiceLoader.Instance.createLibrary("test");
           // GameObjectService gobs = lib.instantiate(sHandle);


           // ServiceProperty serv = gobs.getServiceProperty("Eight");
           // serv.set(gobs, 100);
            
           // Console.WriteLine(serv.get(gobs));
           //// try
           // {
           //     ServiceProperty[] props = gobs.getServiceProperties();
           //     foreach (ServiceProperty p in props)
           //     {
                    
           //         object val = p.get(gobs);
           //         //Console.WriteLine(val);
                   
           //     }

           // }
          

           // lib.unload();

           // gobd.addProperty(new PropertyDescriptor("Seven", typeof(int), 7));
           // gobd.compile();



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
