using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whiskey2D.Core;
using WhiskeyEditor.Controls.TypeEditor;
using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor
{
    public partial class GameObjectIcon : UserControl
    {
        private Type gobType;
        public TypeEditor TypeEditor { get; set; }
        public GameObjectDescriptor Descriptor { get; set; }
        public GameObjectIcon()
        {
            InitializeComponent();

            this.MouseDown += GameObjectIcon_MouseDown;
            
        }

        void GameObjectIcon_MouseDown(object sender, MouseEventArgs e)
        {
           DoDragDrop(gobType, DragDropEffects.All);
        }

        public Type GobType
        {
            get
            {
                return gobType;
            }
            set
            {
                gobType = value;
                typeLabel.Text = gobType.Name;

                //GameObject gobInst = (GameObject) gobType.GetConstructor(new Type[]{}).Invoke(new object[]{});
                //if (gobInst.Sprite != null
                //typePictureBox.BackgroundImage = Image.FromFile(
            }
        }

        private void GameObjectIcon_Load(object sender, EventArgs e)
        {

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            TypeEditor.Descriptor = Descriptor;
        }



    }
}
