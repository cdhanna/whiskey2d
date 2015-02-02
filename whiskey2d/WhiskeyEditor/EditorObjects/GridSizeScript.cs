using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;
using Microsoft.Xna.Framework.Input;

namespace WhiskeyEditor.EditorObjects
{
    class GridSizeScript : Script<ObjectController>
    {
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            if (WhiskeyEditor.MonoHelp.WhiskeyControl.InputManager.isNewKeyDown(Keys.OemOpenBrackets))
            {
                GridManager.Instance.decrease();
            }
            if (WhiskeyEditor.MonoHelp.WhiskeyControl.InputManager.isNewKeyDown(Keys.OemCloseBrackets))
            {
                GridManager.Instance.increase();
            }
        }

        public override void onClose()
        {
        }
    }
}
