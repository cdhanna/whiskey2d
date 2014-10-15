namespace WhiskeyEditor.Controls.TypeEditor
{
    partial class PropertyEditor
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.closeBt = new System.Windows.Forms.Button();
            this.vPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(4, 4);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(210, 22);
            this.nameBox.TabIndex = 0;
            this.nameBox.Text = "unnamedProp";
            // 
            // typeBox
            // 
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Location = new System.Drawing.Point(4, 33);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(241, 24);
            this.typeBox.TabIndex = 1;
            this.typeBox.Text = "unselectedType";
            this.typeBox.SelectedValueChanged += new System.EventHandler(this.typeBox_SelectedValueChanged);
            // 
            // closeBt
            // 
            this.closeBt.Location = new System.Drawing.Point(220, 4);
            this.closeBt.Name = "closeBt";
            this.closeBt.Size = new System.Drawing.Size(24, 23);
            this.closeBt.TabIndex = 2;
            this.closeBt.Text = "X";
            this.closeBt.UseVisualStyleBackColor = true;
            this.closeBt.Click += new System.EventHandler(this.closeBt_Click);
            // 
            // vPanel
            // 
            this.vPanel.AutoSize = true;
            this.vPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vPanel.Location = new System.Drawing.Point(4, 63);
            this.vPanel.Name = "vPanel";
            this.vPanel.Size = new System.Drawing.Size(0, 0);
            this.vPanel.TabIndex = 3;
            // 
            // PropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.vPanel);
            this.Controls.Add(this.closeBt);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.nameBox);
            this.Name = "PropertyEditor";
            this.Size = new System.Drawing.Size(389, 285);
            this.Load += new System.EventHandler(this.PropertyEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Panel vPanel;
        public System.Windows.Forms.Button closeBt;
    }
}
