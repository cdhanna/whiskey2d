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

        private State state;

        public State State { get { return state; } }

        public LevelDescriptor(string name)
            : base(ProjectManager.Instance.ActiveProject.PathStates + Path.DirectorySeparatorChar + name + ".state", name)
        {

        }

        public override void inspectFile()
        {
            state = State.deserialize(FilePath);
        }

        public override void createFile()
        {
            state = new State();
            State.serialize(state, FilePath);
        }

    }
}
