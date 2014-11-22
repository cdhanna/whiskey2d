using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Dockable;
using WhiskeyEditor.UI.Output;
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Menu;

namespace WhiskeyEditor.UI
{
    class TopView : Form
    {
        private Panel mainPanel;

        private OutputView outputView;
        private LibraryView libraryView;

        private DockControl outputViewDock;
        private DockControl libraryViewDock;

        private WhiskeyMenu menu;

        public TopView()
        {

            this.Size = new Size(600, 400);


            initControls();
            addControls();
            configureControls();
        }

        public DockControl lookUpDockControl(string name)
        {
            switch (name)
            {
                case UIManager.VIEW_NAME_OUTPUT:
                    return outputViewDock;
                case UIManager.VIEW_NAME_LIBRARY:
                    return libraryViewDock;
                default:
                    throw new WhiskeyException("Dockable : " + name + " could not be found");
            }
        }

        private void configureControls()
        {
         

            outputViewDock.DockedChanged += (s, args) =>
                {
                    menu.setViewViewable(args.DockControl.Name, args.DockControl.Viewable);
                };

            libraryViewDock.DockedChanged += (s, args) =>
            {
                menu.setViewViewable(args.DockControl.Name, args.DockControl.Viewable);
            };

            menu.ViewToggled += (s, args) =>
            {
                DockControl dockControl = lookUpDockControl(args.ViewName);
                if (dockControl.Viewable)
                    dockControl.undock();
                else dockControl.dock();

            };

        }

        private void initControls()
        {

            menu = new WhiskeyMenu();
            mainPanel = new Panel();
            mainPanel.Padding = new Padding(0, menu.Height, 0, 0);

            


            outputView = new OutputView();
            outputView.Name = "Output";
            outputViewDock = new DockControl(mainPanel, DockStyle.Bottom);
            outputViewDock.Name = UIManager.VIEW_NAME_OUTPUT;
            outputViewDock.PrimaryView = outputView;



            libraryView = new LibraryView();
            libraryView.Name = "Library";
            libraryViewDock = new DockControl(mainPanel, DockStyle.Left);
            libraryViewDock.Name = UIManager.VIEW_NAME_LIBRARY;
            libraryViewDock.PrimaryView = libraryView;


        }

        private void addControls()
        {
            Controls.Add(menu);

            mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);
            //outputViewDock.dock(DockStyle.Bottom);

        }



    }
}
