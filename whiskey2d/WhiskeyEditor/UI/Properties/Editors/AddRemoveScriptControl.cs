using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    public partial class AddRemoveScriptControl : UserControl
    {

        public event EventHandler<Boolean> ClosedControl = new EventHandler<bool>((s, a) => { });

        public bool Accepted { get; private set; }

        public AddRemoveScriptControl()
        {
            InitializeComponent();
            
            btnAccept.Click += (s, a) =>
            {
                Accepted = true;
                ClosedControl(this, true);
            };
            btnCancel.Click += (s, a) =>
            {
                Accepted = false;
                ClosedControl(this, false);
            };

            VisibleChanged += (s, a) =>
            {
                if (Visible)
                {
                    Accepted = true;
                }
            };

            btnAdd.Click += addSelectedScripts;
            btnRemove.Click += removeSelectedScripts;
            btnAddAll.Click += addAll;
            btnRemoveAll.Click += removeAll;

            allScriptsList.MouseDoubleClick += addSelectedScripts;
            selectedScriptList.MouseDoubleClick += removeSelectedScripts;
        }


        private void addSelectedScripts(object sender, EventArgs args)
        {
            foreach (ListViewItem selected in allScriptsList.SelectedItems)
            {
                selectedScriptList.Items.Add(new ListViewItem(selected.Text));
            }
            allScriptsList.SelectedItems.Clear();
        }

        private void addAll(object sender, EventArgs args)
        {
            foreach (ListViewItem item in allScriptsList.Items)
            {
                selectedScriptList.Items.Add(new ListViewItem(item.Text));
            }
            allScriptsList.SelectedItems.Clear();
        }

        private void removeAll(object sender, EventArgs args)
        {
            selectedScriptList.Items.Clear();
            selectedScriptList.SelectedItems.Clear();
        }



        private void removeSelectedScripts(object sender, EventArgs args)
        {
            while (selectedScriptList.SelectedItems.Count > 0)
            {
                ListViewItem selected = selectedScriptList.SelectedItems[0];
                selectedScriptList.Items.Remove(selected);
            }
            selectedScriptList.SelectedItems.Clear();
        }


        public void refreshSelectedScripts(List<string> scriptNames)
        {
            Invoke(new NoArgFunction(() => {

                selectedScriptList.Items.Clear();
                scriptNames.ForEach((name) => { selectedScriptList.Items.Add(name); });

            }));

        }

        public void refreshAllScripts(string typeName)
        {
            Invoke(new NoArgFunction(() =>
            {
                //ListView.ListViewItemCollection collection = new ListView.ListViewItemCollection(allScriptsList);
                allScriptsList.Items.Clear();
                List<string> scriptNames = ScriptManager.Instance.getScriptNamesForType(typeName);
                foreach (string name in scriptNames)
                {
                    allScriptsList.Items.Add(name);

                }

            }));

            //allScriptsList.Items = collection;

        }

        public List<string> getSelectedScripts()
        {
            List<string> names = new List<string>();
            foreach (ListViewItem item in selectedScriptList.Items)
            {
                names.Add(item.Text);
            }
            return names;

        }

    }
}
