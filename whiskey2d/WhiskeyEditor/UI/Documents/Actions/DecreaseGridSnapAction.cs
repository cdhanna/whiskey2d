using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using Whiskey2D.Core.Managers;
using System.Windows.Forms;
using System.Drawing;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class DecreaseGridSnapAction : AbstractAction
    {
        public DecreaseGridSnapAction()
            : base("Decrease Grid", AssetManager.ICON_MINUS)
        {

        }

        protected override void run()
        {
            GridManager.Instance.decrease();
        }
    }
}