namespace WhiskeyEditor.UI.Properties
{
    partial class TypePickerControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.primsTypeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.objsTypeBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Primitives";
            // 
            // primsTypeBox
            // 
            this.primsTypeBox.FormattingEnabled = true;
            this.primsTypeBox.Location = new System.Drawing.Point(60, 6);
            this.primsTypeBox.Name = "primsTypeBox";
            this.primsTypeBox.Size = new System.Drawing.Size(133, 21);
            this.primsTypeBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Objects";
            // 
            // ObjectsTypeBox
            // 
            this.objsTypeBox.FormattingEnabled = true;
            this.objsTypeBox.Location = new System.Drawing.Point(60, 35);
            this.objsTypeBox.Name = "ObjectsTypeBox";
            this.objsTypeBox.Size = new System.Drawing.Size(133, 21);
            this.objsTypeBox.TabIndex = 3;
            // 
            // TypePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.objsTypeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.primsTypeBox);
            this.Controls.Add(this.label1);
            this.Name = "TypePickerControl";
            this.Size = new System.Drawing.Size(202, 68);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox primsTypeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox objsTypeBox;
    }
}
