using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using System.CodeDom.Compiler;
using System.IO;

namespace WhiskeyEditor.UI.Output
{
    class OutputView : Control
    {

        private DataGridView grid;
        private BindingSource data;

        public OutputView()
        {
            this.BackColor = Color.Red;

            this.Size = new Size(100, 100);
            
            initControls();
            addControls();

            UIManager.Instance.Compiler.Compiled += new CompileEventHandler(compiledListener);

            

        }

        private void compiledListener(object sender, CompileEventArgs args)
        {
            this.Invoke(new NoArgFunction(() => {
                data.Clear();
                foreach (CompilerError err in args.Errors)
                {
                    err.FileName = err.FileName.Substring(err.FileName.LastIndexOf(Path.DirectorySeparatorChar));
                    data.Add(err);
                    
                }
            
            }));

            

            

        }

       
        private void initControls()
        {
            data = new BindingSource();

            
            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.DataSource = data;
            grid.AutoGenerateColumns = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FileName";
            column.HeaderText = "File";
            column.FillWeight = .3f;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Line";
            column.HeaderText = "Line";
            column.FillWeight = .1f;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ErrorText";
            column.HeaderText = "Error";
            column.FillWeight = .6f;
            grid.Columns.Add(column);

            
        }

        private void addControls()
        {
            this.Controls.Add(grid);
        }

    }
}
