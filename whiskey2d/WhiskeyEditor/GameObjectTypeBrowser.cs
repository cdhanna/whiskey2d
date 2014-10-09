using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;



namespace WhiskeyEditor
{
    public partial class GameObjectTypeBrowser : UserControl
    {

        List<GameObjectIcon> gobIcons;


        public GameObjectTypeBrowser()
        {
            InitializeComponent();

            gobIcons = new List<GameObjectIcon>();


            TypeManager.getInstance().addDescriptorAddedListener(
                (descr, type) => {

                    GameObjectIcon gobIcon = new GameObjectIcon();
                    gobIcon.GobType = type;

                    gobIcon.Location = new Point(gobIcon.Location.X, (gobIcon.Size.Height + 2) * gobIcons.Count);

                    gobIcons.Add(gobIcon);
                    Controls.Add(gobIcon);
                });


        }
    }
}
