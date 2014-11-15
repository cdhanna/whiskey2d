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
            baseTable = new System.Windows.Forms.TableLayoutPanel();
            playTestBtn = new System.Windows.Forms.Button();
            baseSplitPanel = new System.Windows.Forms.TableLayoutPanel();
            editorTabs = new System.Windows.Forms.TabControl();
            gameSpaceTab = new System.Windows.Forms.TabPage();
            gameSpace = new WhiskeyEditor.MonoHelp.WhiskeyControl();
            instancePropGrid = new WhiskeyEditor.WhiskeyPropertyGrid();
            scriptSpaceTab = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            codeControl = new ICSharpCode.TextEditor.TextEditorControl();
            panel1 = new System.Windows.Forms.Panel();
            saveScriptBtn = new System.Windows.Forms.Button();
            scriptNameBox = new System.Windows.Forms.TextBox();
            newScriptBtn = new System.Windows.Forms.Button();
            detailTabs = new System.Windows.Forms.TabControl();
            scriptLibTab = new System.Windows.Forms.TabPage();
            typeBrowserTab = new System.Windows.Forms.TabPage();
            gameObjectTypeBrowser1 = new WhiskeyEditor.Controls.TypeEditor.GameObjectTypeBrowser();
            typeEditor = new WhiskeyEditor.Controls.TypeEditor.TypeEditor();
            instEditorTab = new System.Windows.Forms.TabPage();
            tabControl1 = new System.Windows.Forms.TabControl();
            propTab = new System.Windows.Forms.TabPage();
            scriptTab = new System.Windows.Forms.TabPage();
            scriptCollection1 = new WhiskeyEditor.Controls.ScriptCollection();
            typeEditorTab = new System.Windows.Forms.TabPage();
            typeTabPanel = new System.Windows.Forms.TableLayoutPanel();
            typeEditorButtonPanel1 = new WhiskeyEditor.Controls.TypeEditor.TypeEditorButtonPanel();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newFileDialog = new System.Windows.Forms.SaveFileDialog();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            baseTable.SuspendLayout();
            baseSplitPanel.SuspendLayout();
            editorTabs.SuspendLayout();
            gameSpaceTab.SuspendLayout();
            scriptSpaceTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            detailTabs.SuspendLayout();
            typeBrowserTab.SuspendLayout();
            instEditorTab.SuspendLayout();
            tabControl1.SuspendLayout();
            propTab.SuspendLayout();
            scriptTab.SuspendLayout();
            typeEditorTab.SuspendLayout();
            typeTabPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // baseTable
            // 
            baseTable.ColumnCount = 1;
            baseTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            baseTable.Controls.Add(playTestBtn, 0, 3);
            baseTable.Controls.Add(baseSplitPanel, 0, 2);
            baseTable.Controls.Add(menuStrip1, 0, 0);
            baseTable.Dock = System.Windows.Forms.DockStyle.Fill;
            baseTable.Location = new System.Drawing.Point(0, 0);
            baseTable.Margin = new System.Windows.Forms.Padding(0);
            baseTable.Name = "baseTable";
            baseTable.RowCount = 4;
            baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 451F));
            baseTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            baseTable.Size = new System.Drawing.Size(826, 600);
            baseTable.TabIndex = 0;
            // 
            // playTestBtn
            // 
            playTestBtn.Location = new System.Drawing.Point(2, 506);
            playTestBtn.Margin = new System.Windows.Forms.Padding(2);
            playTestBtn.Name = "playTestBtn";
            playTestBtn.Size = new System.Drawing.Size(56, 19);
            playTestBtn.TabIndex = 1;
            playTestBtn.Text = "playTest";
            playTestBtn.UseVisualStyleBackColor = true;
            playTestBtn.Click += new System.EventHandler(playTestBtn_Click);
            // 
            // baseSplitPanel
            // 
            baseSplitPanel.ColumnCount = 2;
            baseSplitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            baseSplitPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 598F));
            baseSplitPanel.Controls.Add(editorTabs, 1, 0);
            baseSplitPanel.Controls.Add(detailTabs, 0, 0);
            baseSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            baseSplitPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            baseSplitPanel.Location = new System.Drawing.Point(0, 53);
            baseSplitPanel.Margin = new System.Windows.Forms.Padding(0);
            baseSplitPanel.Name = "baseSplitPanel";
            baseSplitPanel.RowCount = 1;
            baseSplitPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            baseSplitPanel.Size = new System.Drawing.Size(826, 451);
            baseSplitPanel.TabIndex = 0;
            // 
            // editorTabs
            // 
            editorTabs.Controls.Add(gameSpaceTab);
            editorTabs.Controls.Add(scriptSpaceTab);
            editorTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            editorTabs.Location = new System.Drawing.Point(229, 1);
            editorTabs.Margin = new System.Windows.Forms.Padding(1);
            editorTabs.Name = "editorTabs";
            editorTabs.SelectedIndex = 0;
            editorTabs.Size = new System.Drawing.Size(596, 449);
            editorTabs.TabIndex = 0;
            // 
            // gameSpaceTab
            // 
            gameSpaceTab.Controls.Add(gameSpace);
            gameSpaceTab.Location = new System.Drawing.Point(4, 22);
            gameSpaceTab.Margin = new System.Windows.Forms.Padding(0);
            gameSpaceTab.Name = "gameSpaceTab";
            gameSpaceTab.Size = new System.Drawing.Size(588, 423);
            gameSpaceTab.TabIndex = 0;
            gameSpaceTab.Text = "Game";
            gameSpaceTab.UseVisualStyleBackColor = true;
            // 
            // gameSpace
            // 
            gameSpace.AllowDrop = true;
            gameSpace.AutoSize = true;
            gameSpace.BackColor = System.Drawing.Color.Black;
            gameSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            gameSpace.GobGrid = instancePropGrid;
            gameSpace.GobScriptCollection = scriptCollection1;
            gameSpace.Location = new System.Drawing.Point(0, 0);
            gameSpace.Name = "gameSpace";
            gameSpace.Size = new System.Drawing.Size(588, 423);
            gameSpace.TabIndex = 0;
            gameSpace.VSync = false;
            gameSpace.DragDrop += new System.Windows.Forms.DragEventHandler(whiskeyControl_DragDrop);
            gameSpace.DragEnter += new System.Windows.Forms.DragEventHandler(whiskeyControl_DragEnter);
            // 
            // instancePropGrid
            // 
            instancePropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            instancePropGrid.Location = new System.Drawing.Point(3, 3);
            instancePropGrid.Margin = new System.Windows.Forms.Padding(2);
            instancePropGrid.Name = "instancePropGrid";
            instancePropGrid.Size = new System.Drawing.Size(204, 391);
            instancePropGrid.TabIndex = 0;
            instancePropGrid.ToolbarVisible = false;
            // 
            // scriptSpaceTab
            // 
            scriptSpaceTab.Controls.Add(tableLayoutPanel1);
            scriptSpaceTab.Location = new System.Drawing.Point(4, 22);
            scriptSpaceTab.Margin = new System.Windows.Forms.Padding(0);
            scriptSpaceTab.Name = "scriptSpaceTab";
            scriptSpaceTab.Size = new System.Drawing.Size(588, 423);
            scriptSpaceTab.TabIndex = 1;
            scriptSpaceTab.Text = "Code";
            scriptSpaceTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(codeControl, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.16548F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.83452F));
            tableLayoutPanel1.Size = new System.Drawing.Size(588, 423);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // codeControl
            // 
            codeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            codeControl.IsReadOnly = false;
            codeControl.Location = new System.Drawing.Point(2, 44);
            codeControl.Margin = new System.Windows.Forms.Padding(2);
            codeControl.Name = "codeControl";
            codeControl.ShowTabs = true;
            codeControl.Size = new System.Drawing.Size(584, 377);
            codeControl.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(saveScriptBtn);
            panel1.Controls.Add(scriptNameBox);
            panel1.Controls.Add(newScriptBtn);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(582, 36);
            panel1.TabIndex = 1;
            // 
            // saveScriptBtn
            // 
            saveScriptBtn.Location = new System.Drawing.Point(503, 7);
            saveScriptBtn.Name = "saveScriptBtn";
            saveScriptBtn.Size = new System.Drawing.Size(75, 23);
            saveScriptBtn.TabIndex = 2;
            saveScriptBtn.Text = "Save";
            saveScriptBtn.UseVisualStyleBackColor = true;
            saveScriptBtn.Click += new System.EventHandler(saveScriptBtn_Click);
            // 
            // scriptNameBox
            // 
            scriptNameBox.Location = new System.Drawing.Point(85, 7);
            scriptNameBox.Name = "scriptNameBox";
            scriptNameBox.Size = new System.Drawing.Size(132, 20);
            scriptNameBox.TabIndex = 1;
            scriptNameBox.Text = "Unnamed";
            // 
            // newScriptBtn
            // 
            newScriptBtn.Location = new System.Drawing.Point(4, 4);
            newScriptBtn.Name = "newScriptBtn";
            newScriptBtn.Size = new System.Drawing.Size(75, 23);
            newScriptBtn.TabIndex = 0;
            newScriptBtn.Text = "New";
            newScriptBtn.UseVisualStyleBackColor = true;
            newScriptBtn.Click += new System.EventHandler(newScriptBtn_Click);
            // 
            // detailTabs
            // 
            detailTabs.Controls.Add(scriptLibTab);
            detailTabs.Controls.Add(typeBrowserTab);
            detailTabs.Controls.Add(instEditorTab);
            detailTabs.Controls.Add(typeEditorTab);
            detailTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            detailTabs.Location = new System.Drawing.Point(1, 1);
            detailTabs.Margin = new System.Windows.Forms.Padding(1);
            detailTabs.Multiline = true;
            detailTabs.Name = "detailTabs";
            detailTabs.SelectedIndex = 0;
            detailTabs.Size = new System.Drawing.Size(226, 449);
            detailTabs.TabIndex = 1;
            // 
            // scriptLibTab
            // 
            scriptLibTab.Location = new System.Drawing.Point(4, 22);
            scriptLibTab.Margin = new System.Windows.Forms.Padding(2);
            scriptLibTab.Name = "scriptLibTab";
            scriptLibTab.Padding = new System.Windows.Forms.Padding(2);
            scriptLibTab.Size = new System.Drawing.Size(218, 423);
            scriptLibTab.TabIndex = 0;
            scriptLibTab.Text = "Scripts";
            scriptLibTab.UseVisualStyleBackColor = true;

            //lib panel
            scriptLibrary = new ScriptLibrary();
            scriptLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            scriptLibTab.Controls.Add(scriptLibrary);


            // 
            // typeBrowserTab
            // 
            typeBrowserTab.Controls.Add(gameObjectTypeBrowser1);
            typeBrowserTab.Location = new System.Drawing.Point(4, 22);
            typeBrowserTab.Margin = new System.Windows.Forms.Padding(2);
            typeBrowserTab.Name = "typeBrowserTab";
            typeBrowserTab.Padding = new System.Windows.Forms.Padding(2);
            typeBrowserTab.Size = new System.Drawing.Size(218, 423);
            typeBrowserTab.TabIndex = 1;
            typeBrowserTab.Text = "Browser";
            typeBrowserTab.UseVisualStyleBackColor = true;
            // 
            // gameObjectTypeBrowser1
            // 
            gameObjectTypeBrowser1.AutoScroll = true;
            gameObjectTypeBrowser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            gameObjectTypeBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            gameObjectTypeBrowser1.Location = new System.Drawing.Point(2, 2);
            gameObjectTypeBrowser1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            gameObjectTypeBrowser1.Name = "gameObjectTypeBrowser1";
            gameObjectTypeBrowser1.Size = new System.Drawing.Size(214, 419);
            gameObjectTypeBrowser1.TabIndex = 1;
            gameObjectTypeBrowser1.TypeEditor = typeEditor;
            // 
            // typeEditor
            // 
            typeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            typeEditor.Enabled = false;
            typeEditor.Location = new System.Drawing.Point(2, 2);
            typeEditor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            typeEditor.Name = "typeEditor";
            typeEditor.Size = new System.Drawing.Size(214, 369);
            typeEditor.TabIndex = 1;
            // 
            // instEditorTab
            // 
            instEditorTab.Controls.Add(tabControl1);
            instEditorTab.Location = new System.Drawing.Point(4, 22);
            instEditorTab.Margin = new System.Windows.Forms.Padding(2);
            instEditorTab.Name = "instEditorTab";
            instEditorTab.Size = new System.Drawing.Size(218, 423);
            instEditorTab.TabIndex = 2;
            instEditorTab.Text = "Instance";
            instEditorTab.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(propTab);
            tabControl1.Controls.Add(scriptTab);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(218, 423);
            tabControl1.TabIndex = 1;
            // 
            // propTab
            // 
            propTab.Controls.Add(instancePropGrid);
            propTab.Location = new System.Drawing.Point(4, 22);
            propTab.Name = "propTab";
            propTab.Padding = new System.Windows.Forms.Padding(3);
            propTab.Size = new System.Drawing.Size(210, 397);
            propTab.TabIndex = 0;
            propTab.Text = "Properties";
            propTab.UseVisualStyleBackColor = true;
            // 
            // scriptTab
            // 
            scriptTab.Controls.Add(scriptCollection1);
            scriptTab.Location = new System.Drawing.Point(4, 22);
            scriptTab.Name = "scriptTab";
            scriptTab.Padding = new System.Windows.Forms.Padding(3);
            scriptTab.Size = new System.Drawing.Size(210, 397);
            scriptTab.TabIndex = 1;
            scriptTab.Text = "Scripts";
            scriptTab.UseVisualStyleBackColor = true;
            // 
            // scriptCollection1
            // 
            scriptCollection1.Dock = System.Windows.Forms.DockStyle.Fill;
            scriptCollection1.Location = new System.Drawing.Point(3, 3);
            scriptCollection1.Name = "scriptCollection1";
            scriptCollection1.SelectedObject = null;
            scriptCollection1.Size = new System.Drawing.Size(204, 391);
            scriptCollection1.TabIndex = 1;
            // 
            // typeEditorTab
            // 
            typeEditorTab.Controls.Add(typeTabPanel);
            typeEditorTab.Location = new System.Drawing.Point(4, 22);
            typeEditorTab.Margin = new System.Windows.Forms.Padding(2);
            typeEditorTab.Name = "typeEditorTab";
            typeEditorTab.Size = new System.Drawing.Size(218, 423);
            typeEditorTab.TabIndex = 3;
            typeEditorTab.Text = "Type";
            typeEditorTab.UseVisualStyleBackColor = true;
            // 
            // typeTabPanel
            // 
            typeTabPanel.ColumnCount = 1;
            typeTabPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            typeTabPanel.Controls.Add(typeEditorButtonPanel1, 0, 1);
            typeTabPanel.Controls.Add(typeEditor, 0, 0);
            typeTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            typeTabPanel.Location = new System.Drawing.Point(0, 0);
            typeTabPanel.Margin = new System.Windows.Forms.Padding(2);
            typeTabPanel.Name = "typeTabPanel";
            typeTabPanel.RowCount = 2;
            typeTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.32565F));
            typeTabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.67435F));
            typeTabPanel.Size = new System.Drawing.Size(218, 423);
            typeTabPanel.TabIndex = 0;
            // 
            // typeEditorButtonPanel1
            // 
            typeEditorButtonPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            typeEditorButtonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            typeEditorButtonPanel1.Location = new System.Drawing.Point(0, 373);
            typeEditorButtonPanel1.Margin = new System.Windows.Forms.Padding(0);
            typeEditorButtonPanel1.Name = "typeEditorButtonPanel1";
            typeEditorButtonPanel1.Size = new System.Drawing.Size(218, 50);
            typeEditorButtonPanel1.TabIndex = 0;
            typeEditorButtonPanel1.TypeEditor = typeEditor;
            typeEditorButtonPanel1.Load += new System.EventHandler(typeEditorButtonPanel1_Load);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(826, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            newToolStripMenuItem,
            openToolStripMenuItem});
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += new System.EventHandler(newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += new System.EventHandler(openToolStripMenuItem_Click);
            // 
            // newFileDialog
            // 
            newFileDialog.CheckPathExists = false;
            newFileDialog.Title = "New Project";
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            openFileDialog.Title = "Open Project";
            // 
            // Editor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(826, 600);
            Controls.Add(baseTable);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(2);
            Name = "Editor";
            Text = "Whiskey2D Editor";
            baseTable.ResumeLayout(false);
            baseTable.PerformLayout();
            baseSplitPanel.ResumeLayout(false);
            editorTabs.ResumeLayout(false);
            gameSpaceTab.ResumeLayout(false);
            gameSpaceTab.PerformLayout();
            scriptSpaceTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            detailTabs.ResumeLayout(false);
            typeBrowserTab.ResumeLayout(false);
            instEditorTab.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            propTab.ResumeLayout(false);
            scriptTab.ResumeLayout(false);
            typeEditorTab.ResumeLayout(false);
            typeTabPanel.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);




        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel baseTable;
        private System.Windows.Forms.TableLayoutPanel baseSplitPanel;
        public static System.Windows.Forms.TabControl editorTabs;
        public static System.Windows.Forms.TabPage gameSpaceTab;
        public static System.Windows.Forms.TabPage scriptSpaceTab;
        public static System.Windows.Forms.TabControl detailTabs;
        public static System.Windows.Forms.TabPage scriptLibTab;
        public static System.Windows.Forms.TabPage typeBrowserTab;
        public static System.Windows.Forms.TabPage instEditorTab;
        public static System.Windows.Forms.TabPage typeEditorTab;
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
        public System.Windows.Forms.TabPage scriptTab;
        public ScriptCollection scriptCollection1;
        private ScriptLibrary scriptLibrary;
    }
}