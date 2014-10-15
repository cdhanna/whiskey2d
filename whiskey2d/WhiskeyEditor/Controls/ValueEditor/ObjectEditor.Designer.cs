namespace WhiskeyEditor.Controls.ValueEditor
{
    partial class ObjectEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.whiskeyPropertyGrid1 = new WhiskeyEditor.WhiskeyPropertyGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.whiskeyPropertyGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 80);
            this.panel1.TabIndex = 1;
            // 
            // whiskeyPropertyGrid1
            // 
            this.whiskeyPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiskeyPropertyGrid1.HelpVisible = false;
            this.whiskeyPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.whiskeyPropertyGrid1.Name = "whiskeyPropertyGrid1";
            this.whiskeyPropertyGrid1.Size = new System.Drawing.Size(261, 80);
            this.whiskeyPropertyGrid1.TabIndex = 0;
            this.whiskeyPropertyGrid1.ToolbarVisible = false;
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "ObjectEditor";
            this.Size = new System.Drawing.Size(261, 80);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WhiskeyPropertyGrid whiskeyPropertyGrid1;
        private System.Windows.Forms.Panel panel1;
    }
}
