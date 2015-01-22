using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents
{
    class CoreDescriptorControl : UserControl
    {
        private Label typeLbl;
        private Label nameLbl;
        private TextBox descBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;

        public CoreDescriptorControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.typeLbl = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.descBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // typeLbl
            // 
            this.typeLbl.AutoSize = true;
            this.typeLbl.Location = new System.Drawing.Point(6, 8);
            this.typeLbl.Name = "typeLbl";
            this.typeLbl.Size = new System.Drawing.Size(81, 15);
            this.typeLbl.TabIndex = 0;
            this.typeLbl.Text = "Script or Type";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(3, 23);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(94, 32);
            this.nameLbl.TabIndex = 1;
            this.nameLbl.Text = "Name";
            // 
            // descBox
            // 
            this.descBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descBox.Location = new System.Drawing.Point(3, 83);
            this.descBox.Multiline = true;
            this.descBox.Name = "descBox";
            this.descBox.ReadOnly = true;
            this.descBox.Size = new System.Drawing.Size(540, 282);
            this.descBox.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.descBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 368);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nameLbl);
            this.panel1.Controls.Add(this.typeLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 74);
            this.panel1.TabIndex = 3;
            // 
            // CoreDescriptorControl
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CoreDescriptorControl";
            this.Size = new System.Drawing.Size(546, 368);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        public void setForDescriptor(CoreDescriptorDocument doc)
        {
            string type = "Script";

            if (doc.Descriptor is CoreTypeDescriptor)
            {
                type = "Type";
            }

            typeLbl.Text = type;
            nameLbl.Text = doc.Descriptor.Name;
            descBox.Text = doc.Descriptor.Description;
        }

    }
}
