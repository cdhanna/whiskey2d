using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.ex2
{
    public abstract class Gob
    {

        List<ScriptBundle<Gob>> scripts;

        public Gob()
        {
            scripts = new List<ScriptBundle<Gob>>();
            addScripts();
        }

        protected abstract void addScripts();

        public void updateScripts()
        {
            scripts.ForEach((s) => { s.updateMe(); });

        }


        public void addScript<G>(Script<G> script) where G : Gob
        {
            if (this.GetType() != typeof(G)) //runtime exception
            {
                throw new ScriptTypeException();
            }
            
            script.Gob = (G)this;

            ScriptBundle<Gob> converted = ScriptBundle<Gob>.createFrom(script);
            //ScriptBundle<Gob> converted = script.convert();
            


            this.scripts.Add(converted);

        }

        public class ScriptTypeException : Exception
        {
            
        }
    }
}
