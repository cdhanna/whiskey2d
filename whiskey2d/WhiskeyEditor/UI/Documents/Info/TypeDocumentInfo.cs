﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;

namespace WhiskeyEditor.UI.Documents.Info
{
    class TypeDocumentInfo : DocumentContentInfo
    {

        public TypeDescriptor TypeDescriptor { get; private set; }

        public TypeDocumentInfo(TypeDescriptor descriptor)
        {
            TypeDescriptor = descriptor;
        }


        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new TypeContentFactory(DocumentView, this);
        }
    }
}
