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
        public const string NEW_CODE = "Code";

        private Label typeLabel;
        private Label nameLabel;
        private Button btnCancel;
        private Button btnOkay;
        private TextBox nameBox;
        private ComboBox typeBox;
        private ComboBox scriptTypeBox;
        private FlowLayoutPanel mainPanel;

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

            initControls();
            configureControls();
            addControls();
        }


        public void setForType()
        {
            typeBox.SelectedItem = NewForm.NEW_TYPE;
        }
        public void setForCode()
        {
            typeBox.SelectedItem = NewForm.NEW_CODE;

        }
        public void setForScript()
        {
            typeBox.SelectedItem = NewForm.NEW_SCRIPT;
            scriptTypeBox.Visible = (typeBox.SelectedItem.ToString().Equals(NEW_SCRIPT));
            scriptTypeBox.Items.Clear();
            if (scriptTypeBox.Visible)
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
            mainPanel = new FlowLayoutPanel();
            mainPanel.AutoSize = true;
            mainPanel.FlowDirection = FlowDirection.TopDown;

            FlowLayoutPanel topFlowPanel = new FlowLayoutPanel();
            topFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            topFlowPanel.AutoSize = true;

            typeLabel = new Label();
            typeLabel.Text = "Select file type ";
            topFlowPanel.Controls.Add(typeLabel);

            typeBox = new ComboBox();
            topFlowPanel.Controls.Add(typeBox);

            mainPanel.Controls.Add(topFlowPanel);

            FlowLayoutPanel nameFlowPanel = new FlowLayoutPanel();
            nameFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            nameFlowPanel.AutoSize = true;
            mainPanel.Controls.Add(nameFlowPanel);

            nameLabel = new Label();
            nameLabel.Text = "Name ";
            nameFlowPanel.Controls.Add(nameLabel);
            
            nameBox = new TextBox();
            nameBox.Dock = DockStyle.Fill;
            nameFlowPanel.Controls.Add(nameBox);
            nameFlowPanel.SetFlowBreak(nameBox, true);

            scriptTypeBox = new ComboBox();
            scriptTypeBox.Width = mainPanel.Width;
            scriptTypeBox.Visible = false;
            nameFlowPanel.Controls.Add(scriptTypeBox);
            

            #region Buttons

            FlowLayoutPanel btnFlowPanel = new FlowLayoutPanel();
            btnFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            btnFlowPanel.AutoSize = true;
            btnFlowPanel.Dock = DockStyle.Bottom;
            mainPanel.Controls.Add(btnFlowPanel);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnFlowPanel.Controls.Add(btnCancel);

            btnOkay = new Button();
            btnOkay.Dock = DockStyle.Fill;
            btnOkay.Text = "Okay";
            btnFlowPanel.Controls.Add(btnOkay);

            #endregion

        }

        private void addControls()
        {



           // mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);
        }

        private void configureControls()
        {

            typeBox.Items.Add(NEW_SCRIPT);
            typeBox.Items.Add(NEW_TYPE);
            typeBox.Items.Add(NEW_LEVEL);
            typeBox.Items.Add(NEW_CODE);
            typeBox.SelectedItem = NEW_CODE;

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

    }
}
