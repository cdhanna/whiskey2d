using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.compile_types.Scripts
{
    [Serializable]
    class CameraMasterControl : CoreScriptDescriptor
    {

        public CameraMasterControl()
            : base("CameraMasterControl", "CameraMaster")
        {

        }


        public override string getClassCode()
        {


            return "CamZone lastZone = null; bool lastZoneFlag = false;";

        }

        public override string getStartCode()
        {

            String code = "";
            code += "Level.Camera.Zoom = 1f;";
            code += "Level.Camera.PositionSpeed = Gob.TranslationSpeed;";
            code += "Level.Camera.ZoomSpring = Gob.ZoomSpeed;";
            code += "Level.Camera.ZoomFriction = Gob.ZoomFriction;";
            return code;
        }

        public override string getUpdateCode()
        {
            String code = "";
            code += "GameObject target = Objects.getObject(Gob.Target);";
            code += "if (target != null) {";
            code += "var zones = target.currentCollisions<CamZone>();";
            code += "float l = 0, r = 0, t = 0, b = 0, z = 0;";
            code += "if (zones.Count == 1){"
                + "lastZone = zones[0].Gob;"
                + "lastZoneFlag = false;"
            + "} else if (zones.Count == 2){"
                + "if (!lastZoneFlag){"
                    + "lastZoneFlag = true;"
                    + "if (zones[1].Gob == lastZone)"
                    + "	lastZone = zones[0].Gob;"
                    + "else lastZone = zones[1].Gob;"
                + "}"
            + "}"
            + "l = lastZone.Bounds.Left;"
            + "r = lastZone.Bounds.Right;"
            + "t = lastZone.Bounds.Top;"
            + "b = lastZone.Bounds.Bottom;"
            + "z = lastZone.Zoom;"


            
            + "if (Gob.ObeyCamZones) { "
            + " Level.Camera.TargetZoom = z;"
            + " Level.Camera.followClamped(target, l, t, r, b); }"
            + "else Level.Camera.follow(target);";
            code += "}";
            
            return code;
        }


    }
}
