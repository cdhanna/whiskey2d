using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{
    public abstract class Script
    {

        private GameObject gob;


        
        public Script()
        {
            
        }

        public GameObject Gob
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
}
