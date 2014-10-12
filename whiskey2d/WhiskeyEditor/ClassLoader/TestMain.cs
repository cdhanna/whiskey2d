using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using System.Reflection;




namespace WhiskeyEditor.ClassLoader
{


    static class TestMain
    {

  

        [STAThread]
        static void Main()
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

            //TypeManager.getInstance().recReplace(aGobd, aGobdNew, cGobd);
            //Type aTypeOld = aType;
            Type aType2 = TypeManager.getInstance().updateDescriptor(aGobd);

            TypeManager.getInstance().replace(aType, aType2);

            List<object> awesome = TypeManager.getInstance().updateObjects(objList);



        }

    }
}
