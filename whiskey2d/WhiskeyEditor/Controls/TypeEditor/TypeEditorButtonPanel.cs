using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;


namespace WhiskeyEditor.Controls.TypeEditor
{
    public partial class TypeEditorButtonPanel : UserControl
    {

        public TypeEditor TypeEditor { get; set; }

        public TypeEditorButtonPanel()
        {
            InitializeComponent();
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            GameObjectDescriptor descr = TypeEditor.Descriptor;
            TypeManager.getInstance().updateDescriptor(descr);
            TypeEditor.Clear();
        }

        private void newTypeBtn_Click(object sender, EventArgs e)
        {
            
            TypeEditor.Descriptor = new ClassLoader.GameObjectDescriptor("Project", "Unnamed");
        }
    }
}
