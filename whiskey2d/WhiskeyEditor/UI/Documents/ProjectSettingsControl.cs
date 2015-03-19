using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents
{
    public partial class ProjectSettingsControl : UserControl
    {

        public bool IsFullScreen
        {
            get
            {
                return isFullScreen.Checked;
            }
            set
            {
                isFullScreen.Checked = value;
            }
        }

        public bool CloseOnExit
        {
            get
            {
                return closeOnExit.Checked;
            }
            set
            {
                closeOnExit.Checked = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return projectNameBox.Text;
            }
            set
            {
                projectNameBox.Text = value;
            }
        }

        public string Author
        {
            get
            {
                return authorNameBox.Text;
            }
            set
            {
                authorNameBox.Text = value;
            }
        }

        public string StartScene
        {
            get
            {
                return (string)startLevelBox.SelectedItem;
            }
            set
            {
                startLevelBox.SelectedItem = (string)value;
            }
        }

        public ProjectSettingsControl()
        {
            InitializeComponent();

            startLevelBox.DropDownStyle = ComboBoxStyle.DropDownList;


            projectNameBox.Enabled = false;
            authorNameBox.Enabled = false;

        }

        public void setLevelOptions(List<string> levelNames)
        {
            object oldSelected = startLevelBox.SelectedItem;
            startLevelBox.Items.Clear();

            levelNames.ForEach(s => startLevelBox.Items.Add(s));

            if (levelNames.Contains((string)oldSelected))
            {
                startLevelBox.SelectedItem = oldSelected;
            }
            else if (startLevelBox.Items.Count > 0) 
                startLevelBox.SelectedItem = startLevelBox.Items[0];

        }

    }
}
