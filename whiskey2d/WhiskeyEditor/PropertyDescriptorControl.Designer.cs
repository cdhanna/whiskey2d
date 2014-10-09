namespace WhiskeyEditor
{
    partial class PropertyDescriptorControl
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(3, 3);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(230, 22);
            this.nameBox.TabIndex = 0;
            this.nameBox.Text = "unnamedProperty";
            // 
            // typeBox
            // 
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "Integer",
            "Boolean",
            "Float",
            "Vector",
            "Color",
            "String"});
            this.typeBox.Location = new System.Drawing.Point(4, 31);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(229, 24);
            this.typeBox.TabIndex = 1;
            this.typeBox.Text = "Type";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(239, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(31, 54);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // PropertyDescriptorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.nameBox);
            this.Name = "PropertyDescriptorControl";
            this.Size = new System.Drawing.Size(274, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ComboBox typeBox;
        public System.Windows.Forms.Button closeBtn;
    }
}
