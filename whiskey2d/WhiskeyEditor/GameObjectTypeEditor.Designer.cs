namespace WhiskeyEditor
{
    partial class GameObjectTypeEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propPanel = new System.Windows.Forms.Panel();
            this.addPropertyButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.objectTypeNameBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.propPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.headerPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.52882F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.47118F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // propPanel
            // 
            this.propPanel.AutoScroll = true;
            this.propPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propPanel.Location = new System.Drawing.Point(3, 55);
            this.propPanel.Name = "propPanel";
            this.propPanel.Size = new System.Drawing.Size(393, 395);
            this.propPanel.TabIndex = 1;
            // 
            // addPropertyButton
            // 
            this.addPropertyButton.Location = new System.Drawing.Point(275, 3);
            this.addPropertyButton.Name = "addPropertyButton";
            this.addPropertyButton.Size = new System.Drawing.Size(115, 23);
            this.addPropertyButton.TabIndex = 0;
            this.addPropertyButton.Text = "Add Property";
            this.addPropertyButton.UseVisualStyleBackColor = true;
            this.addPropertyButton.Click += new System.EventHandler(this.addPropertyButton_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.objectTypeNameBox);
            this.headerPanel.Controls.Add(this.nameLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(3, 3);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(393, 46);
            this.headerPanel.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 13);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(53, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name: ";
            // 
            // objectTypeNameBox
            // 
            this.objectTypeNameBox.Location = new System.Drawing.Point(62, 10);
            this.objectTypeNameBox.Name = "objectTypeNameBox";
            this.objectTypeNameBox.Size = new System.Drawing.Size(203, 22);
            this.objectTypeNameBox.TabIndex = 1;
            this.objectTypeNameBox.Text = "unnamedObjectType";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addPropertyButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 456);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 31);
            this.panel1.TabIndex = 0;
            // 
            // GameObjectTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GameObjectTypeEditor";
            this.Size = new System.Drawing.Size(399, 490);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel propPanel;
        private System.Windows.Forms.Button addPropertyButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.TextBox objectTypeNameBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel panel1;

    }
}
