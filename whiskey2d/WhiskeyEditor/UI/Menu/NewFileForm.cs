using System;
using System.Collections.Generic;

using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.Backend.Managers;
using System.Text.RegularExpressions;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Menu
{
    public partial class NewFileForm : Form
    {

        public const string NEW_TYPE = "Type";
        public const string NEW_SCRIPT = "Script";
        public const string NEW_LEVEL = "Level";
        public const string NEW_FOLDER = "Folder";

        public DialogResult DialogResult { get; private set; }
        public String SelectedType { get { return typeBox.SelectedItem.ToString(); } }
        public String SelectedText { get { return nameBox.Text;  } }
        public String SelectedOption
        {
            get
            {
                if (scriptBox.SelectedItem != null)
                    return scriptBox.SelectedItem.ToString();
                else return "";
            }
        }
        

        public NewFileForm()
        {
            InitializeComponent();

            typeBox.Items.Add(NEW_SCRIPT);
            typeBox.Items.Add(NEW_TYPE);
            typeBox.Items.Add(NEW_LEVEL);
            typeBox.Items.Add(NEW_FOLDER);
            //typeBox.Items.Add(NEW_CODE);
            typeBox.SelectedItem = NEW_TYPE;


            typeBox.SelectedIndexChanged += (s, a) =>
            {
                scriptBox.Visible = (typeBox.SelectedItem.ToString().Equals(NEW_SCRIPT));
                scriptBox.Items.Clear();
                if (scriptBox.Visible)
                {
                    //scriptBox.Items.Add("GameObject");
                    FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList().ForEach((f) =>
                    {
                        scriptBox.Items.Add(f.Name);
                    });

                }

            };

            scriptBox.DropDownStyle = ComboBoxStyle.DropDownList;

            nameBox.TextChanged += (s, a) =>
            {
                validateClassName();
            };
            scriptBox.SelectedIndexChanged += (s, a) =>
            {
                validateClassName();
            };


            okayBtn.Click += (s, a) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            cancelBtn.Click += (s, a) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            okayBtn.Enabled = false;


            pathBox.Visible = false;
            pathLabel.Visible = false;
        }

        private void validateClassName()
        {
            okayBtn.Enabled = false;
            string name = nameBox.Text;
            Regex alphaNumeric = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
            if (alphaNumeric.IsMatch(name))
            {
                okayBtn.Enabled = true;
            }
            if (scriptBox.Visible)
                okayBtn.Enabled &= scriptBox.SelectedItem != null && scriptBox.SelectedItem.ToString().Length > 0;
        }

        public void setForType(string path)
        {
            setPath(path);
            typeBox.SelectedItem = NewFileForm.NEW_TYPE;
            scriptBox.Visible = false;
        }



        public void setForScript(string path)
        {
            setPath(path);
            typeBox.SelectedItem = NewFileForm.NEW_SCRIPT;
            
            scriptBox.Visible = true;
            scriptBox.Items.Clear();
            //scriptBox.Items.Add("GameObject");
            FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList().ForEach((f) =>
            {
                scriptBox.Items.Add(f.Name);
            });
        }
        public void setForLevel(string path)
        {
            setPath(path);
            typeBox.SelectedItem = NewFileForm.NEW_LEVEL;
        }

        public void setForFolder(string path)
        {
            setPath(path);
            typeBox.SelectedItem = NewFileForm.NEW_FOLDER;
        }

        public void setPath(string path)
        {
            pathBox.Text = path;
        }

    }
}
