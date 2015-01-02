using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;


namespace WhiskeyEditor.UI.Documents.ContentFactories
{
    interface IDocumentContentFactory
    {

        PropertyContent generatePropertyContent();
        DocumentTab generateDocumentTab();

    }



}
