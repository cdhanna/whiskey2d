using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Menu
{
    class NewForm : Form
    {
        public const string NEW_TYPE = "Type";
        public const string NEW_SCRIPT = "Script";
        public const string NEW_LEVEL = "Level";
        //public const string NEW_CODE = "Code";


        private FlowLayoutPanel btnPanel;


        private Label typeLabel;
        private Label nameLabel;
        private Label detailLabel;
        
        private Button btnCancel;
        private Button btnOkay;


        private TextBox nameBox;
        private ComboBox typeBox;
        private ComboBox scriptTypeBox;


        private TableLayoutPanel mainPanel;

        public DialogResult DialogResult { get; private set; }
        public String SelectedType { get { return typeBox.SelectedItem.ToString(); } }
        public String SelectedText { get { return textCheck(nameBox.Text); } }
        public String SelectedOption
        {
            get
            {
                if (scriptTypeBox.SelectedItem != null)
                    return scriptTypeBox.SelectedItem.ToString();
                else return "";
            }
        }
        private string textCheck(string text)
        {
            return (("" + text[0]).ToUpper() + text.Substring(1));
        }

        public NewForm()
        {
            Size = new Size(250, 170);
            Text = "New...";

            this.Shown += (s, a) =>
            {
                nameBox.Text = "";
            };
            this.FormBorderStyle =  System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            initControls();
            configureControls();
            addControls();
        }


        public void setForType()
        {
            typeBox.SelectedItem = NewForm.NEW_TYPE;
            scriptTypeBox.Visible = false;
        }
        //public void setForCode()
        //{
        //    typeBox.SelectedItem = NewForm.NEW_CODE;

        //}
        public void setForScript()
        {
            typeBox.SelectedItem = NewForm.NEW_SCRIPT;
            //scriptTypeBox.Visible = (typeBox.SelectedItem.ToString().Equals(NEW_SCRIPT));
            //scriptTypeBox.Items.Clear();
            //if (scriptTypeBox.Visible)
            //{
            //    FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList().ForEach((f) =>
            //    {
            //        scriptTypeBox.Items.Add(f.Name);
            //    });

            //}
            scriptTypeBox.Visible = true;// (typeBox.SelectedItem.ToString().Equals(NEW_SCRIPT));
            scriptTypeBox.Items.Clear();
            //if (scriptTypeBox.Visible)
            {
                FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList().ForEach((f) =>
                {
                    scriptTypeBox.Items.Add(f.Name);
                });

            }
        }
        public void setForLevel()
        {
            typeBox.SelectedItem = NewForm.NEW_LEVEL;
        }

        private void initControls()
        {
            mainPanel = new TableLayoutPanel();
            mainPanel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            //mainPanel.AutoSize = true;

            //mainPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;//.AddColumns;

            //mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
            //mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
           
            
            
            #region type

            typeLabel = new Label();
            typeLabel.Text = "New";
            typeLabel.TextAlign = ContentAlignment.MiddleRight;
           // typePanel.Controls.Add(typeLabel);
            mainPanel.Controls.Add(typeLabel, 0, 0);

            typeBox = new ComboBox();
            typeBox.Dock = DockStyle.Fill;
           // typePanel.Controls.Add(typeBox);
            mainPanel.Controls.Add(typeBox, 1, 0);

            //mainPanel.Controls.Add(typePanel, 0, 0);
            #endregion 

            #region name

            nameLabel = new Label();
            nameLabel.Text = "Name";
            nameLabel.TextAlign = ContentAlignment.MiddleRight;
            mainPanel.Controls.Add(nameLabel, 0, 1);
           // namePanel.Controls.Add(nameLabel);

            nameBox = new TextBox();
            nameBox.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(nameBox, 1, 1);
           // namePanel.Controls.Add(nameBox);

           // mainPanel.Controls.Add(namePanel, 0, 1);

            #endregion


            #region detail


            detailLabel = new Label();
            detailLabel.TextAlign = ContentAlignment.MiddleRight;
            detailLabel.Text = "For";
            mainPanel.Controls.Add(detailLabel, 0, 2);
           // detailPanel.Controls.Add(detailLabel);

            scriptTypeBox = new ComboBox();
            scriptTypeBox.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(scriptTypeBox, 1, 2);
           // detailPanel.Controls.Add(scriptTypeBox);

            //mainPanel.Controls.Add(detailPanel, 0, 2);

            #endregion

            #region buttons
            btnPanel = new FlowLayoutPanel();
            btnPanel.FlowDirection = FlowDirection.LeftToRight;
            //btnPanel.AutoSize = true;
            btnPanel.Dock = DockStyle.Fill;
            //btnPanel.Width = Width;
            btnPanel.Anchor = AnchorStyles.Right ;
           // btnPanel.BackColor = Color.AliceBlue;

            btnOkay = new Button();
            btnOkay.Dock = DockStyle.Fill;
            btnOkay.Text = "Okay";
           // mainPanel.Controls.Add(btnOkay, 0, 3);
            btnPanel.Controls.Add(btnOkay);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            //btnCancel.Dock = DockStyle.Fill;
            //mainPanel.Controls.Add(btnCancel, 2, 3);
            btnPanel.Controls.Add(btnCancel);
            
            mainPanel.Controls.Add(btnPanel, 0, 3);
            mainPanel.SetColumnSpan(btnPanel, 2);
            #endregion

            mainPanel.SetRowSpan(btnPanel, 2);
            //scriptTypeBox = new ComboBox();
            //scriptTypeBox.Width = mainPanel.Width;
            //scriptTypeBox.Visible = false;
            //nameFlowPanel.Controls.Add(scriptTypeBox);
            

     
            //FlowLayoutPanel btnFlowPanel = new FlowLayoutPanel();
            //btnFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            //btnFlowPanel.AutoSize = true;
            //btnFlowPanel.Dock = DockStyle.Bottom;
            //mainPanel.Controls.Add(btnFlowPanel);


            


        }

        private void addControls()
        {



            mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);
        }

        private void configureControls()
        {

            typeBox.Items.Add(NEW_SCRIPT);
            typeBox.Items.Add(NEW_TYPE);
            typeBox.Items.Add(NEW_LEVEL);
            //typeBox.Items.Add(NEW_CODE);
            typeBox.SelectedItem = NEW_TYPE;

            typeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeBox.SelectedIndexChanged += (s, a) =>
            {
                scriptTypeBox.Visible = (typeBox.SelectedItem.ToString().Equals(NEW_SCRIPT));
                scriptTypeBox.Items.Clear();
                if (scriptTypeBox.Visible)
                {
                    FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList().ForEach((f) => {
                        scriptTypeBox.Items.Add(f.Name);
                    });
                    
                }

            };

            scriptTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;

            nameBox.TextChanged += (s, a) =>
            {
                btnOkay.Enabled = nameBox.Text.Length > 1;
                if (scriptTypeBox.Visible)
                    btnOkay.Enabled &= scriptTypeBox.SelectedItem != null && scriptTypeBox.SelectedItem.ToString().Length > 0;
                
            };

            

            btnOkay.Enabled = false;

            btnOkay.Click += (s, a) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancel.Click += (s, a) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NewForm
            // 
            this.ClientSize = new System.Drawing.Size(335, 311);
            this.Name = "NewForm";
            this.ResumeLayout(false);

        }

    }
}
