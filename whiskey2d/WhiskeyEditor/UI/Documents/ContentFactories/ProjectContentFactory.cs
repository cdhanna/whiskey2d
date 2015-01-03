using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Documents.Info;
namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    class ProjectContentFactory : IDocumentContentFactory
    {
        public DocumentView DocumentView { get; private set; }
        public ProjectSettingsDocumentInfo Info { get; private set; }

        public ProjectContentFactory(DocumentView docView, ProjectSettingsDocumentInfo info)
        {
            DocumentView = docView;
            Info = info;
        }

        public PropertyEditor generatePropertyContent()
        {
            return new PropertyEditor(Info.Project);
        }

        public DocumentTab generateDocumentTab()
        {

            ProjectSettingsDocument doc = (ProjectSettingsDocument)DocumentView.getTab(Info.Project.Name);
            if (doc == null)
            {
                doc = new ProjectSettingsDocument(Info.Project, DocumentView);
            }
            return doc;

        }
    }
}
