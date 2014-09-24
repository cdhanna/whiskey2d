namespace WhiskeyEditor
{
    partial class WhiskeyForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryTree = new System.Windows.Forms.TreeView();
            this.openProjectDialog = new System.Windows.Forms.OpenFileDialog();
            this.newProjectDialog = new System.Windows.Forms.SaveFileDialog();
            this.compileButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.addGameObjectButton = new System.Windows.Forms.Button();
            this.addScriptButton = new System.Windows.Forms.Button();
            this.spinningTriangleControl1 = new WinFormsGraphicsDevice.SpinningTriangleControl();
            this.menuBar.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(608, 28);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "menuBar";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // directoryTree
            // 
            this.directoryTree.Location = new System.Drawing.Point(3, 2);
            this.directoryTree.Name = "directoryTree";
            this.directoryTree.Size = new System.Drawing.Size(223, 361);
            this.directoryTree.TabIndex = 1;
            // 
            // openProjectDialog
            // 
            this.openProjectDialog.FileName = "openProjectDialog";
            // 
            // newProjectDialog
            // 
            this.newProjectDialog.CheckPathExists = false;
            this.newProjectDialog.DefaultExt = "w2d";
            this.newProjectDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.newProjectDialog_FileOk);
            // 
            // compileButton
            // 
            this.compileButton.Location = new System.Drawing.Point(32, 427);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(75, 23);
            this.compileButton.TabIndex = 3;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(113, 427);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.spinningTriangleControl1);
            this.mainPanel.Controls.Add(this.addGameObjectButton);
            this.mainPanel.Controls.Add(this.addScriptButton);
            this.mainPanel.Controls.Add(this.runButton);
            this.mainPanel.Controls.Add(this.compileButton);
            this.mainPanel.Controls.Add(this.directoryTree);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 28);
            this.mainPanel.MinimumSize = new System.Drawing.Size(100, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(608, 459);
            this.mainPanel.TabIndex = 5;
            // 
            // addGameObjectButton
            // 
            this.addGameObjectButton.Location = new System.Drawing.Point(3, 398);
            this.addGameObjectButton.Name = "addGameObjectButton";
            this.addGameObjectButton.Size = new System.Drawing.Size(223, 23);
            this.addGameObjectButton.TabIndex = 7;
            this.addGameObjectButton.Text = "New Object";
            this.addGameObjectButton.UseVisualStyleBackColor = true;
            this.addGameObjectButton.Click += new System.EventHandler(this.addGameObjectButton_Click);
            // 
            // addScriptButton
            // 
            this.addScriptButton.Location = new System.Drawing.Point(3, 369);
            this.addScriptButton.Name = "addScriptButton";
            this.addScriptButton.Size = new System.Drawing.Size(223, 23);
            this.addScriptButton.TabIndex = 6;
            this.addScriptButton.Text = "New Script";
            this.addScriptButton.UseVisualStyleBackColor = true;
            this.addScriptButton.Click += new System.EventHandler(this.addScriptButton_Click);
            // 
            // spinningTriangleControl1
            // 
            this.spinningTriangleControl1.BackColor = System.Drawing.Color.Black;
            this.spinningTriangleControl1.Location = new System.Drawing.Point(233, 4);
            this.spinningTriangleControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spinningTriangleControl1.Name = "spinningTriangleControl1";
            this.spinningTriangleControl1.Size = new System.Drawing.Size(361, 359);
            this.spinningTriangleControl1.TabIndex = 8;
            this.spinningTriangleControl1.VSync = false;
            // 
            // WhiskeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 487);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuBar);
            this.MainMenuStrip = this.menuBar;
            this.Name = "WhiskeyForm";
            this.Text = "Whiskey2D Editor";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TreeView directoryTree;
        private System.Windows.Forms.OpenFileDialog openProjectDialog;
        private System.Windows.Forms.SaveFileDialog newProjectDialog;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button addScriptButton;
        private System.Windows.Forms.Button addGameObjectButton;
        private WinFormsGraphicsDevice.SpinningTriangleControl spinningTriangleControl1;
    }
}

