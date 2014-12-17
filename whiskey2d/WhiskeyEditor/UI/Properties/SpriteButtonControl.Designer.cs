namespace WhiskeyEditor.UI.Properties
{
    partial class SpriteButtonControl
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
            this.pixelButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.artBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pixelButton
            // 
            this.pixelButton.Location = new System.Drawing.Point(3, 3);
            this.pixelButton.Name = "pixelButton";
            this.pixelButton.Size = new System.Drawing.Size(75, 23);
            this.pixelButton.TabIndex = 0;
            this.pixelButton.Text = "Pixel";
            this.pixelButton.UseVisualStyleBackColor = true;
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(84, 3);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(92, 23);
            this.fileButton.TabIndex = 1;
            this.fileButton.Text = "Load Form File";
            this.fileButton.UseVisualStyleBackColor = true;
            // 
            // artBox
            // 
            this.artBox.FormattingEnabled = true;
            this.artBox.Location = new System.Drawing.Point(4, 33);
            this.artBox.Name = "artBox";
            this.artBox.Size = new System.Drawing.Size(172, 21);
            this.artBox.TabIndex = 2;
            // 
            // SpriteButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.artBox);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.pixelButton);
            this.Name = "SpriteButtonControl";
            this.Size = new System.Drawing.Size(179, 61);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pixelButton;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.ComboBox artBox;
    }
}
