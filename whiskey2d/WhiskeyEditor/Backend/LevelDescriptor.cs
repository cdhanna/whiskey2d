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

       // private List<PropertyDescriptor> propDescs = new List<PropertyDescriptor>();

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
        
        public EditorLevel Level { get; private set; }
        //private State state;

        //public State State { get { return state; } }

        public LevelDescriptor (string basePath, string name)
            : base(basePath, name)
        {
            Level = new EditorLevel(name);
            Color = Level.BackgroundColor;

            if (ProjectManager.Instance.ActiveProject.GameStartScene.Equals("default"))
            {
                ProjectManager.Instance.ActiveProject.GameStartScene = name;
            }

            FileManager.Instance.addFileDescriptor(this);
        }

        public LevelDescriptor(string name)
            : this(ProjectManager.Instance.ActiveProject.PathStates + Path.DirectorySeparatorChar + name + ".state", name)
        {
            
        }

        //private void addPropertyDescriptor(PropertyDescriptor prop)
        //{
        //    propDescs.Add(prop);
        //}

        //public List<PropertyDescriptor> getPropertySet()
        //{
        //    return propDescs;
        //}

        public override void save()
        {
            EditorLevel.serialize(Level, FilePath);
        }

        public override void inspectFile()
        {
            Level = EditorLevel.deserialize(FilePath);
            
            //Level.setInstanceLevelState(State.deserialize(FilePath));
            
            //colorProperty.TypeVal.Value = Level.BackgroundColor;
            Level.BackgroundColor = Color;
            
            //State.serialize(Level.getInstanceLevelState(), FilePath);

            //state = State.deserialize(FilePath);
        }

        public override void createFile()
        {
            //State state = Level.getInstanceLevelState();
            //State.serialize(state, FilePath);
            save();
        }

    }
}
