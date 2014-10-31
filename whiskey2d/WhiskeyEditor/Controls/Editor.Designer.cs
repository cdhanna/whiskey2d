namespace WhiskeyEditor.Controls
{
    partial class Editor
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
            this.baseTable = new System.Windows.Forms.TableLayoutPanel();
            this.playTestBtn = new System.Windows.Forms.Button();
            this.baseSplitPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editorTabs = new System.Windows.Forms.TabControl();
            this.gameSpaceTab = new System.Windows.Forms.TabPage();
            this.gameSpace = new WhiskeyEditor.MonoHelp.WhiskeyControl();
            this.instancePropGrid = new WhiskeyEditor.WhiskeyPropertyGrid();
            this.scriptSpaceTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.codeControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveScriptBtn = new System.Windows.Forms.Button();
            this.scriptNameBox = new System.Windows.Forms.TextBox();
            this.newScriptBtn = new System.Windows.Forms.Button();
            this.detailTabs = new System.Windows.Forms.TabControl();
            this.scriptLibTab = new System.Windows.Forms.TabPage();
            this.typeBrowserTab = new System.Windows.Forms.TabPage();
            this.gameObjectTypeBrowser1 = new WhiskeyEditor.Controls.TypeEditor.GameObjectTypeBrowser();
            this.typeEditor = new WhiskeyEditor.Controls.TypeEditor.TypeEditor();
            this.instEditorTab = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.propTab = new System.Windows.Forms.TabPage();
            this.scriptTab = new System.Windows.Forms.TabPage();
            this.scriptCollection1 = new WhiskeyEditor.Controls.ScriptCollection();
            this.typeEditorTab = new System.Windows.Forms.TabPage();
            this.typeTabPanel = new System.Windows.Forms.TableLayoutPanel();
            this.typeEditorButtonPanel1 = new WhiskeyEditor.Controls.TypeEditor.TypeEditorButtonPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.baseTable.SuspendLayout();
            this.baseSplitPanel.SuspendLayout();
            this.editorTabs.SuspendLayout();
            this.gameSpaceTab.SuspendLayout();
            this.scriptSpaceTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.detailTabs.SuspendLayout();
            this.typeBrowserTab.SuspendLayout();
            this.instEditorTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.propTab.SuspendLayout();
            this.scriptTab.SuspendLayout();
            this.typeEditorTab.SuspendLayout();
            this.typeTabPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseTable
            // 
            this.baseTable.ColumnCount = 1;
            this.baseTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseTable.Controls.Add(this.playTestBtn, 0, 3);
            this.baseTable.Controls.Add(this.baseSplitPanel, 0, 2);
            this.baseTable.Controls.Add(this.menuStrip1, 0, 0);
            this.baseTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseTable.Location = new System.Drawing.Point(0, 0);
            this.baseTable.Margin = new System.Windows.Forms.Padding(0);
            this.baseTable.Name = "baseTable";
            this.baseTable.RowCount = 4;
            this.baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 451F));
            this.baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.baseTable.Size = new System.Drawing.Size(826, 600);
            this.baseTable.TabIndex = 0;
            // 
            // playTestBtn
            // 
            this.playTestBtn.Location = new System.Drawing.Point(2, 506);
            this.playTestBtn.Margin = new System.Windows.Forms.Padding(2);
            this.playTestBtn.Name = "playTestBtn";
            this.playTestBtn.Size = new System.Drawing.Size(56, 19);
            this.playTestBtn.TabIndex = 1;
            this.playTestBtn.Text = "playTest";
            this.playTestBtn.UseVisualStyleBackColor = true;
            this.playTestBtn.Click += new System.EventHandler(this.playTestBtn_Click);
            // 
            // baseSplitPanel
            // 
            this.baseSplitPanel.ColumnCount = 2;
            this.baseSplitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseSplitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 598F));
            this.baseSplitPanel.Controls.Add(this.editorTabs, 1, 0);
            this.baseSplitPanel.Controls.Add(this.detailTabs, 0, 0);
            this.baseSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseSplitPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.baseSplitPanel.Location = new System.Drawing.Point(0, 53);
            this.baseSplitPanel.Margin = new System.Windows.Forms.Padding(0);
            this.baseSplitPanel.Name = "baseSplitPanel";
            this.baseSplitPanel.RowCount = 1;
            this.baseSplitPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseSplitPanel.Size = new System.Drawing.Size(826, 451);
            this.baseSplitPanel.TabIndex = 0;
            // 
            // editorTabs
            // 
            this.editorTabs.Controls.Add(this.gameSpaceTab);
            this.editorTabs.Controls.Add(this.scriptSpaceTab);
            this.editorTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTabs.Location = new System.Drawing.Point(229, 1);
            this.editorTabs.Margin = new System.Windows.Forms.Padding(1);
            this.editorTabs.Name = "editorTabs";
            this.editorTabs.SelectedIndex = 0;
            this.editorTabs.Size = new System.Drawing.Size(596, 449);
            this.editorTabs.TabIndex = 0;
            // 
            // gameSpaceTab
            // 
            this.gameSpaceTab.Controls.Add(this.gameSpace);
            this.gameSpaceTab.Location = new System.Drawing.Point(4, 22);
            this.gameSpaceTab.Margin = new System.Windows.Forms.Padding(0);
            this.gameSpaceTab.Name = "gameSpaceTab";
            this.gameSpaceTab.Size = new System.Drawing.Size(588, 423);
            this.gameSpaceTab.TabIndex = 0;
            this.gameSpaceTab.Text = "Game";
            this.gameSpaceTab.UseVisualStyleBackColor = true;
            // 
            // gameSpace
            // 
            this.gameSpace.AllowDrop = true;
            this.gameSpace.AutoSize = true;
            this.gameSpace.BackColor = System.Drawing.Color.Black;
            this.gameSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameSpace.GobGrid = this.instancePropGrid;
            this.gameSpace.GobScriptCollection = this.scriptCollection1;
            this.gameSpace.Location = new System.Drawing.Point(0, 0);
            this.gameSpace.Name = "gameSpace";
            this.gameSpace.Size = new System.Drawing.Size(588, 423);
            this.gameSpace.TabIndex = 0;
            this.gameSpace.VSync = false;
            this.gameSpace.DragDrop += new System.Windows.Forms.DragEventHandler(this.whiskeyControl_DragDrop);
            this.gameSpace.DragEnter += new System.Windows.Forms.DragEventHandler(this.whiskeyControl_DragEnter);
            // 
            // instancePropGrid
            // 
            this.instancePropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instancePropGrid.Location = new System.Drawing.Point(3, 3);
            this.instancePropGrid.Margin = new System.Windows.Forms.Padding(2);
            this.instancePropGrid.Name = "instancePropGrid";
            this.instancePropGrid.Size = new System.Drawing.Size(204, 391);
            this.instancePropGrid.TabIndex = 0;
            this.instancePropGrid.ToolbarVisible = false;
            // 
            // scriptSpaceTab
            // 
            this.scriptSpaceTab.Controls.Add(this.tableLayoutPanel1);
            this.scriptSpaceTab.Location = new System.Drawing.Point(4, 22);
            this.scriptSpaceTab.Margin = new System.Windows.Forms.Padding(0);
            this.scriptSpaceTab.Name = "scriptSpaceTab";
            this.scriptSpaceTab.Size = new System.Drawing.Size(588, 423);
            this.scriptSpaceTab.TabIndex = 1;
            this.scriptSpaceTab.Text = "Code";
            this.scriptSpaceTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.codeControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.16548F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.83452F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(588, 423);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // codeControl
            // 
            this.codeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeControl.IsReadOnly = false;
            this.codeControl.Location = new System.Drawing.Point(2, 44);
            this.codeControl.Margin = new System.Windows.Forms.Padding(2);
            this.codeControl.Name = "codeControl";
            this.codeControl.ShowTabs = true;
            this.codeControl.Size = new System.Drawing.Size(584, 377);
            this.codeControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveScriptBtn);
            this.panel1.Controls.Add(this.scriptNameBox);
            this.panel1.Controls.Add(this.newScriptBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 36);
            this.panel1.TabIndex = 1;
            // 
            // saveScriptBtn
            // 
            this.saveScriptBtn.Location = new System.Drawing.Point(503, 7);
            this.saveScriptBtn.Name = "saveScriptBtn";
            this.saveScriptBtn.Size = new System.Drawing.Size(75, 23);
            this.saveScriptBtn.TabIndex = 2;
            this.saveScriptBtn.Text = "Save";
            this.saveScriptBtn.UseVisualStyleBackColor = true;
            this.saveScriptBtn.Click += new System.EventHandler(this.saveScriptBtn_Click);
            // 
            // scriptNameBox
            // 
            this.scriptNameBox.Location = new System.Drawing.Point(85, 7);
            this.scriptNameBox.Name = "scriptNameBox";
            this.scriptNameBox.Size = new System.Drawing.Size(132, 20);
            this.scriptNameBox.TabIndex = 1;
            this.scriptNameBox.Text = "Unnamed";
            // 
            // newScriptBtn
            // 
            this.newScriptBtn.Location = new System.Drawing.Point(4, 4);
            this.newScriptBtn.Name = "newScriptBtn";
            this.newScriptBtn.Size = new System.Drawing.Size(75, 23);
            this.newScriptBtn.TabIndex = 0;
            this.newScriptBtn.Text = "New";
            this.newScriptBtn.UseVisualStyleBackColor = true;
            this.newScriptBtn.Click += new System.EventHandler(this.newScriptBtn_Click);
            // 
            // detailTabs
            // 
            this.detailTabs.Controls.Add(this.scriptLibTab);
            this.detailTabs.Controls.Add(this.typeBrowserTab);
            this.detailTabs.Controls.Add(this.instEditorTab);
            this.detailTabs.Controls.Add(this.typeEditorTab);
            this.detailTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTabs.Location = new System.Drawing.Point(1, 1);
            this.detailTabs.Margin = new System.Windows.Forms.Padding(1);
            this.detailTabs.Multiline = true;
            this.detailTabs.Name = "detailTabs";
            this.detailTabs.SelectedIndex = 0;
            this.detailTabs.Size = new System.Drawing.Size(226, 449);
            this.detailTabs.TabIndex = 1;
            // 
            // scriptLibTab
            // 
            this.scriptLibTab.Location = new System.Drawing.Point(4, 22);
            this.scriptLibTab.Margin = new System.Windows.Forms.Padding(2);
            this.scriptLibTab.Name = "scriptLibTab";
            this.scriptLibTab.Padding = new System.Windows.Forms.Padding(2);
            this.scriptLibTab.Size = new System.Drawing.Size(218, 423);
            this.scriptLibTab.TabIndex = 0;
            this.scriptLibTab.Text = "Scripts";
            this.scriptLibTab.UseVisualStyleBackColor = true;
            // 
            // typeBrowserTab
            // 
            this.typeBrowserTab.Controls.Add(this.gameObjectTypeBrowser1);
            this.typeBrowserTab.Location = new System.Drawing.Point(4, 22);
            this.typeBrowserTab.Margin = new System.Windows.Forms.Padding(2);
            this.typeBrowserTab.Name = "typeBrowserTab";
            this.typeBrowserTab.Padding = new System.Windows.Forms.Padding(2);
            this.typeBrowserTab.Size = new System.Drawing.Size(218, 423);
            this.typeBrowserTab.TabIndex = 1;
            this.typeBrowserTab.Text = "Browser";
            this.typeBrowserTab.UseVisualStyleBackColor = true;
            // 
            // gameObjectTypeBrowser1
            // 
            this.gameObjectTypeBrowser1.AutoScroll = true;
            this.gameObjectTypeBrowser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameObjectTypeBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObjectTypeBrowser1.Location = new System.Drawing.Point(2, 2);
            this.gameObjectTypeBrowser1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gameObjectTypeBrowser1.Name = "gameObjectTypeBrowser1";
            this.gameObjectTypeBrowser1.Size = new System.Drawing.Size(214, 419);
            this.gameObjectTypeBrowser1.TabIndex = 1;
            this.gameObjectTypeBrowser1.TypeEditor = this.typeEditor;
            // 
            // typeEditor
            // 
            this.typeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeEditor.Enabled = false;
            this.typeEditor.Location = new System.Drawing.Point(2, 2);
            this.typeEditor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.typeEditor.Name = "typeEditor";
            this.typeEditor.Size = new System.Drawing.Size(214, 369);
            this.typeEditor.TabIndex = 1;
            // 
            // instEditorTab
            // 
            this.instEditorTab.Controls.Add(this.tabControl1);
            this.instEditorTab.Location = new System.Drawing.Point(4, 22);
            this.instEditorTab.Margin = new System.Windows.Forms.Padding(2);
            this.instEditorTab.Name = "instEditorTab";
            this.instEditorTab.Size = new System.Drawing.Size(218, 423);
            this.instEditorTab.TabIndex = 2;
            this.instEditorTab.Text = "Instance";
            this.instEditorTab.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.propTab);
            this.tabControl1.Controls.Add(this.scriptTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(218, 423);
            this.tabControl1.TabIndex = 1;
            // 
            // propTab
            // 
            this.propTab.Controls.Add(this.instancePropGrid);
            this.propTab.Location = new System.Drawing.Point(4, 22);
            this.propTab.Name = "propTab";
            this.propTab.Padding = new System.Windows.Forms.Padding(3);
            this.propTab.Size = new System.Drawing.Size(210, 397);
            this.propTab.TabIndex = 0;
            this.propTab.Text = "Properties";
            this.propTab.UseVisualStyleBackColor = true;
            // 
            // scriptTab
            // 
            this.scriptTab.Controls.Add(this.scriptCollection1);
            this.scriptTab.Location = new System.Drawing.Point(4, 22);
            this.scriptTab.Name = "scriptTab";
            this.scriptTab.Padding = new System.Windows.Forms.Padding(3);
            this.scriptTab.Size = new System.Drawing.Size(210, 397);
            this.scriptTab.TabIndex = 1;
            this.scriptTab.Text = "Scripts";
            this.scriptTab.UseVisualStyleBackColor = true;
            // 
            // scriptCollection1
            // 
            this.scriptCollection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptCollection1.Location = new System.Drawing.Point(3, 3);
            this.scriptCollection1.Name = "scriptCollection1";
            this.scriptCollection1.SelectedObject = null;
            this.scriptCollection1.Size = new System.Drawing.Size(204, 391);
            this.scriptCollection1.TabIndex = 1;
            // 
            // typeEditorTab
            // 
            this.typeEditorTab.Controls.Add(this.typeTabPanel);
            this.typeEditorTab.Location = new System.Drawing.Point(4, 22);
            this.typeEditorTab.Margin = new System.Windows.Forms.Padding(2);
            this.typeEditorTab.Name = "typeEditorTab";
            this.typeEditorTab.Size = new System.Drawing.Size(218, 423);
            this.typeEditorTab.TabIndex = 3;
            this.typeEditorTab.Text = "Type";
            this.typeEditorTab.UseVisualStyleBackColor = true;
            // 
            // typeTabPanel
            // 
            this.typeTabPanel.ColumnCount = 1;
            this.typeTabPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.typeTabPanel.Controls.Add(this.typeEditorButtonPanel1, 0, 1);
            this.typeTabPanel.Controls.Add(this.typeEditor, 0, 0);
            this.typeTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeTabPanel.Location = new System.Drawing.Point(0, 0);
            this.typeTabPanel.Margin = new System.Windows.Forms.Padding(2);
            this.typeTabPanel.Name = "typeTabPanel";
            this.typeTabPanel.RowCount = 2;
            this.typeTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.32565F));
            this.typeTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.67435F));
            this.typeTabPanel.Size = new System.Drawing.Size(218, 423);
            this.typeTabPanel.TabIndex = 0;
            // 
            // typeEditorButtonPanel1
            // 
            this.typeEditorButtonPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.typeEditorButtonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeEditorButtonPanel1.Location = new System.Drawing.Point(0, 373);
            this.typeEditorButtonPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.typeEditorButtonPanel1.Name = "typeEditorButtonPanel1";
            this.typeEditorButtonPanel1.Size = new System.Drawing.Size(218, 50);
            this.typeEditorButtonPanel1.TabIndex = 0;
            this.typeEditorButtonPanel1.TypeEditor = this.typeEditor;
            this.typeEditorButtonPanel1.Load += new System.EventHandler(this.typeEditorButtonPanel1_Load);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(826, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newFileDialog
            // 
            this.newFileDialog.CheckPathExists = false;
            this.newFileDialog.Title = "New Project";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Title = "Open Project";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 600);
            this.Controls.Add(this.baseTable);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Editor";
            this.Text = "Whiskey2D Editor";
            this.baseTable.ResumeLayout(false);
            this.baseTable.PerformLayout();
            this.baseSplitPanel.ResumeLayout(false);
            this.editorTabs.ResumeLayout(false);
            this.gameSpaceTab.ResumeLayout(false);
            this.gameSpaceTab.PerformLayout();
            this.scriptSpaceTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.detailTabs.ResumeLayout(false);
            this.typeBrowserTab.ResumeLayout(false);
            this.instEditorTab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.propTab.ResumeLayout(false);
            this.scriptTab.ResumeLayout(false);
            this.typeEditorTab.ResumeLayout(false);
            this.typeTabPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel baseTable;
        private System.Windows.Forms.TableLayoutPanel baseSplitPanel;
        private System.Windows.Forms.TabControl editorTabs;
        private System.Windows.Forms.TabPage gameSpaceTab;
        private System.Windows.Forms.TabPage scriptSpaceTab;
        private System.Windows.Forms.TabControl detailTabs;
        private System.Windows.Forms.TabPage scriptLibTab;
        private System.Windows.Forms.TabPage typeBrowserTab;
        private System.Windows.Forms.TabPage instEditorTab;
        private System.Windows.Forms.TabPage typeEditorTab;
        private System.Windows.Forms.TableLayoutPanel typeTabPanel;
        private TypeEditor.TypeEditorButtonPanel typeEditorButtonPanel1;
        private TypeEditor.TypeEditor typeEditor;
        private MonoHelp.WhiskeyControl gameSpace;
        private TypeEditor.GameObjectTypeBrowser gameObjectTypeBrowser1;
        private WhiskeyPropertyGrid instancePropGrid;
        private ICSharpCode.TextEditor.TextEditorControl codeControl;
        private System.Windows.Forms.Button playTestBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog newFileDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button newScriptBtn;
        private System.Windows.Forms.TextBox scriptNameBox;
        private System.Windows.Forms.Button saveScriptBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage propTab;
        private System.Windows.Forms.TabPage scriptTab;
        private ScriptCollection scriptCollection1;
    }
}