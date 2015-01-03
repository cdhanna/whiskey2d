using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.UI.Documents.Info
{
    class ProjectSettingsDocumentInfo : DocumentContentInfo
    {

        public Project Project { get; private set; }

        public ProjectSettingsDocumentInfo(Project project)
        {
            Project = project;
        }

    }
}
