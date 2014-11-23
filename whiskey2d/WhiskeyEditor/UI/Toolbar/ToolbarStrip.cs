using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Toolbar
{
    class ToolBarStrip : ToolStrip
    {

        private ToolStripButton btnCompile;

       
        public ToolBarStrip()
        {
            
            BackColor = UIManager.Instance.FlairColor;

            this.GripStyle = ToolStripGripStyle.Hidden;
            this.GripMargin = new Padding(0);
            this.Padding = new Padding(0);
            

            initControls();
            configureControls();
            addControls();

        }

        private void initControls()
        {
            btnCompile = new ToolStripButton("Compile");
            
        }
        private void configureControls()
        {
            btnCompile.Click += (s, a) =>
            {
                UIManager.Instance.Compiler.compile();
            };
        }
        private void addControls()
        {
            Items.Add(btnCompile);
        }


    }
}
