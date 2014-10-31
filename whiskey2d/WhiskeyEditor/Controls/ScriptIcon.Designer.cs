namespace WhiskeyEditor.Controls
{
    partial class ScriptIcon
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
            this.scriptNameLabel = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scriptNameLabel
            // 
            this.scriptNameLabel.AutoSize = true;
            this.scriptNameLabel.Location = new System.Drawing.Point(3, 15);
            this.scriptNameLabel.Name = "scriptNameLabel";
            this.scriptNameLabel.Size = new System.Drawing.Size(72, 15);
            this.scriptNameLabel.TabIndex = 0;
            this.scriptNameLabel.Text = "ScriptName";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(77, 10);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(32, 23);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = true;
            // 
            // ScriptIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.scriptNameLabel);
            this.Name = "ScriptIcon";
            this.Size = new System.Drawing.Size(112, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scriptNameLabel;
        public System.Windows.Forms.Button closeBtn;
    }
}
