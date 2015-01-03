using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Properties.Editors
{
    public class PropertyEditor : Control
    {
       
        protected List<ToolStripItem> ToolStripItems { get; set; }

        public string Title { get; protected set; }

        public Descriptor PropertyObject { get; private set; }

        public PropertyEditor(Descriptor propertyDescriptor)
        {
            PropertyObject = propertyDescriptor;

            ToolStripItems = new List<ToolStripItem>();

            BackColor = Color.White;
            Size = new Size(50, 50);
        }

        public ToolStripItemCollection getToolStripCollection(ToolStrip strip)
        {
            return new ToolStripItemCollection(strip, ToolStripItems.ToArray());
        }

        private void initControls()
        {

        }


        private void addControls()
        {

        }

    }
}
