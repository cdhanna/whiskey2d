using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;

namespace WhiskeyEditor.UI.Properties.Editors
{
    public class PropertyContent : Control
    {
       
        protected List<ToolStripItem> ToolStripItems { get; set; }

        public string Title { get; protected set; }

        public Object PropertyObject { get; private set; }

        public PropertyContent(Object propertyObject)
        {
            PropertyObject = propertyObject;

            ToolStripItems = new List<ToolStripItem>();

            BackColor = Color.Yellow;
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
