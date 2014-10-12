namespace WhiskeyEditor
{
    partial class EditorForm
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
            this.verticalSplit = new System.Windows.Forms.TableLayoutPanel();
            this.centerSplit = new System.Windows.Forms.TableLayoutPanel();
            this.detailTabs = new System.Windows.Forms.TabControl();
            this.gobPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addGameObjectTypeBtn = new System.Windows.Forms.Button();
            this.gobTypePage = new System.Windows.Forms.TabPage();
            this.gameObjectPropertyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.instPage = new System.Windows.Forms.TabPage();
            this.propertyScrollPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gameObjectTypeName = new System.Windows.Forms.Label();
            this.gameObjectTypeNameBox = new System.Windows.Forms.TextBox();
            this.addPropertyBtn = new System.Windows.Forms.Button();
            this.gameObjectTypeBrowser = new WhiskeyEditor.GameObjectTypeBrowser();
            this.gameObjectTypeEditor = new WhiskeyEditor.GameObjectTypeEditor();
            this.whiskeyPropertyGrid1 = new WhiskeyEditor.WhiskeyPropertyGrid();
            this.whiskeyControl = new WhiskeyEditor.MonoHelp.WhiskeyControl();
            this.verticalSplit.SuspendLayout();
            this.centerSplit.SuspendLayout();
            this.detailTabs.SuspendLayout();
            this.gobPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gobTypePage.SuspendLayout();
            this.gameObjectPropertyPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.instPage.SuspendLayout();
            this.propertyScrollPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // verticalSplit
            // 
            this.verticalSplit.ColumnCount = 1;
            this.verticalSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.65537F));
            this.verticalSplit.Controls.Add(this.centerSplit, 0, 1);
            this.verticalSplit.Controls.Add(this.propertyScrollPanel, 0, 0);
            this.verticalSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSplit.Location = new System.Drawing.Point(0, 0);
            this.verticalSplit.Name = "verticalSplit";
            this.verticalSplit.RowCount = 3;
            this.verticalSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.89063F));
            this.verticalSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.10938F));
            this.verticalSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.verticalSplit.Size = new System.Drawing.Size(1381, 741);
            this.verticalSplit.TabIndex = 0;
            // 
            // centerSplit
            // 
            this.centerSplit.ColumnCount = 2;
            this.centerSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.68909F));
            this.centerSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.31091F));
            this.centerSplit.Controls.Add(this.detailTabs, 0, 0);
            this.centerSplit.Controls.Add(this.whiskeyControl, 1, 0);
            this.centerSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerSplit.Location = new System.Drawing.Point(3, 89);
            this.centerSplit.Name = "centerSplit";
            this.centerSplit.RowCount = 1;
            this.centerSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.centerSplit.Size = new System.Drawing.Size(1375, 577);
            this.centerSplit.TabIndex = 0;
            // 
            // detailTabs
            // 
            this.detailTabs.Controls.Add(this.gobPage);
            this.detailTabs.Controls.Add(this.gobTypePage);
            this.detailTabs.Controls.Add(this.instPage);
            this.detailTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTabs.Location = new System.Drawing.Point(3, 3);
            this.detailTabs.Multiline = true;
            this.detailTabs.Name = "detailTabs";
            this.detailTabs.SelectedIndex = 0;
            this.detailTabs.Size = new System.Drawing.Size(429, 571);
            this.detailTabs.TabIndex = 0;
            // 
            // gobPage
            // 
            this.gobPage.Controls.Add(this.tableLayoutPanel1);
            this.gobPage.Location = new System.Drawing.Point(4, 25);
            this.gobPage.Name = "gobPage";
            this.gobPage.Padding = new System.Windows.Forms.Padding(3);
            this.gobPage.Size = new System.Drawing.Size(421, 542);
            this.gobPage.TabIndex = 0;
            this.gobPage.Text = "GameObjects";
            this.gobPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gameObjectTypeBrowser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.02454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.97546F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(415, 536);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addGameObjectTypeBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 496);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 37);
            this.panel1.TabIndex = 0;
            // 
            // addGameObjectTypeBtn
            // 
            this.addGameObjectTypeBtn.Location = new System.Drawing.Point(3, 3);
            this.addGameObjectTypeBtn.Name = "addGameObjectTypeBtn";
            this.addGameObjectTypeBtn.Size = new System.Drawing.Size(183, 23);
            this.addGameObjectTypeBtn.TabIndex = 0;
            this.addGameObjectTypeBtn.Text = "New GameObject Type";
            this.addGameObjectTypeBtn.UseVisualStyleBackColor = true;
            this.addGameObjectTypeBtn.Click += new System.EventHandler(this.addGameObjectTypeBtn_Click);
            // 
            // gobTypePage
            // 
            this.gobTypePage.Controls.Add(this.gameObjectPropertyPanel);
            this.gobTypePage.Location = new System.Drawing.Point(4, 25);
            this.gobTypePage.Name = "gobTypePage";
            this.gobTypePage.Padding = new System.Windows.Forms.Padding(3);
            this.gobTypePage.Size = new System.Drawing.Size(421, 542);
            this.gobTypePage.TabIndex = 1;
            this.gobTypePage.Text = "GameObject Properties";
            this.gobTypePage.UseVisualStyleBackColor = true;
            // 
            // gameObjectPropertyPanel
            // 
            this.gameObjectPropertyPanel.ColumnCount = 1;
            this.gameObjectPropertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameObjectPropertyPanel.Controls.Add(this.gameObjectTypeEditor, 0, 0);
            this.gameObjectPropertyPanel.Controls.Add(this.panel3, 0, 1);
            this.gameObjectPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObjectPropertyPanel.Location = new System.Drawing.Point(3, 3);
            this.gameObjectPropertyPanel.Margin = new System.Windows.Forms.Padding(0);
            this.gameObjectPropertyPanel.Name = "gameObjectPropertyPanel";
            this.gameObjectPropertyPanel.RowCount = 2;
            this.gameObjectPropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.04478F));
            this.gameObjectPropertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.955224F));
            this.gameObjectPropertyPanel.Size = new System.Drawing.Size(415, 536);
            this.gameObjectPropertyPanel.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.acceptBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 491);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(409, 42);
            this.panel3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "New GameObject Type";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(285, 3);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(121, 23);
            this.acceptBtn.TabIndex = 1;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // instPage
            // 
            this.instPage.Controls.Add(this.whiskeyPropertyGrid1);
            this.instPage.Location = new System.Drawing.Point(4, 25);
            this.instPage.Name = "instPage";
            this.instPage.Padding = new System.Windows.Forms.Padding(3);
            this.instPage.Size = new System.Drawing.Size(421, 542);
            this.instPage.TabIndex = 2;
            this.instPage.Text = "Instance Properties";
            this.instPage.UseVisualStyleBackColor = true;
            // 
            // propertyScrollPanel
            // 
            this.propertyScrollPanel.AutoScroll = true;
            this.propertyScrollPanel.Controls.Add(this.panel2);
            this.propertyScrollPanel.Controls.Add(this.addPropertyBtn);
            this.propertyScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.propertyScrollPanel.Margin = new System.Windows.Forms.Padding(0);
            this.propertyScrollPanel.Name = "propertyScrollPanel";
            this.propertyScrollPanel.Size = new System.Drawing.Size(175, 86);
            this.propertyScrollPanel.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gameObjectTypeName);
            this.panel2.Controls.Add(this.gameObjectTypeNameBox);
            this.panel2.Location = new System.Drawing.Point(92, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 56);
            this.panel2.TabIndex = 2;
            // 
            // gameObjectTypeName
            // 
            this.gameObjectTypeName.AutoSize = true;
            this.gameObjectTypeName.Location = new System.Drawing.Point(16, 5);
            this.gameObjectTypeName.Name = "gameObjectTypeName";
            this.gameObjectTypeName.Size = new System.Drawing.Size(172, 17);
            this.gameObjectTypeName.TabIndex = 0;
            this.gameObjectTypeName.Text = "Game Object Type Name:";
            // 
            // gameObjectTypeNameBox
            // 
            this.gameObjectTypeNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameObjectTypeNameBox.Location = new System.Drawing.Point(6, 25);
            this.gameObjectTypeNameBox.Name = "gameObjectTypeNameBox";
            this.gameObjectTypeNameBox.Size = new System.Drawing.Size(288, 22);
            this.gameObjectTypeNameBox.TabIndex = 1;
            this.gameObjectTypeNameBox.Text = "UnnamedGameObject";
            this.gameObjectTypeNameBox.TextChanged += new System.EventHandler(this.gameObjectTypeNameBox_TextChanged);
            // 
            // addPropertyBtn
            // 
            this.addPropertyBtn.Location = new System.Drawing.Point(0, 13);
            this.addPropertyBtn.Name = "addPropertyBtn";
            this.addPropertyBtn.Size = new System.Drawing.Size(121, 23);
            this.addPropertyBtn.TabIndex = 0;
            this.addPropertyBtn.Text = "Add Property...";
            this.addPropertyBtn.UseVisualStyleBackColor = true;
            this.addPropertyBtn.Click += new System.EventHandler(this.addPropertyBtn_Click);
            // 
            // gameObjectTypeBrowser
            // 
            this.gameObjectTypeBrowser.AutoScroll = true;
            this.gameObjectTypeBrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameObjectTypeBrowser.Location = new System.Drawing.Point(3, 3);
            this.gameObjectTypeBrowser.Name = "gameObjectTypeBrowser";
            this.gameObjectTypeBrowser.Size = new System.Drawing.Size(159, 229);
            this.gameObjectTypeBrowser.TabIndex = 1;
            // 
            // gameObjectTypeEditor
            // 
            this.gameObjectTypeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObjectTypeEditor.Location = new System.Drawing.Point(3, 3);
            this.gameObjectTypeEditor.Name = "gameObjectTypeEditor";
            this.gameObjectTypeEditor.Size = new System.Drawing.Size(409, 482);
            this.gameObjectTypeEditor.TabIndex = 4;
            // 
            // whiskeyPropertyGrid1
            // 
            this.whiskeyPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiskeyPropertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.whiskeyPropertyGrid1.Name = "whiskeyPropertyGrid1";
            this.whiskeyPropertyGrid1.Size = new System.Drawing.Size(415, 536);
            this.whiskeyPropertyGrid1.TabIndex = 0;
            // 
            // whiskeyControl
            // 
            this.whiskeyControl.AllowDrop = true;
            this.whiskeyControl.BackColor = System.Drawing.Color.Black;
            this.whiskeyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiskeyControl.GobGrid = this.whiskeyPropertyGrid1;
            this.whiskeyControl.Location = new System.Drawing.Point(439, 4);
            this.whiskeyControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.whiskeyControl.Name = "whiskeyControl";
            this.whiskeyControl.Size = new System.Drawing.Size(932, 569);
            this.whiskeyControl.TabIndex = 1;
            this.whiskeyControl.VSync = false;
            this.whiskeyControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.whiskeyControl_DragDrop);
            this.whiskeyControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.whiskeyControl_DragEnter);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 741);
            this.Controls.Add(this.verticalSplit);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.verticalSplit.ResumeLayout(false);
            this.centerSplit.ResumeLayout(false);
            this.detailTabs.ResumeLayout(false);
            this.gobPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gobTypePage.ResumeLayout(false);
            this.gameObjectPropertyPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.instPage.ResumeLayout(false);
            this.propertyScrollPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel verticalSplit;
        private System.Windows.Forms.TableLayoutPanel centerSplit;
        private System.Windows.Forms.TabControl detailTabs;
        private System.Windows.Forms.TabPage gobPage;
        private System.Windows.Forms.TabPage gobTypePage;
        private System.Windows.Forms.TabPage instPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addGameObjectTypeBtn;
        private System.Windows.Forms.TableLayoutPanel gameObjectPropertyPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label gameObjectTypeName;
        private System.Windows.Forms.TextBox gameObjectTypeNameBox;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Button addPropertyBtn;
        private System.Windows.Forms.Panel propertyScrollPanel;
        private GameObjectTypeBrowser gameObjectTypeBrowser;
        private MonoHelp.WhiskeyControl whiskeyControl;
        private WhiskeyPropertyGrid whiskeyPropertyGrid1;
        private GameObjectTypeEditor gameObjectTypeEditor;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
    }
}