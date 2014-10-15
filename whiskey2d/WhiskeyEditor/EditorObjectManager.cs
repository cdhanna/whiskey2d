using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;
using WhiskeyEditor.EditorObjects;
using WhiskeyEditor.MonoHelp;


namespace WhiskeyEditor
{
    public class EditorObjectManager : DefaultObjectManager
    {

        public EditorObjectManager()
        {
            UpdateGameObjects = false;
        }

        public Boolean UpdateGameObjects { get; set; }

        public void addObjects(List<GameObject> gobs)
        {
            gameObjects.AddRange(gobs);
        }

        public override void updateAll()
        {
           
            foreach (GameObject gob in gameObjects)
            {
                if (UpdateGameObjects || gob is EditorGameObject)
                {
                    gob.update();
                }
            }
            

            foreach (GameObject gob in deadObjects)
            {
                gameObjects.Remove(gob);
            }
            foreach (GameObject gob in newObjects)
            {
                gameObjects.Add(gob);
                gob.init();
            }


            deadObjects.Clear();
            newObjects.Clear();
        }

        public override State getState()
        {

            State state = new State();
            List<GameObject> gobs = getAllObjectsNotOfType<EditorGameObject>();
            GameObject[] objs = new GameObject[gobs.Count];
            gobs.CopyTo(objs);
            state.GameObjects = objs.ToList();
            return state;
        
        }

        public override void setState(State state)
        {
            deadObjects.Clear();
            newObjects.Clear();
            List<GameObject> gobs = getAllObjectsNotOfType<EditorGameObject>();
            foreach (GameObject gob in gobs)
            {
                gameObjects.Remove(gob);
            }




            GameObject[] objs = new GameObject[state.GameObjects.Count];
            state.GameObjects.CopyTo(objs);
            newObjects = objs.ToList();
        }

    }
}
