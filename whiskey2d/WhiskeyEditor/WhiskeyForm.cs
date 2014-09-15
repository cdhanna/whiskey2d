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
        public WhiskeyForm()
        {
            InitializeComponent();

            //this.newProjectDialog.ex

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
                string directory = this.openProjectDialog.InitialDirectory + this.openProjectDialog.FileName;
                //TODO validate this to make sure its a real Whiskey Game Directory
            }

          

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.newProjectDialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                
                Console.WriteLine(newProjectDialog.FileName);
                //this.debug.Text = this.newProjectDialog.fi;
            }
        }

        private void newProjectDialog_FileOk(object sender, CancelEventArgs e)
        {
           
        }
    }
}
