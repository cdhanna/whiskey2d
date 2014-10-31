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
using Whiskey2D.Core;

namespace WhiskeyEditor.Controls
{
    public partial class ScriptCollection : UserControl
    {

        private List<ScriptIcon> scriptIcons;

        public GameObject SelectedObject { get; set; } 

        public ScriptCollection()
        {
            scriptIcons = new List<ScriptIcon>();
            InitializeComponent();
        }

        public override void Refresh()
        {
            base.Refresh();
        }

        private void scriptNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addScriptBtn_Click(object sender, EventArgs e)
        {
            string requestedScriptName = scriptNameBox.Text;


            ScriptDescriptor sdesc = ScriptManager.Instance.getFromName(requestedScriptName);

            //Type scriptType = ScriptManager.Instance.getTypeOfScript(requestedScriptName);
            //ScriptManager.Instance.addScriptToGob(gob, scriptType);

            ScriptIcon sIcon = new ScriptIcon(sdesc);

            sIcon.closeBtn.Click += (sndr, args) =>
            {
                scriptIcons.Remove(sIcon);
                flowPanel.Controls.Remove(sIcon);
            };

            //sIcon.ScriptDescriptor = something;

            scriptIcons.Add(sIcon);
            flowPanel.Controls.Add(sIcon);


            try
            {
                if (SelectedObject != null)
                {

                    
                    SelectedObject.addScript( sdesc.generateInstance() );

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine("could not add script");
            }


        }
    }
}
