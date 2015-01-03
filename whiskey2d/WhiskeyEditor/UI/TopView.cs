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
using WhiskeyEditor.UI.Documents.ContentFactories;
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

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

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


            this.KeyPreview = true;
            this.KeyDown += (s, a) =>
            {
                if (a.Control && a.KeyCode == Keys.S)
                {
                    docView.saveCurrent();
                }
            };

            ProjectManager.Instance.ActiveProject.MediaAdded += mediaAddedListener;
            ProjectManager.Instance.ProjectChanged += (s, a) =>
            {
                a.NewProject.MediaAdded += mediaAddedListener;
            };

            libraryView.SelectionChanged += (s, args) =>
            {

               
                if (args.Selected.FilePath.ToLower().StartsWith(ProjectManager.Instance.ActiveProject.PathMedia.ToLower()))
                {
                    //clicked on a node in the media directory
                }
                else
                {

                    //clicked on a node not in the media directory
                    setDocumentAndProperties(args.Selected.FilePath, true, true);
                }
            };

      
            libraryView.ClickedOnNode += (s, args) =>
            {
                if (args.Selected.FilePath.ToLower().StartsWith(ProjectManager.Instance.ActiveProject.PathMedia.ToLower()))
                {

                }
                else
                {
                    setDocumentAndProperties(args.Selected.FilePath, false, true);
                }
            };



            docView.PropertyChangeRequested += (s, args) =>
            {
                setDocumentAndProperties(UIManager.Instance.getDocumentFactory(args.PropertyDescriptor), false, true);
            };

            SelectionManager.Instance.SelectedInstanceChanged += (s, old) =>
            {
                if (SelectionManager.Instance.SelectedInstance == null)
                {
                    propView.clearPropertyContent();
                }
                else
                {
                    setDocumentAndProperties(UIManager.Instance.getDocumentFactory(SelectionManager.Instance.SelectedInstance), false, true);
                }
            };
           
        }

        public void setDocumentAndProperties(string filePath, bool document, bool properties)
        {
            Descriptor desc = null;
            
            FileDescriptor fDesc = FileManager.Instance.lookUp(filePath);
            desc = fDesc;

            if (desc == null) //try for whiskeyProperties 
            {
                if (filePath.EndsWith(Project.EXTENSION_PROJ))
                {
                    desc = ProjectManager.Instance.ActiveProject;
                }
            }



            if (desc != null)
            {
                IDocumentContentFactory factory = UIManager.Instance.getDocumentFactory(desc);
                setDocumentAndProperties(factory, document, properties);
            }
        }
        public void setDocumentAndProperties(IDocumentContentFactory factory, bool document, bool properties)
        {
            if (factory != null)
            {
                if (properties)
                {
                    propView.setPropertyContent(factory.generatePropertyContent());
                }
                if (document)
                {
                    docView.openDocument(factory.generateDocumentTab());
                }
            }
        }

        private void mediaAddedListener(object sender, EventArgs args)
        {
            libraryView.refreshContent();
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
            UIManager.Instance.setDocumentView(docView);
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
