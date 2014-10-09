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

            GameObjectDescriptor gobd = new GameObjectDescriptor("SomeNameSpace", "TestClass");

            gobd.addProperty(new PropertyDescriptor("Name", typeof(String), "empty"));
            gobd.addProperty(new PropertyDescriptor("Pos", typeof(Vector), new Vector(43, 1235)));


            Type gobType = TypeManager.convertDescriptorToType(gobd);

            GameObjectDescriptor gobd3 = TypeManager.convertTypeToDescriptor(gobType);


            TypeManager.convertDescriptorToFile(gobd3);


            Type gobType2 = TypeManager.convertFileToType("SomeNameSpace\\TestClass.cs");


            GameObjectDescriptor gobd2 = TypeManager.convertTypeToDescriptor(gobType2);

            //gobd.generateSource("test_gen.cs");
            //Assembly code = gobd.generateSourceInMem();

            //Type type = code.GetType("SomeNameSpace.TestClass");
            //Console.WriteLine("TYPE IS " + type);

            //object obj = type.GetConstructor(new Type[] { }).Invoke(new Object[] { });
            
            //type.GetProperty("Name").SetValue(obj, "TestHello");
            //string objName = (string)type.GetProperty("Name").GetValue(obj);
            //Console.WriteLine("NAME IS " + objName);
            //type.GetProperty("Pos").SetValue(obj, new Vector(1, 3));
            //Vector objPos = (Vector)type.GetProperty("Pos").GetValue(obj);
            //Console.WriteLine("POS IS " + objPos.X);


        }

    }
}
