using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using System.Windows.Forms;
using System.Drawing;

namespace WhiskeyEditor.UI.Documents
{
    class ProjectSettingsDocument : DocumentTab
    {

        public ProjectSettingsControl control { get; private set; }
        public Project Project { get; private set; }


        public ProjectSettingsDocument(Project project, DocumentView docView)
            : base(project.Name, docView)
        {
            Project = project;
            initControls();
            addControls();
            configureControls();
        }

        public override void Refresh()
        {
            setValuesFromProject();
            base.Refresh();
        }
        public override void open()
        {
            setValuesFromProject();
            base.open();
        }

        public override void save(ProgressNotifier pn)
        {
            Invoke(new NoArgFunction(() =>
            {
                Project.Name = control.ProjectName;
                Project.GameStartScene = control.StartScene;

                base.save(pn);
            }));
        }

        private void setValuesFromProject()
        {
            control.ProjectName = Project.Name;

            //change folder structure?

            List<string> levelNames = InstanceManager.Instance.Levels.Select(f => f.LevelName).ToList();
            control.setLevelOptions(levelNames);

            control.StartScene = Project.GameStartScene;

        }

        private void initControls()
        {
            control = new ProjectSettingsControl();
        }
        private void configureControls()
        {
        }
        private void addControls()
        {
            control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(control);
        }

    }
}
