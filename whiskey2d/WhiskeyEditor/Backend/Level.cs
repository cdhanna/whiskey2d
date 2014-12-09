using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class Level : DefaultObjectManager
    {

        //public List<InstanceDescriptor> Descriptors { get; private set; }
        public string LevelName { get; private set; }
        public Color BackgroundColor { get; set; }

        public Level(string name)
        {
            init();
            BackgroundColor = Color.Tomato;
            LevelName = name;
            //Descriptors = new List<InstanceDescriptor>();
            InstanceManager.Instance.addLevel(this);
        }

        public Level(State state) : this(state.Name)
        {
            init();
            setInstanceLevelState(state);
        }

        public void syncAllTypesToInstances()
        {
            getInstances().ForEach((i) =>
            {
                i.syncType();
            });
        }

        public void syncTypeToInstances(TypeDescriptor typeDescriptor)
        {
            getInstances().ForEach((i) =>
            {

                if (i.TypeDescriptor.Name.Equals(typeDescriptor.Name))
                {
                    i.syncType();
                }

            });

        }

        public State getInstanceLevelState()
        {
            //State state = new State();
            //state.Name = LevelName;
            //state.BackgroundColor = BackgroundColor;
            //Descriptors.ForEach((iDesc) => { 
            //    state.GameObjects.Add(iDesc); 
            //});
            //return state;
            State state = base.getState();
            state.BackgroundColor = BackgroundColor;
            return state;
        }

        public void setInstanceLevelState(State state)
        {
            //Descriptors.Clear();
            //BackgroundColor = state.BackgroundColor;
            //state.GameObjects.ForEach((gob) =>
            //{
            //    Descriptors.Add((InstanceDescriptor)gob);
            //});
            BackgroundColor = state.BackgroundColor;
            base.setState(state);

            //getInstances().ForEach((i) =>
            //{
            //    i.syncType();
            // //   i.registerListeners();

            //});
        }

        public void addObject(InstanceDescriptor gob)
        {
            base.addObject(gob);
        }

        public List<InstanceDescriptor> getInstances()
        {
            List<InstanceDescriptor> list = new List<InstanceDescriptor>();
            foreach (GameObject gob in getAllObjects())
            {
                list.Add((InstanceDescriptor)gob);
            }
            return list;
        }

        //public State getWhiskeyLevelState()
        //{

        //}
       


    }
}
