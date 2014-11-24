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
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Menu;
using WhiskeyEditor.UI.Toolbar;

namespace WhiskeyEditor.UI
{
    class TopView : Form
    {
        private Panel mainPanel;

        private OutputView outputView;
        private LibraryView libraryView;
        private DocumentView docView;


        private DockControl outputViewDock;
        private DockControl libraryViewDock;
        private DockControl docViewDock;

        private WhiskeyMenu menu;
        private ToolBarStrip toolBar;

        public TopView()
        {

            this.Size = new Size(600, 400);
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


            menu.ViewToggled += (s, args) =>
            {
                DockControl dockControl = lookUpDockControl(args.ViewName);
                if (dockControl.Viewable)
                    dockControl.undock();
                else dockControl.dock();

            };

            toolBar.ButtonPressed += (s, args) =>
            {
                switch (args.ButtonName)
                {
                    case UIManager.COMMAND_PLAY:

                        string dll = UIManager.Instance.Compiler.compile();
                        UIManager.Instance.GobInstances.convertToGobs(dll, "default");
                        ProjectManager.Instance.ActiveProject.buildExecutable();
                        ProjectManager.Instance.ActiveProject.runGame();

                        break;
                    case UIManager.COMMAND_SAVE:
                        docView.saveCurrent();
                        break;
                    default:
                        break;
                }

                
            };

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
            };

        }

        private void initControls()
        {
            toolBar = new ToolBarStrip();

            menu = new WhiskeyMenu();
            mainPanel = new Panel();
            mainPanel.Padding = new Padding(0, menu.Height + toolBar.Height, 0, 0);

           



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

            

            mainPanel.Dock = DockStyle.Fill;
            Controls.Add(mainPanel);

            libraryViewDock.dock();
            outputViewDock.dock();
            docViewDock.dock();
            
            //outputViewDock.dock(DockStyle.Bottom);

        }



    }
}
