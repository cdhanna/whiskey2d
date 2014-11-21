using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using System.Reflection;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;
using WhiskeyEditor.Project;
using System.IO;

namespace WhiskeyEditor.Backend.Managers
{
    class InstanceManager
    {
        private static InstanceManager instance = new InstanceManager();
        public static InstanceManager Instance { get { return instance; } }

        private List<InstanceDescriptor> iDescs;
        
        private InstanceManager()
        {
            iDescs = new List<InstanceDescriptor>();
        }


        public void addInstance(InstanceDescriptor iDesc)
        {
            iDescs.Add(iDesc);
        }

        public string convertToGobs(string dllPath, string stateName)
        {

            Assembly gameData = Assembly.LoadFrom(dllPath);

            List<GameObject> lGobs = new List<GameObject>();            
            foreach (InstanceDescriptor iDesc in iDescs)
            {

                string typeName = iDesc.TypeDescriptor.QualifiedName;
                Type type = gameData.GetType(typeName, true, false);

                GameObject gob = (GameObject)type.GetConstructor
                    (new Type[] { }).Invoke
                    (new object[] { });

                foreach (PropertyDescriptor typeProp in iDesc.TypeDescriptor.getPropertySetClone())
                {
                    PropertyInfo propInfo = gob.GetType().GetProperty(typeProp.Name);
                    if (propInfo.SetMethod != null)
                    {
                        propInfo.GetSetMethod().Invoke(gob, new object[]{iDesc.getTypeValOfName(typeProp.Name).value});
                    }
                }

                gob.clearScripts();

                foreach (String scriptName in iDesc.getScriptNames())
                {
                    Type scriptType = gameData.GetType(ScriptManager.Instance.lookUpScript(scriptName).QualifiedName, false);
                    object script = scriptType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    gob.addScript((Script)script);
                }


                lGobs.Add(gob);
            }
            State state = new State();
            state.GameObjects = lGobs;
            state.Name = stateName;
            string filename = State.serialize(
                                state,
                                ProjectManager.Instance.ActiveProject.PathStates + Path.DirectorySeparatorChar + state.Name + ".state");
            return filename;
        }

    }
}
