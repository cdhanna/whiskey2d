namespace WhiskeyEditor.Controls.ValueEditor
{
    partial class IntEditor
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
            this.valueBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // valueBox
            // 
            this.valueBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueBox.Location = new System.Drawing.Point(0, 0);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(223, 22);
            this.valueBox.TabIndex = 0;
            this.valueBox.Text = "0";
            this.valueBox.TextChanged += new System.EventHandler(this.valueBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.valueBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 24);
            this.panel1.TabIndex = 1;
            // 
            // IntEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "IntEditor";
            this.Size = new System.Drawing.Size(223, 24);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Panel panel1;
    }
}
