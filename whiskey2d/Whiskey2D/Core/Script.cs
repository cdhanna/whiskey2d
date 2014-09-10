using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    public abstract class Script <G> where G:GameObject
    {

        private G gob;


        
        public Script()
        {
            
        }

        public G GameObject
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

       


        //public Type getGameObjectType()
        //{
        //    return typeof(G);
        //}

        public abstract void onStart();
        public abstract void onUpdate();

    }
}
