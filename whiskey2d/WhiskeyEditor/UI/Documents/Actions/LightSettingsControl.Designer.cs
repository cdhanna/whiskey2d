namespace WhiskeyEditor.UI.Documents.Actions
{
    partial class LightSettingsControl
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
            this.prevLightingBox = new System.Windows.Forms.CheckBox();
            this.prevShadowBox = new System.Windows.Forms.CheckBox();
            this.enableShadowBox = new System.Windows.Forms.CheckBox();
            this.enableLightingBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // prevLightingBox
            // 
            this.prevLightingBox.AutoSize = true;
            this.prevLightingBox.Location = new System.Drawing.Point(14, 3);
            this.prevLightingBox.Name = "prevLightingBox";
            this.prevLightingBox.Size = new System.Drawing.Size(104, 17);
            this.prevLightingBox.TabIndex = 0;
            this.prevLightingBox.Text = "Preview Lighting";
            this.prevLightingBox.UseVisualStyleBackColor = true;
            // 
            // prevShadowBox
            // 
            this.prevShadowBox.AutoSize = true;
            this.prevShadowBox.Location = new System.Drawing.Point(14, 26);
            this.prevShadowBox.Name = "prevShadowBox";
            this.prevShadowBox.Size = new System.Drawing.Size(120, 17);
            this.prevShadowBox.TabIndex = 1;
            this.prevShadowBox.Text = "Preview Shadowing";
            this.prevShadowBox.UseVisualStyleBackColor = true;
            // 
            // enableShadowBox
            // 
            this.enableShadowBox.AutoSize = true;
            this.enableShadowBox.Location = new System.Drawing.Point(14, 72);
            this.enableShadowBox.Name = "enableShadowBox";
            this.enableShadowBox.Size = new System.Drawing.Size(115, 17);
            this.enableShadowBox.TabIndex = 2;
            this.enableShadowBox.Text = "Enable Shadowing";
            this.enableShadowBox.UseVisualStyleBackColor = true;
            // 
            // enableLightingBox
            // 
            this.enableLightingBox.AutoSize = true;
            this.enableLightingBox.Location = new System.Drawing.Point(14, 49);
            this.enableLightingBox.Name = "enableLightingBox";
            this.enableLightingBox.Size = new System.Drawing.Size(99, 17);
            this.enableLightingBox.TabIndex = 3;
            this.enableLightingBox.Text = "Enable Lighting";
            this.enableLightingBox.UseVisualStyleBackColor = true;
            // 
            // LightSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enableLightingBox);
            this.Controls.Add(this.enableShadowBox);
            this.Controls.Add(this.prevShadowBox);
            this.Controls.Add(this.prevLightingBox);
            this.Name = "LightSettingsControl";
            this.Size = new System.Drawing.Size(131, 106);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox prevLightingBox;
        private System.Windows.Forms.CheckBox prevShadowBox;
        private System.Windows.Forms.CheckBox enableShadowBox;
        private System.Windows.Forms.CheckBox enableLightingBox;
    }
}
