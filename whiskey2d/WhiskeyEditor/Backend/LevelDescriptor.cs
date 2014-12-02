using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using System.IO;
using Whiskey2D.Core;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    class LevelDescriptor : FileDescriptor
    {
        public Level Level { get; private set; }
        //private State state;

        //public State State { get { return state; } }

        public LevelDescriptor(string name)
            : base(ProjectManager.Instance.ActiveProject.PathStates + Path.DirectorySeparatorChar + name + ".state", name)
        {
            Level = new Level(name);

            FileManager.Instance.addFileDescriptor(this);
        }


        public override void inspectFile()
        {
            Level.setInstanceLevelState(State.deserialize(FilePath));
            //state = State.deserialize(FilePath);
        }

        public override void createFile()
        {
            State state = Level.getInstanceLevelState();
            State.serialize(state, FilePath);
        }

    }
}
