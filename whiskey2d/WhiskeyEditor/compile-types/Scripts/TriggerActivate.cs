using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Scripts
{
    [Serializable]
    class TriggerActivate : CoreScriptDescriptor
    {
        public TriggerActivate()
            : base("TriggerActivate", "TriggerZone")
        {
            
        }

        public override string getStartCode()
        {
            

            return base.getStartCode();
        }
        public override string getUpdateCode()
        {
            return @"
GameObject tripperObj = GameManager.Objects.getObject(Gob.Tripper);
GameObject tripObj = GameManager.Objects.getObject(Gob.Trip);
if (!Gob.Tripped && tripperObj != null && tripObj != null){
    if (Gob.Bounds.boundWithin(tripperObj.Bounds)){
        tripObj.Active = true;
        Gob.Tripped = true;
    }
}

"; 
            
            //return base.getUpdateCode();
        }
    }
}
