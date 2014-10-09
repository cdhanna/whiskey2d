namespace WhiskeyEditor
{
    partial class GameObjectIcon
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
            this.typeLabel = new System.Windows.Forms.Label();
            this.typePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.typePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(51, 11);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(46, 17);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "label1";
            // 
            // typePictureBox
            // 
            this.typePictureBox.Location = new System.Drawing.Point(6, 31);
            this.typePictureBox.Name = "typePictureBox";
            this.typePictureBox.Size = new System.Drawing.Size(142, 111);
            this.typePictureBox.TabIndex = 1;
            this.typePictureBox.TabStop = false;
            // 
            // GameObjectIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.typePictureBox);
            this.Controls.Add(this.typeLabel);
            this.Name = "GameObjectIcon";
            this.Size = new System.Drawing.Size(151, 145);
            ((System.ComponentModel.ISupportInitialize)(this.typePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.PictureBox typePictureBox;
    }
}
