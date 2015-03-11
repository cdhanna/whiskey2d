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
    using Layer = Whiskey2D.Core.Layer;

    class ViewLevelLayersAction : AbstractAction 
    {

        public LevelDescriptor Descriptor { get; private set; }

        private LayerDetailsControl control;
        private BindingSource data;
        ToolStripControlHost host;
        DataGridViewComboBoxColumn shaderColumn, postShaderColumn;
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

            Rectangle dragBoxFromMouseDown = Rectangle.Empty ;
            int rowIndexFromMouseDown = 0;
            int rowIndexOfItemUnderMouseToDrop = 0;
            control.DataGrid.AllowDrop = true;
            control.DataGrid.MouseMove += (s, e) =>
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    // If the mouse moves outside the rectangle, start the drag.
                    if (dragBoxFromMouseDown != Rectangle.Empty &&
                        !dragBoxFromMouseDown.Contains(e.X, e.Y))
                    {

                        // Proceed with the drag and drop, passing in the list item.                    
                        DragDropEffects dropEffect = control.DataGrid.DoDragDrop(
                        control.DataGrid.Rows[rowIndexFromMouseDown],
                        DragDropEffects.Move);
                    }
                }
            }; 
            control.DataGrid.MouseDown += (s, e) => {
                rowIndexFromMouseDown = control.DataGrid.HitTest(e.X, e.Y).RowIndex;
                if (rowIndexFromMouseDown != -1)
                {
                    Size dragSize = SystemInformation.DragSize;

                    dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                                   e.Y - (dragSize.Height / 2)),
                                        dragSize);
                }
                else dragBoxFromMouseDown = Rectangle.Empty;
            };
            control.DataGrid.DragOver += (s, a) => { a.Effect = DragDropEffects.Move; };
            control.DataGrid.DragDrop += (s, e) =>
            {
                // The mouse locations are relative to the screen, so they must be 
                // converted to client coordinates.
                Point clientPoint = control.DataGrid.PointToClient(new Point(e.X, e.Y));

                // Get the row index of the item the mouse is below. 
                rowIndexOfItemUnderMouseToDrop =
                    control.DataGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                // If the drag operation was a move then remove and insert the row.
                if (e.Effect == DragDropEffects.Move && rowIndexOfItemUnderMouseToDrop > -1 && rowIndexOfItemUnderMouseToDrop < Descriptor.Level.Layers.Count)
                {
                    DataGridViewRow rowToMove = e.Data.GetData(
                        typeof(DataGridViewRow)) as DataGridViewRow;

                    Layer layerToMove = Descriptor.Level.Layers[rowIndexFromMouseDown];
                    //Layer layerInsertNextTo = Descriptor.Level.Layers[rowIndexOfItemUnderMouseToDrop];

                    //control.DataGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    //control.DataGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                    Descriptor.Level.Layers.Remove(layerToMove);
                    Descriptor.Level.Layers.Insert(rowIndexOfItemUnderMouseToDrop, layerToMove);
                    refresh();
                   // control.DataGrid.Invalidate();
                }
            };
           

            // This event handler manually raises the CellValueChanged event 
            // by calling the CommitEdit method. 
            control.DataGrid.CurrentCellDirtyStateChanged += (s, a) =>
            {
                if (control.DataGrid.IsCurrentCellDirty && control.DataGrid.SelectedCells[0].ColumnIndex > 0)
                {

                    
                    {
                        control.DataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
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

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "ScreenShader";
            column.HeaderText = "ScreenShader";
            column.ValueType = typeof(Boolean);
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.FillWeight = .4f;
            control.DataGrid.Columns.Add(column);

            shaderColumn = new DataGridViewComboBoxColumn();
            shaderColumn.AutoComplete = true;
            shaderColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            
            shaderColumn.DataPropertyName = "ShaderMode";
            shaderColumn.HeaderText = "ShaderMode";
            shaderColumn.ValueType = typeof(String);
            
            shaderColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            shaderColumn.FillWeight = .6f;
            control.DataGrid.Columns.Add(shaderColumn);

            postShaderColumn = new DataGridViewComboBoxColumn();
            postShaderColumn.AutoComplete = true;
            postShaderColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

            postShaderColumn.DataPropertyName = "PostShaderMode";
            postShaderColumn.HeaderText = "PostShaderMode";
            postShaderColumn.ValueType = typeof(String);

            postShaderColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            postShaderColumn.FillWeight = .6f;
            control.DataGrid.Columns.Add(postShaderColumn);


            DataGridViewComboBoxColumn blendColumn = new DataGridViewComboBoxColumn();
            blendColumn.AutoComplete = true;
            blendColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            blendColumn.DataPropertyName = "BlendMode";
            blendColumn.HeaderText = "BlendMode";
            blendColumn.ValueType = typeof(BlendMode);
            blendColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            blendColumn.FillWeight = .6f;
            blendColumn.Items.Add(BlendMode.NORMAL);
            blendColumn.Items.Add(BlendMode.ALPHABLEND);
            blendColumn.Items.Add(BlendMode.ADDITIVE);
            control.DataGrid.Columns.Add(blendColumn);


            

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
            dropDown.AutoClose = false;
            control.BindingContext = bc;

            control.CloseBtn.Click += (s, a) => dropDown.Close();

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

                shaderColumn.Items.Clear();
                postShaderColumn.Items.Clear();
                shaderColumn.Items.Add(Layer.DEFAULT_SHADER_MODE);
                postShaderColumn.Items.Add(Layer.DEFAULT_SHADER_MODE);
                FileManager.Instance.FileDescriptors.Where(f => f is ShaderDescriptor).ToList().ForEach(s => { shaderColumn.Items.Add(((ShaderDescriptor)s).Name); postShaderColumn.Items.Add(((ShaderDescriptor)s).Name); });
                


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
