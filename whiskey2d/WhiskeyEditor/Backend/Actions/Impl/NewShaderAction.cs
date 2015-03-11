using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewShaderAction : NewFileAction
    {
        

        public NewShaderAction()
            : base("New Shader", AssetManager.ICON_FILE_PICTURE)
        {
        }


        protected override void beforeShow(UI.Menu.NewFileForm form)
        {
            form.setForShader(Path);
        }


    }
}
