using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class Level
    {

        public List<InstanceDescriptor> Descriptors { get; private set; }
        public string LevelName { get; private set; }


        public Level(string name)
        {
            LevelName = name;
            Descriptors = new List<InstanceDescriptor>();
            InstanceManager.Instance.addLevel(this);
        }

        public Level(State state) : this(state.Name)
        {
            setInstanceLevelState(state);
        }

        public State getInstanceLevelState()
        {
            State state = new State();
            state.Name = LevelName;
            Descriptors.ForEach((iDesc) => { 
                state.GameObjects.Add(iDesc); 
            });
            return state;
        }

        public void setInstanceLevelState(State state)
        {
            Descriptors.Clear();
            state.GameObjects.ForEach((gob) =>
            {
                Descriptors.Add((InstanceDescriptor)gob);
            });
        }

        //public State getWhiskeyLevelState()
        //{

        //}
       


    }
}
