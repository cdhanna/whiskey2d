using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.UI.Documents
{
    public partial class LayerDetailsControl : UserControl
    {

        public DataGridView DataGrid { get { return dataGrid; } }

        public TextBox NameBox { get { return nameBox; } }
        public Button AddBtn { get { return addBtn; } }

        public LayerDetailsControl()
        {
            InitializeComponent();
        }
    }
}
