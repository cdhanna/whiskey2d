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



namespace WhiskeyEditor.Controls.TypeEditor
{
    public partial class GameObjectTypeBrowser : UserControl
    {

        List<GameObjectIcon> gobIcons;
        public TypeEditor TypeEditor { get; set; }

        public GameObjectTypeBrowser()
        {
            InitializeComponent();

            gobIcons = new List<GameObjectIcon>();


            TypeManager.getInstance().addDescriptorAddedListener(
                (descr, type) => {

                    GameObjectIcon foundIcon = null;
                    gobIcons.ForEach((f) =>
                    {
                        if (f.Descriptor.Equals(descr))
                        {
                            foundIcon = f;
                        }
                    });

                    if (foundIcon == null)
                    {
                        GameObjectIcon gobIcon = new GameObjectIcon();
                        gobIcon.GobType = type;
                        gobIcon.Descriptor = descr;
                        gobIcon.TypeEditor = TypeEditor;
                        //gobIcon.Location = new Point(gobIcon.Location.X, (gobIcon.Size.Height + 2) * gobIcons.Count);

                        gobIcons.Add(gobIcon);
                        flowPanel.Controls.Add(gobIcon);
                    }
                    else
                    {
                        foundIcon.GobType = type;
                        foundIcon.Descriptor = descr;
                        foundIcon.TypeEditor = TypeEditor;
                    }
                });


        }
    }
}
