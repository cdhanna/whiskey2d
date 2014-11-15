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

        public void totalRefresh(ScriptDescriptor sdesc)
        {
            List<ScriptIcon> icons = new List<ScriptIcon>();
            foreach (GameObject gob in sdesc.gobRefs)
            {
                SelectedObject = gob;
                Refresh();

                foreach (ScriptIcon icon in scriptIcons)
                {
                    if (icon.ScriptDescriptor.Equals(sdesc))
                    {
                        icons.Add(icon);
                        
                    }
                }

            }

            foreach (ScriptIcon icon in icons)
            {
                icon.closeBtn.PerformClick();
                addScriptLogic(sdesc.Name);
            }
        }

        public override void Refresh()
        {

            scriptIcons.Clear();
            flowPanel.Controls.Clear();

            if (SelectedObject != null)
            {

                List<ScriptBundle<GameObject>> scriptBundles = SelectedObject.getScriptBundles();
                foreach (ScriptBundle<GameObject> sb in scriptBundles)
                {
                    string scriptName = sb.ScriptName;
                    ScriptDescriptor sdesc = ScriptManager.Instance.getFromName(scriptName);
                    addScriptUI(sdesc, sb.Script);
                }
            }


            base.Refresh();
        }

        private void scriptNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addScriptBtn_Click(object sender, EventArgs e)
        {
            string requestedScriptName = scriptNameBox.Text;
            addScriptLogic(requestedScriptName);
            

        }

        public void addScriptLogic(string scriptName)
        {
            ScriptDescriptor sdesc = ScriptManager.Instance.getFromName(scriptName);
            sdesc.ScriptCollection = this;
            object scriptObject = null;
            try
            {
                if (SelectedObject != null)
                {
                    scriptObject = sdesc.generateInstance();
                    SelectedObject.addScript(scriptObject);
                    sdesc.addGobRef(SelectedObject);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }




            addScriptUI(sdesc, scriptObject);

        }


        private void addScriptUI(ScriptDescriptor sdesc, object scriptObject)
        {
            ScriptIcon sIcon = new ScriptIcon(sdesc);

            sIcon.closeBtn.Click += (sndr, args) =>
            {
                scriptIcons.Remove(sIcon);
                flowPanel.Controls.Remove(sIcon);
                sdesc.removeGobRef(SelectedObject);
                SelectedObject.removeScript(scriptObject);

            };


            scriptIcons.Add(sIcon);
            flowPanel.Controls.Add(sIcon);
        }

    }
}