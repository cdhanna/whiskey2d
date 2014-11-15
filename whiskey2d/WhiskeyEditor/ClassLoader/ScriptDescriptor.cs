using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Controls;

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
using Project;
namespace Project.Scripts
{
    //Change name and type of GameObject
    [Serializable]
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

        private const string OnStart = "//fill in code that runs when the script is added to the gameObject";
        private const string OnUpdate = "//fill in code that runs everytime the gameObject is updated (every frame)";
        public static string getEmptyCode(string name, Type gobType)
        {
            return  ScriptTokens.TEMPLATE.Replace(
                    ScriptTokens.NAME, name).Replace(
                    ScriptTokens.ON_START, OnStart).Replace(
                    ScriptTokens.ON_UPDATE, OnUpdate).Replace(
                    ScriptTokens.TYPE, gobType.Name);
        }

    }

    public class ScriptDescriptor
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {

                name = value;
            }
        }
        private Type GameObjectType { get; set; }

        public List<GameObject> gobRefs;

        private static int sid = 0;
        private int id;
        public int Id { get { return id; } }


        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public ScriptCollection ScriptCollection { get; set; }

        public void refreshAllGobRefs()
        {
            //foreach (GameObject gob in gobRefs)
            //{
            //    List<ScriptBundle<GameObject>> scripts = gob.getScriptBundles();
            //    List<ScriptBundle<GameObject>> marked = new List<ScriptBundle<GameObject>>();
            //    foreach (ScriptBundle<GameObject> script in scripts)
            //    {
            //        if (script.ScriptName.Equals(Name))
            //        {
            //            marked.Add(script);
            //        }
            //    }
            //    foreach (ScriptBundle<GameObject> script in marked)
            //    {
            //        gob.removeScript(script);
            //        gob.addScript(generateInstance());
            //    }
            //}
        }

        public void addGobRef(GameObject gob)
        {
            gobRefs.Add(gob);
        }

        public void removeGobRef(GameObject gob)
        {
            gobRefs.Remove(gob);
        }

        //private string latestDllPath = null;
        //public string LatestDllPath
        //{
        //    get
        //    {
        //        return latestDllPath;
        //    }
        //    set
        //    {
        //        latestDllPath = value;
        //    }

        //}
        public string LatestDllPath { get; set; }
        public Type LatestBuiltType { get; set; }

        public ScriptDescriptor(string name)
        {
            gobRefs = new List<GameObject>();
            id = sid++;
            Name = name;
            GameObjectType = typeof(GameObject);

            code = ScriptTokens.getEmptyCode(Name, GameObjectType);

            ScriptManager.Instance.addScriptDescriptor(this);
        }

        public object generateInstance()
        {
            if (LatestBuiltType == null)
            {
                return null;
            }
            else
            {
                return LatestBuiltType.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
        }
    }
}
