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
    class ViewLevelLayersAction : AbstractAction 
    {

        public LevelDescriptor Descriptor { get; private set; }

        private LayerDetailsControl control;
        private BindingSource data;
        ToolStripControlHost host;

        private BindingContext bc;

        public ViewLevelLayersAction(LevelDescriptor lvl)
            : base("Layers", Assets.AssetManager.ICON_FILE_LEVEL)
        {
            Descriptor = lvl;
            control = new LayerDetailsControl();

            control.AddBtn.Enabled = false;

            control.AddBtn.Click += (s, a) =>
            {

                Layer layer = new Layer();
                layer.Name = control.NameBox.Text;
                Descriptor.Level.Layers.Add(layer);

                control.NameBox.Text = "";
                refresh();
            };

            control.NameBox.TextChanged += (s, a) =>
            {
                Layer existingLayer = Descriptor.Level.Layers.Find(l => l.Name.Equals(control.NameBox.Text));
                control.AddBtn.Enabled = (existingLayer == null) && control.NameBox.Text.Length > 0;
            };

            data = new BindingSource();
            

            control.DataGrid.DataSource = data;
            control.DataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            control.DataGrid.AutoGenerateColumns = false;
            control.DataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            control.DataGrid.ShowEditingIcon = false;
            


            control.DataGrid.AllowUserToAddRows = false;
            control.DataGrid.AllowUserToDeleteRows = true;
            control.DataGrid.MultiSelect = false;

            

           

            // This event handler manually raises the CellValueChanged event 
            // by calling the CommitEdit method. 
            control.DataGrid.CurrentCellDirtyStateChanged += (s, a) =>
            {
                if (control.DataGrid.IsCurrentCellDirty && control.DataGrid.SelectedCells[0].ColumnIndex > 0)
                {
                    control.DataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            //control.DataGrid.CellEndEdit += (s, a) =>
            //{
            //    Console.WriteLine("EDIT: " + a.RowIndex + " " + a.ColumnIndex);
            //    object value = control.DataGrid.Rows[a.RowIndex].Cells[a.ColumnIndex].Value;
            //    //object value = control.DataGrid.SelectedCells[0].Value;
            //    Console.WriteLine(value);
            //};

           
            control.DataGrid.CellValueChanged += (s, a) =>
            {
                object value = control.DataGrid.Rows[a.RowIndex].Cells[a.ColumnIndex].Value;
                //object value = control.DataGrid.SelectedCells[0].Value;
                Console.WriteLine(value);
                
            };

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.HeaderText = "Name";
            column.ValueType = typeof(String);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = .6f;

          

            control.DataGrid.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "Visible";
            column.HeaderText = "Visible";
            column.ValueType = typeof(Boolean);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = .4f;
            control.DataGrid.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "Locked";
            column.HeaderText = "Locked";
            column.ValueType = typeof(Boolean);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = .4f;
            control.DataGrid.Columns.Add(column);
        }

        protected override void setupDropDown(ToolStripDropDownButton btn)
        {



            ToolStripDropDown dropDown = btn.DropDown;

            //dropDown.AutoSize = false;
            dropDown.Margin = Padding.Empty;
            dropDown.Padding = Padding.Empty;
            host = new ToolStripControlHost(control);

            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = control.Size;
            dropDown.Size = control.Size;
            //((ToolStripDropDownMenu)btn.DropDown).ShowImageMargin = false;
            //((ToolStripDropDownMenu)btn.DropDown).ShowItemToolTips = false;
            dropDown.Items.Add(host);

            control.BindingContext = bc;

            //host.Margin = Padding.Empty;
            //host.Padding = Padding.Empty;
            //btn.DropDown.Margin = Padding.Empty;
            //btn.DropDown.Padding = Padding.Empty;
            //host.Width = grid.Width*2;
            //host.Height = grid.Height * 2;
            //btn.DropDown.Width = host.Width;
            //btn.DropDown.Items.Add(host);

            //grid.SelectionChanged += (s, a) =>
            //{
            //    DataGridViewSelectedRowCollection rows = grid.SelectedRows;
            //    if (rows.Count > 0)
            //    {
            //        //DataGridViewRow row = rows[0];
            //        //object name = row.Cells[1].Value;
            //        //Console.WriteLine(name);
            //        SelectionManager.Instance.SelectedInstance = (InstanceDescriptor)Objects.getObject((string)rows[0].Cells[0].Value);
            //        //dropDown.Close();
            //    }

            //};

            base.setupDropDown(btn);
        }


        protected override void run()
        {
            refresh();
        }

        private void refresh()
        {
            control.DataGrid.Invoke(new NoArgFunction(() =>
            {
                data.Clear();
                foreach (Layer layer in Descriptor.Level.Layers)
                {
                    //DataRow row = new DataRow(gob.Name, gob.getTypeName());
                    data.Add(layer);
                }
                control.DataGrid.Refresh();
                control.DataGrid.ClearSelection();
                //SelectionManager.Instance.SelectedInstance = null;
                //grid.DataSource = data;
                //host.Invalidate();


                //Form popup = new Form();
                //popup.Controls.Add(grid);
                //popup.ShowDialog();
            }));
        }

    }
}
