using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace WhiskeyEditor.UI.Menu
{

    public delegate void ViewChangedEventHandler (object sender, ViewChangedEventArgs args);
    public class ViewChangedEventArgs : EventArgs
    {
        public String ViewName { get; private set; }
        public ViewChangedEventArgs(String name)
        {
            ViewName = name;
        }
    }

    class WhiskeyMenu : MenuStrip
    {

        private OpenFileDialog openProjDialog;

        #region file
        private ToolStripItem fileItem;
        private ToolStripMenuItem fileNew;
        private ToolStripMenuItem fileNewProject;
        private ToolStripMenuItem fileOpen;
        private ToolStripMenuItem fileOpenProject;
        private ToolStripMenuItem fileSave;
        private ToolStripMenuItem fileSaveAll;
        private ToolStripMenuItem fileExit;
        #endregion

        
        #region view
        private ToolStripMenuItem viewItem;
        private ToolStripMenuItem viewOutput;
        private ToolStripMenuItem viewLibrary;
        private ToolStripMenuItem viewDocuments;
        #endregion

        public WhiskeyMenu()
        {
            BackColor = Color.LightGray;//UIManager.Instance.PaleFlairColor;

            initControls();
            addControls();
            configureControls();
            

        }

        #region events

        public event ViewChangedEventHandler ViewToggled;
        private void fireViewToggleEvt(ViewChangedEventArgs args)
        {
            if (ViewToggled != null)
                ViewToggled(this, args);
        }

        #endregion

        private ToolStripMenuItem lookUpViewMenuItem(string name)
        {
            switch (name)
            {
                case UIManager.VIEW_NAME_OUTPUT:
                    return viewOutput;
                case UIManager.VIEW_NAME_LIBRARY:
                    return viewLibrary;
                case UIManager.VIEW_NAME_DOCUMENTS:
                    return viewDocuments;
                default:
                    throw new WhiskeyException("Menu Item of name : " + name + " could not be found");
            }
        }

        public void setViewViewable(string viewName, bool viewable)
        {
            ToolStripMenuItem menuItem = lookUpViewMenuItem(viewName);
            menuItem.Checked = viewable;
        }


        public void configureControls()
        {
            #region VIEW
            viewOutput.Click += (s, a) => {
                fireViewToggleEvt(new ViewChangedEventArgs(UIManager.VIEW_NAME_OUTPUT));
            };

            viewLibrary.Click += (s, a) =>
            {
                fireViewToggleEvt(new ViewChangedEventArgs(UIManager.VIEW_NAME_LIBRARY));
            };

            viewDocuments.Click += (s, a) =>
            {
                fireViewToggleEvt(new ViewChangedEventArgs(UIManager.VIEW_NAME_DOCUMENTS));
            };
            #endregion

            #region FILE

            fileSaveAll.Click += (s, a) =>
            {
                ProjectManager.Instance.ActiveProject.saveGameData();
            };

            fileNewProject.Click += (s, a) =>
            {
                openProjDialog.CheckFileExists = false;
                DialogResult result = openProjDialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Console.WriteLine(openProjDialog.FileName);
                    string projPath = openProjDialog.FileName.Replace(".whiskeyproj", "");
                    string projName = projPath.Substring( projPath.LastIndexOf(Path.DirectorySeparatorChar) +1);
                    
                    Project proj = ProjectManager.Instance.createNewProject(projPath, projName);
                    ProjectManager.Instance.ActiveProject = proj;
                }
            };

            fileOpenProject.Click += (s, a) =>
            {
                openProjDialog.CheckFileExists = true;
                DialogResult result = openProjDialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Console.WriteLine(openProjDialog.FileName);
                    string projPath = openProjDialog.FileName.Replace(".whiskeyproj", "");
                    string projName = projPath.Substring(projPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);

                    Project proj = ProjectManager.Instance.openProject(projPath);
                   
                    ProjectManager.Instance.ActiveProject = proj;
                    ProjectManager.Instance.ActiveProject.loadGameData();
                }
            };

            fileExit.Click += (s, a) =>
            {
                UIManager.Instance.requestClose();
            };

            #endregion
        }

        private void initControls()
        {
            openProjDialog = new OpenFileDialog();
            openProjDialog.DefaultExt = ".whiskeyproj";
            openProjDialog.Filter = "WhiskeyProjects|*.whiskeyproj";
            openProjDialog.CheckPathExists = false;
            openProjDialog.CheckFileExists = false;

            fileNewProject = new ToolStripMenuItem("Project");
            fileNew = new ToolStripMenuItem("New");
            fileNew.DropDown.Items.Add(fileNewProject);

            fileOpenProject = new ToolStripMenuItem("Project");
            fileOpen = new ToolStripMenuItem("Open");
            fileOpen.DropDown.Items.Add(fileOpenProject);

            fileSaveAll = new ToolStripMenuItem("Save All");
            

            fileExit = new ToolStripMenuItem("Exit");

            
            fileItem = new ToolStripMenuItem("File", null, 
                fileNew,
                fileOpen,
                fileSaveAll,
                fileExit);


            viewOutput = new ToolStripMenuItem("Output");
            viewLibrary = new ToolStripMenuItem("Library");
            viewDocuments = new ToolStripMenuItem("Documents");
            viewItem = new ToolStripMenuItem("View", null,
                viewOutput,
                viewLibrary,
                viewDocuments);
            

        }

        private void addControls()
        {
            this.Items.Add(fileItem);
            this.Items.Add(viewItem);
        }

    }
}
