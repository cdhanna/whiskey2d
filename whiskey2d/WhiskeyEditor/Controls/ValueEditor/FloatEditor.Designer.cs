namespace WhiskeyEditor.Controls.ValueEditor
{
    partial class FloatEditor
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
            this.SuspendLayout();
            // 
            // valueBox
            // 
            this.valueBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueBox.Location = new System.Drawing.Point(0, 0);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(196, 22);
            this.valueBox.TabIndex = 1;
            this.valueBox.Text = "0";
            // 
            // FloatEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.valueBox);
            this.Name = "FloatEditor";
            this.Size = new System.Drawing.Size(196, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TextBox valueBox;

    }
}
