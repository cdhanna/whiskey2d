namespace WhiskeyEditor.UI.Documents
{
    partial class ProjectSettingsControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.projectNameBox = new System.Windows.Forms.TextBox();
            this.authorNameBox = new System.Windows.Forms.TextBox();
            this.startLevelBox = new System.Windows.Forms.ComboBox();
            this.isFullScreen = new System.Windows.Forms.CheckBox();
            this.closeOnExit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Author";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Level";
            // 
            // projectNameBox
            // 
            this.projectNameBox.Location = new System.Drawing.Point(5, 20);
            this.projectNameBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.projectNameBox.Name = "projectNameBox";
            this.projectNameBox.Size = new System.Drawing.Size(432, 20);
            this.projectNameBox.TabIndex = 3;
            // 
            // authorNameBox
            // 
            this.authorNameBox.Location = new System.Drawing.Point(5, 70);
            this.authorNameBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.authorNameBox.Name = "authorNameBox";
            this.authorNameBox.Size = new System.Drawing.Size(432, 20);
            this.authorNameBox.TabIndex = 4;
            // 
            // startLevelBox
            // 
            this.startLevelBox.FormattingEnabled = true;
            this.startLevelBox.Location = new System.Drawing.Point(5, 124);
            this.startLevelBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.startLevelBox.Name = "startLevelBox";
            this.startLevelBox.Size = new System.Drawing.Size(432, 21);
            this.startLevelBox.TabIndex = 5;
            // 
            // isFullScreen
            // 
            this.isFullScreen.AutoSize = true;
            this.isFullScreen.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isFullScreen.Location = new System.Drawing.Point(6, 157);
            this.isFullScreen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.isFullScreen.Name = "isFullScreen";
            this.isFullScreen.Size = new System.Drawing.Size(79, 17);
            this.isFullScreen.TabIndex = 6;
            this.isFullScreen.Text = "Full Screen";
            this.isFullScreen.UseVisualStyleBackColor = true;
            // 
            // closeOnExit
            // 
            this.closeOnExit.AutoSize = true;
            this.closeOnExit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.closeOnExit.Location = new System.Drawing.Point(98, 157);
            this.closeOnExit.Margin = new System.Windows.Forms.Padding(2);
            this.closeOnExit.Name = "closeOnExit";
            this.closeOnExit.Size = new System.Drawing.Size(108, 17);
            this.closeOnExit.TabIndex = 7;
            this.closeOnExit.Text = "Close On Escape";
            this.closeOnExit.UseVisualStyleBackColor = true;
            // 
            // ProjectSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closeOnExit);
            this.Controls.Add(this.isFullScreen);
            this.Controls.Add(this.startLevelBox);
            this.Controls.Add(this.authorNameBox);
            this.Controls.Add(this.projectNameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProjectSettingsControl";
            this.Size = new System.Drawing.Size(439, 256);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox projectNameBox;
        private System.Windows.Forms.TextBox authorNameBox;
        private System.Windows.Forms.ComboBox startLevelBox;
        private System.Windows.Forms.CheckBox isFullScreen;
        private System.Windows.Forms.CheckBox closeOnExit;
    }
}
