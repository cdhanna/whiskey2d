using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;


namespace WhiskeyEditor.compile_types.Scripts
{
    [Serializable]
    class SoundZoneControl : CoreScriptDescriptor
    {
        public SoundZoneControl()
            : base("SoundZoneControl", "SoundZone")
        {

        }

        public override string getClassCode()
        {
            String code = @"bool active = true;
		                    bool inside = false;
		                    List<Sound> sounds;";
            return code;
        }

        public override string getStartCode()
        {
            String code = @"sounds = new List<Sound>();";
            return code;
        }

        public override string getUpdateCode()
        {
            String code = @"GameObject tripee = Objects.getObject(Gob.TripOn);
			                if (tripee != null && Gob.PlayCount != 0){
				                if (tripee.Bounds.boundWithin(Gob.Bounds)){
					
					
					                if (!inside){
						                //ON ENTER
						                Gob.PlayCount --;
						                Sound s = new Sound(Gob.Sound);
						                s.play();
						                s.Looped = Gob.Loop;
						                sounds.Add(s);
					                }
					
					                inside = true;
				                } else {
					                if (inside){
						                Gob.PlayCount --;
						                Sound s = new Sound(Gob.Sound);
						                s.Looped = Gob.Loop;
						                s.play();
						                sounds.Add(s);
					                }
					                inside =false;
				                }
			                }";
            return code;
        }

        public override string getCloseCode()
        {
            String code = "sounds.ForEach( s => s.stop());";
            return code;
        }
    }
}
