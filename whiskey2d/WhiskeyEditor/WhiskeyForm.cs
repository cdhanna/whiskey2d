using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor
{
    public partial class WhiskeyForm : Form
    {
        ProjectManager projMan;


        public WhiskeyForm()
        {
            InitializeComponent();

            projMan = new ProjectManager();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = this.openProjectDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = this.openProjectDialog.InitialDirectory + this.openProjectDialog.FileName;

                Project project = projMan.loadProject(path);
                projMan.setTreeFor(project, this.directoryTree);
                
            }

          

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.newProjectDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string directory = this.newProjectDialog.FileName;
                string name = directory.Substring(directory.LastIndexOf('\\') + 1);
                //directory = directory.Substring(0, directory.Length - name.Length);

                Project project = projMan.createNewProject(directory, name);
                projMan.setTreeFor(project, this.directoryTree);
                

            }
            else
            {
                debug.Text = "FILE NOT OKAY";
            }
        }

        private void newProjectDialog_FileOk(object sender, CancelEventArgs e)
        {
           
        }
    }
}
