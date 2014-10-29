using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;

namespace WhiskeyEditor.ClassLoader
{

    class ScriptTokens
    {
        public const string NAME = "__SCRIPT_NAME__";
        public const string ON_START = "__SCRIPT_ON_START__";
        public const string ON_UPDATE = "__SCRIPT_ON_UPDATE__";
        public const string TYPE = "__SCRIPT_GOB_TYPE__";
        public const string TEMPLATE = @"
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;

namespace Project.Scripts
{
    //Change name and type of GameObject
    public class " + NAME + @" : Script<" + TYPE + @">
    {
        
        public override void onStart()
        {
           " + ON_START + @"
        }

        public override void onUpdate()
        {
           " + ON_UPDATE + @"
        }
    } 
}";
    }

    class ScriptDescriptor
    {

        public string Name { get; set; }
        private Type GameObjectType { get; set; }
        private string OnStart { get; set; }
        private string OnUpdate { get; set; }


        public ScriptDescriptor(string name)
        {
            Name = name;
            GameObjectType = typeof(GameObject);
            OnStart = "//fill in code that runs when the script is added to the gameObject";
            OnUpdate = "//fill in code that runs everytime the gameObject is updated (every frame)";
        }

        public string generateScript()
        {
            return ScriptTokens.TEMPLATE.Replace(
                ScriptTokens.NAME, Name).Replace(
                ScriptTokens.ON_START, OnStart).Replace(
                ScriptTokens.ON_UPDATE, OnUpdate).Replace(
                ScriptTokens.TYPE, GameObjectType.Name);
        }

    }
}
