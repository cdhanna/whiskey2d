using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Dockable;
using WhiskeyEditor.UI.Output;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Properties;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Menu;
using WhiskeyEditor.UI.Toolbar;
using WhiskeyEditor.UI.Status;

namespace WhiskeyEditor.UI
{
    class TopView : Form
    {
        private Panel mainPanel;

        private OutputView outputView;
        private LibraryView libraryView;
        private DocumentView docView;
        private PropertyView propView;

        private DockControl outputViewDock;
        private DockControl libraryViewDock;
        private DockControl docViewDock;
        private DockControl propViewDock;

        private WhiskeyMenu menu;
        private ToolBarStrip toolBar;
        private WhiskeyStatusBar statBar;

        public TopView()
        {

            this.Size = new Size(1440, 900);
            Text = ProjectManager.Instance.ActiveProject.Name;
            ProjectManager.Instance.ProjectChanged += (s, a) =>
            {
                Text = a.NewProject.Name;
            };


            initControls();
            configureControls();
            addControls();
            
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = "Whiskey2D - " + value;
            }
        }


        public DockControl lookUpDockControl(string name)
        {
            switch (name)
            {
                case UIManager.VIEW_NAME_OUTPUT:
                    return outputViewDock;
                case UIManager.VIEW_NAME_LIBRARY:
                    return libraryViewDock;
                case UIManager.VIEW_NAME_DOCUMENTS:
                    return docViewDock;
                case UIManager.VIEW_NAME_PROPERTIES:
                    return propViewDock;
                default:
                    throw new WhiskeyException("Dockable : " + name + " could not be found");
            }
        }



        private void configureControls()
        {

            DockControlDockChangedEventHandler dockChangedHandler = new DockControlDockChangedEventHandler((s, args) =>
            {
                menu.setViewViewable(args.DockControl.Name, args.DockControl.Viewable);
            });

            outputViewDock.DockedChanged += dockChangedHandler;
            libraryViewDock.DockedChanged += dockChangedHandler;
            docViewDock.DockedChanged += dockChangedHandler;
            propViewDock.DockedChanged += dockChangedHandler;

            menu.ViewToggled += (s, args) =>
            {
                DockControl dockControl = lookUpDockControl(args.ViewName);
                if (dockControl.Viewable)
                    dockControl.undock();
                else dockControl.dock();

            };

            //toolBar.ButtonPressed += (s, args) =>
            //{
            //    switch (args.ButtonName)
            //    {
            //        case UIManager.COMMAND_PLAY:

            //            string dll = UIManager.Instance.Compiler.compile();
            //           //// UIManager.Instance.GobInstances.convertToGobs(dll, "default");
            //            ProjectManager.Instance.ActiveProject.buildExecutable();
            //            ProjectManager.Instance.ActiveProject.runGame();

            //            break;
            //        case UIManager.COMMAND_COMPILE:
            //            UIManager.Instance.Compiler.compile();
            //            break;
            //        case UIManager.COMMAND_SAVE:
            //            docView.saveCurrent();
            //            break;
            //        default:
            //            break;
            //    }


            //};

            this.KeyPreview = true;
            this.KeyDown += (s, a) =>
            {
                if (a.Control && a.KeyCode == Keys.S)
                {
                    docView.saveCurrent();
                }
            };


            libraryView.SelectionChanged += (s, args) =>
            {
                docView.openDocument(args.Selected.FilePath);
                FileDescriptor desc = FileManager.Instance.lookUp(args.Selected.FilePath);
                propView.setPropertyContent(UIManager.Instance.getPropertyEditor(desc));
            };

            libraryView.SelectionChanged += (s, args) =>
            {
                

            };
            libraryView.ClickedOnNode += (s, args) =>
            {
                FileDescriptor desc = FileManager.Instance.lookUp(args.Selected.FilePath);
                propView.setPropertyContent(UIManager.Instance.getPropertyEditor(desc));
            };



            docView.PropertyChangeRequested += (s, args) =>
            {

                propView.setPropertyContent(UIManager.Instance.getPropertyEditor(args.PropertyObject));
            };


           
        }

        private void initControls()
        {
            toolBar = new ToolBarStrip();
            statBar = new WhiskeyStatusBar();
            menu = new WhiskeyMenu();
            mainPanel = new Panel();
            mainPanel.Padding = new Padding(0, menu.Height + toolBar.Height, 0, statBar.Height);



            propView = new PropertyView();
            propView.Name = "Properties";
            propViewDock = new DockControl(mainPanel, DockStyle.Right);
            propViewDock.Name = UIManager.VIEW_NAME_PROPERTIES;
            propViewDock.PrimaryView = propView;

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


            docView = new DocumentView();
            docView.Name = "Documents";
            docViewDock = new DockControl(mainPanel, DockStyle.Fill);
            docViewDock.Name = UIManager.VIEW_NAME_DOCUMENTS;
            docViewDock.PrimaryView = docView;

        }

        private void addControls()
        {
            Controls.Add(toolBar);
            Controls.Add(menu);

            statBar.Dock = DockStyle.Bottom;
            Controls.Add(statBar);
            

            mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);

            docViewDock.dock();
            libraryViewDock.dock();
            propViewDock.dock();
            outputViewDock.dock();

            propViewDock.Size = new Size(300, 100);
            //outputViewDock.dock(DockStyle.Bottom);

        }



    }
}
