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
    public partial class SimpleTextPopup : Form
    {
        private DialogResult results;
        private string enteredText;

        public DialogResult Results { get { return results; } }
        public string EnteredText { get { return enteredText; } }

       
        

        public SimpleTextPopup()
        {
            
            InitializeComponent();
        }

        

        private void cancelButton_Click(object sender, EventArgs e)
        {
            results = DialogResult.Cancel;
            this.Close();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            results = DialogResult.OK;
            this.enteredText = this.textBox.Text;
            this.Close();
        }

       

    }
}
