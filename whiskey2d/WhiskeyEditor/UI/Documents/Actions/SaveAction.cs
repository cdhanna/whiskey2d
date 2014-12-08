using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class SaveAction : AbstractAction
    {
        private DocumentTab tab;

        public SaveAction(DocumentTab tab)
            : base("Save", AssetManager.ICON_SAVE)
        {
            this.tab = tab;
        }

        protected override void run()
        {
            tab.save(this);
        }
    }
}
