using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor.Controls
{
    public partial class ScriptLibrary : UserControl
    {

        Dictionary<ScriptDescriptor, Button> descButtTable;

        public ScriptLibrary()
        {
            InitializeComponent();

            descButtTable = new Dictionary<ScriptDescriptor, Button>();

        }

        public void scriptSaved(Editor editor, ScriptDescriptor sDesc)
        {

            if (!descButtTable.ContainsKey(sDesc))
            {
                Button butt = new Button();
                butt.Text = sDesc.Name;
                butt.BackColor = Color.BurlyWood;
                flowPanel.Controls.Add(butt);

                butt.Click += (evt, sender) =>
                {
                    editor.setActiveScript(sDesc);
                };


                descButtTable.Add(sDesc, butt);
            }





        }

    }
}
