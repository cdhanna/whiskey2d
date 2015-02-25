using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Whiskey2D.Core.Hud.Commands
{
    class ReadValueCommand : ConsoleCommand
    {

        public ReadValueCommand() : base("get") { }

        public override void run(WhiskeyConsole console, string[] args)
        {

            if (args.Length != 2)
            {
                console.writeLine("Must supply object name and variable name.");
            }
            else
            {

                String path = args[1];
                String[] pathNodes = path.Split('.');

                String objName = pathNodes[0];

                GameObject obj = GameManager.Objects.getObject(objName);
                if (obj == null)
                {
                    console.writeLine("Could not find object, " + objName);
                }
                else
                {
                    console.writeLine("OBJ: " + obj.Name);

                    object latestObject = obj;

                    for (int i = 1; i < pathNodes.Length; i ++ )
                    {
                        string nodeName = pathNodes[i];

                        if (latestObject == null)
                        {
                            console.writeLine("Value is null");
                            break;
                        }

                        PropertyInfo[] props = latestObject.GetType().GetProperties();
                        PropertyInfo prop = props.ToList().Find(p => p.Name.Equals(nodeName));

                        if (prop == null)
                        {
                            console.writeLine("No property named " + nodeName);
                            break;
                        }

                        object nextObject = prop.GetValue(latestObject);
                        latestObject = nextObject;
                    }

                    if (latestObject != null)
                    {
                        console.writeLine(path + " = " + latestObject);
                    }


                }
            }
        }



    }
}
