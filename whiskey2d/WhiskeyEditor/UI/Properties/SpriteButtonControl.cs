using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhiskeyEditor.UI.Properties
{
    public partial class SpriteButtonControl : UserControl
    {

        public Button PixelButton { get { return pixelButton; } }
        public Button FileButton { get { return fileButton; } }
        public ComboBox ArtBox { get { return artBox; } }
        public SpriteButtonControl()
        {
            InitializeComponent();
            artBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
