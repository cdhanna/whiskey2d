using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Documents.ContentFactories;
namespace WhiskeyEditor.UI.Documents.Info
{
    class ShaderDocumentInfo : DocumentContentInfo
    {
        public ShaderDescriptor Shader { get; private set; }
        public ShaderDocumentInfo(ShaderDescriptor shader)
        {
            Shader = shader;
        }
        public IDocumentContentFactory generateContentFactory(DocumentView DocumentView)
        {
            return new ShaderContentFactory(DocumentView, this);
        }
    }
}
