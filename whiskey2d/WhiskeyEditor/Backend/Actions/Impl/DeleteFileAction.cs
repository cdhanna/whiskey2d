using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Documents;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class DeleteFileAction : AbstractAction
    {
        public LibraryView LibraryView { get; private set; }

        public DeleteFileAction(LibraryView libView)
            : base("Delete", AssetManager.ICON_MINUS)
        {
            LibraryView = libView;
        }

        protected override void run()
        {
            LibraryView.deleteSelectedNode();
        }
    }
}
