using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Core
{

   
    [Serializable]
    public abstract class Script
    {

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


        public abstract void onStart();
        public abstract void onUpdate();

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

       // public abstract void onStart();
       // public abstract void onUpdate();
    }


    ///// <summary>
    ///// A delegate that supports a no-arg call to a void return type. A.K.A., the update function
    ///// </summary>
    //public delegate void UpdateFunctionPointer();

    ///// <summary>
    ///// A ScriptBundle gives access to the GameObject, and the function that updates that GameObject
    ///// </summary>
    ///// <typeparam name="T">The type of GameObject that the script works with</typeparam>
    //[Serializable]
    //public class ScriptBundle<T> where T : GameObject
    //{

    //    public const string CODE_GOB = "Gob";
    //    public const string CODE_START = "onStart";
    //    public const string CODE_UPDATE = "onUpdate";

    //    protected ScriptBundle(object script)
    //    {

    //        if (script != null)
    //        {

    //            this.script = script;

    //        }
    //    }

    //    /// <summary>
    //    /// The GameObject
    //    /// </summary>
    //    public T Gob { get; set; }


    //    public string ScriptName { get { return script.GetType().Name; } }


    //    protected object script;
    //    public object Script { get { return script; } }

    //    /// <summary>
    //    /// The pointer to a function
    //    /// </summary>
    //    private UpdateFunctionPointer UpdatePointer { get; set; }

    //    private UpdateFunctionPointer StartPointer { get; set; }

    //    public void start()
    //    {
    //        StartPointer();
    //    }

    //    public void update()
    //    {
    //        UpdatePointer();
    //    }


    //    /// <summary>
    //    /// Very Dangerous
    //    /// </summary>
    //    /// <param name="script"></param>
    //    /// <returns></returns>
    //    public static ScriptBundle<GameObject> createFrom(object script)
    //    {

    //        //validate script

    //        ScriptBundle<GameObject> gs = new ScriptBundle<GameObject>(script);

    //        gs.StartPointer = (UpdateFunctionPointer)Delegate.CreateDelegate(typeof(UpdateFunctionPointer), script, CODE_START, false);
    //        gs.UpdatePointer = (UpdateFunctionPointer)Delegate.CreateDelegate(typeof(UpdateFunctionPointer), script, CODE_UPDATE, false);


    //        gs.Gob = (GameObject)script.GetType().GetProperty(CODE_GOB).GetValue(script, new object[] { });

    //        return gs;
    //    }

    //    public static ScriptBundle<GameObject> createFrom<X>(Script<X> script) where X : GameObject
    //    {
    //        ScriptBundle<GameObject> gs = new ScriptBundle<GameObject>(script);
    //        gs.UpdatePointer = new UpdateFunctionPointer(script.onUpdate);
    //        gs.StartPointer = new UpdateFunctionPointer(script.onStart);
    //        gs.Gob = script.Gob;

    //        return gs;
    //    }


    //}


    ///// <summary>
    ///// The base class for all scripts.
    ///// Scripts control GameObject behavour.
    ///// </summary>
    //[Serializable]
    //public abstract class Script<G> : ScriptBundle<G> where G : GameObject
    //{




    //    ///// <summary>
    //    ///// creates a new Script
    //    ///// </summary>
    //    public Script()
    //        : base(null)
    //    {
    //        script = this;
    //    }

    //    /// <summary>
    //    /// Called once upon startup of the GameObject
    //    /// </summary>
    //    public abstract void onStart();

    //    /// <summary>
    //    /// Called upon every update cycle of the GameObject
    //    /// </summary>
    //    public abstract void onUpdate();


    //}
}