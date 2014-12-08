using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using System.IO;
using Whiskey2D.Core;

using System.Reflection;

namespace WhiskeyEditor.Backend
{


    [Serializable]
    public class LevelDescriptor : FileDescriptor
    {

        private List<PropertyDescriptor> propDescs = new List<PropertyDescriptor>();

        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                Level.BackgroundColor = color;
                ProjectManager.Instance.ActiveProject.saveGameData();
            }
        }

        public Level Level { get; private set; }
        //private State state;

        //public State State { get { return state; } }

        public LevelDescriptor(string name)
            : base(ProjectManager.Instance.ActiveProject.PathStates + Path.DirectorySeparatorChar + name + ".state", name)
        {
            Level = new Level(name);
            Color = Level.BackgroundColor;
           
           

            FileManager.Instance.addFileDescriptor(this);
        }

        private void addPropertyDescriptor(PropertyDescriptor prop)
        {
            propDescs.Add(prop);
        }

        public List<PropertyDescriptor> getPropertySet()
        {
            return propDescs;
        }
        

        public override void inspectFile()
        {
            Level.setInstanceLevelState(State.deserialize(FilePath));
            
            //colorProperty.TypeVal.Value = Level.BackgroundColor;
            Level.BackgroundColor = Color;
            //State.serialize(Level.getInstanceLevelState(), FilePath);

            //state = State.deserialize(FilePath);
        }

        public override void createFile()
        {
            State state = Level.getInstanceLevelState();
            State.serialize(state, FilePath);
        }

    }
}
