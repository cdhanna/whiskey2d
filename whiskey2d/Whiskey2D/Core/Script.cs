using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{

   
    [Serializable]
    public abstract class Script
    {

        public Script()
        {
            Active = true;
        }

        public InputManager Input { get { return GameManager.Instance.InputManager; } }
        public InputSourceManager InputSource { get { return GameManager.Instance.InputSourceManager; } }
        public LogManager Log { get { return GameManager.Instance.LogManager; } }
        public ObjectManager Objects { get { return GameManager.Instance.ObjectManager; } }
        public RenderManager Renderer { get { return GameManager.Instance.RenderManager; } }
        public ResourceManager Resources { get { return GameManager.Instance.ResourceManager; } }
        public GameController Controller { get { return GameManager.Instance.GameController; } }
        public GameLevel Level { get { return GameManager.Instance.ActiveLevel; } }
        public int ScreenWidth { get { return GameManager.Instance.WindowScreenWidth; } }
        public int ScreenHeight { get { return GameManager.Instance.WindowScreenHeight; } }




        public virtual Type GobType
        {
            get
            {
                if (Gob == null)
                    return typeof(GameObject);
                else return Gob.GetType();
            }
        }

        private GameObject gob;

        public virtual GameObject Gob
        {
            get
            {
                return gob;
            }
            set
            {
                gob = value;
            }
        }

        public Boolean Active
        {
            get;
            set;
        }


        public abstract void onStart();
        public abstract void onUpdate();
        public abstract void onClose();


    }


    [Serializable]
    public abstract class Script<G> : Script where G : GameObject
    {

        public override Type GobType
        {
            get
            {
                return typeof(G);
            }
        }

        public new virtual G Gob
        {

            get
            {
                return (G)base.Gob;
            }
            set
            {
                if (value != null)
                {
                    if (!typeof(G).IsSubclassOf(value.GetType()) &&
                         !typeof(G).Equals(value.GetType()))
                    {
                        throw new WhiskeyRunTimeException("Cannot assigned script GOB of " + typeof(G) + " as a " + value.GetType());
                    }
                }
                base.Gob = value;
                //gob = value;
            }
        }

    }


}