using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using Whiskey2D.Core.Managers;
using System.Windows.Forms;
using System.Drawing;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Actions
{
    class ViewInstanceDetailsAction : AbstractAction
    {

        public ObjectManager Objects { get; private set; }
       
        private DataGridView grid;
        private BindingSource data;
        ToolStripControlHost host;

        private BindingContext bc;

        public ViewInstanceDetailsAction(ObjectManager objManager)
            : base("View Objects", Assets.AssetManager.ICON_FILE)
        {
            Objects = objManager;
            bc = new BindingContext();

            data = new BindingSource();
          
           

            grid = new DataGridView();
           // grid.Dock = DockStyle.Fill;
            //grid.AutoSize = true;
            grid.DataSource = data;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoGenerateColumns = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            //grid.Enabled = false;
            
           
            DataGridViewColumn column = new DataGridViewTextBoxColumn();

            


         

            column.DataPropertyName = "Name";
            column.HeaderText = "Name";
            column.ValueType = typeof(String);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = .6f;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Type";
            column.HeaderText = "Type";
            column.FillWeight = .6f;
            column.ValueType = typeof(String);
            grid.Columns.Add(column);

           
            

        }

        protected override void setupDropDown(ToolStripDropDownButton btn)
        {


            
            ToolStripDropDown dropDown = btn.DropDown;

            //dropDown.AutoSize = false;
            dropDown.Margin = Padding.Empty;
            dropDown.Padding = Padding.Empty;
            host = new ToolStripControlHost(grid);
           
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = grid.Size;
            dropDown.Size = grid.Size;
            //((ToolStripDropDownMenu)btn.DropDown).ShowImageMargin = false;
            //((ToolStripDropDownMenu)btn.DropDown).ShowItemToolTips = false;
            dropDown.Items.Add(host);
           
            grid.BindingContext = bc;
            
            //host.Margin = Padding.Empty;
            //host.Padding = Padding.Empty;
            //btn.DropDown.Margin = Padding.Empty;
            //btn.DropDown.Padding = Padding.Empty;
            //host.Width = grid.Width*2;
            //host.Height = grid.Height * 2;
            //btn.DropDown.Width = host.Width;
            //btn.DropDown.Items.Add(host);

            grid.SelectionChanged += (s, a) =>
            {
                DataGridViewSelectedRowCollection rows = grid.SelectedRows;
                if (rows.Count > 0)
                {
                    //DataGridViewRow row = rows[0];
                    //object name = row.Cells[1].Value;
                    //Console.WriteLine(name);
                    SelectionManager.Instance.SelectedInstance = (InstanceDescriptor)Objects.getObject((string)rows[0].Cells[0].Value);
                    //dropDown.Close();
                }

            };

            base.setupDropDown(btn);
        }

        protected override void run()
        {
            refresh();
            
        }

        private void refresh()
        {
            grid.Invoke(new NoArgFunction(() =>
            {
                data.Clear();
                foreach (GameObject gob in Objects.getAllObjects())
                {
                    DataRow row = new DataRow(gob.Name, gob.getTypeName());
                    data.Add(row);
                }
                grid.Refresh();
                grid.ClearSelection();
                SelectionManager.Instance.SelectedInstance = null;
                //grid.DataSource = data;
                //host.Invalidate();
               

                //Form popup = new Form();
                //popup.Controls.Add(grid);
                //popup.ShowDialog();
            }));
        }
        


        public class DataRow
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public DataRow(string name, string type)
            {
                Name = name;
                Type = type;
            }
        }

    }
}
